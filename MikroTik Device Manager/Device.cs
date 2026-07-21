using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;
using MikroTik_Device_Manager.models;
using System.Text;

namespace MikroTik_Device_Manager
{
    /// <summary>
    /// Дочірня форма для пошуку DHCP lease за MAC-адресою та виконання дій над знайденим пристроєм.
    /// </summary>
    public partial class Device : Form
    {
        // Батьківська форма, яку потрібно розблокувати після закриття цього вікна.
        private readonly DeviceManager _form;
        // SSH-менеджер для запитів і змін на MikroTik.
        private readonly MikroTikManager _manager;
        // Поточний стан тимчасового блокування UI під час виконання SSH-команди.
        private bool _isBusy;
        // Останній успішно завантажений DHCP lease, який визначає доступні бізнес-дії.
        private LeaseInfo? _currentLease;
        // True лише після успішного завантаження списків адрес; порожній список є валідним результатом.
        private bool _addressListsLoaded;
        // Нормалізована MAC-адреса у форматі RouterOS: XX:XX:XX:XX:XX:XX.
        private string _MAC { get; set; } = string.Empty;

        public Device(DeviceManager form, MikroTikManager manager)
        {
            InitializeComponent();
            _form = form;
            _manager = manager;

            comBox_AddAddressList.SelectedIndexChanged += comBox_AddAddressList_SelectedIndexChanged;
            NullStatusForm();
        }

        /// <summary>
        /// Скидає форму до початкового стану: очищає текст і ховає кнопки дій.
        /// </summary>
        private void NullStatusForm()
        {
            _currentLease = null;
            _addressListsLoaded = false;
            tB_Monitor.Clear();
            L_AddressList.Text = string.Empty;
            L_IP_Now.Text = string.Empty;
            comBox_AddAddressList.Text = string.Empty;
            comBox_AddAddressList.Items.Clear();
            UpdateControlsState();
        }

        /// <summary>
        /// Блокує взаємодію з формою на час виконання SSH-запиту.
        /// </summary>
        private void LockControls()
        {
            _isBusy = true;
            UseWaitCursor = true;
            UpdateControlsState();
        }

        /// <summary>
        /// Знімає тимчасове блокування після SSH-запиту, не змінюючи бізнес-стан форми.
        /// </summary>
        private void UnlockControls()
        {
            _isBusy = false;
            UseWaitCursor = false;
            UpdateControlsState();
        }

        /// <summary>
        /// Єдине джерело істини для Visible/Enabled стану контролів відповідно до busy та lease-стану.
        /// </summary>
        private void UpdateControlsState()
        {
            bool hasLease = _currentLease is not null;
            bool isStaticLease = hasLease && !_currentLease!.Dynamic;
            bool hasAddressList = isStaticLease && _currentLease!.AddressList != "not list";
            bool canAddAddressList = isStaticLease && !hasAddressList;
            bool hasAvailableAddressLists = _addressListsLoaded && comBox_AddAddressList.Items.Count > 0;
            bool hasSelectedAddressList = comBox_AddAddressList.SelectedItem is string selectedList
                                          && !string.IsNullOrWhiteSpace(selectedList);
            bool canInteract = !_isBusy;

            tB_Mac.Enabled = canInteract;
            b_Find.Enabled = canInteract;
            tB_Monitor.Enabled = canInteract;

            SetControlState(b_ClearForm, !string.IsNullOrEmpty(_MAC), canInteract);
            SetControlState(L_TextBoard, hasLease, canInteract);
            SetControlState(L_AvailableAction, hasLease, canInteract);
            SetControlState(L_IP, hasLease, canInteract);
            SetControlState(L_IP_Now, hasLease, canInteract);
            SetControlState(L_AddAddressList, hasLease, canInteract);
            SetControlState(L_AddressList, hasLease, canInteract);

            SetControlState(b_MakeStatic, hasLease && _currentLease!.Dynamic, canInteract);
            SetControlState(b_RemoveIP, isStaticLease, canInteract);
            SetControlState(comBox_AddAddressList, canAddAddressList && hasAvailableAddressLists, canInteract);
            SetControlState(b_AddAddressList, canAddAddressList && hasAvailableAddressLists && hasSelectedAddressList, canInteract);
            SetControlState(b_RemoveAddressList, hasAddressList, canInteract);
        }

        private void comBox_AddAddressList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateControlsState();
        }

        private static void SetControlState(Control control, bool visible, bool canInteract)
        {
            control.Visible = visible;
            control.Enabled = visible && canInteract;
        }

        private void HandleBrokenConnection()
        {
            MessageBox.Show(
                "Маршрутизатор не відповідає.\n\nSSH-з’єднання перервано.\n\nБудь ласка, підключіться знову, щоб продовжити роботу",
                "SSH connection lost",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            Close();
            _form.Close();
        }

        private bool StopIfConnectionBroken()
        {
            if (!_manager.IsConnectionBroken)
                return false;

            HandleBrokenConnection();
            return true;
        }

        /// <summary>
        /// Повертає керування батьківській формі після закриття пошуку за MAC.
        /// </summary>
        private void Device_FormClosed(object sender, FormClosedEventArgs e)
        {
            _form.ControlBox = true;
            _form.b_FindMAC.Enabled = true;
            _form.Activate();
        }

        /// <summary>
        /// Очищає введення від розділювачів, перевіряє 12 hex-символів і приводить MAC до стандартного формату.
        /// </summary>
        private bool NormalizeMac()
        {
            string mac = tB_Mac.Text.Trim().ToUpper();
            const string validChars = "0123456789ABCDEF";
            List<char> tmp = new();

            foreach (char c in mac)
            {
                if (validChars.Contains(c))
                    tmp.Add(c);
            }

            if (tmp.Count != 12)
                return false;

            _MAC = $"{tmp[0]}{tmp[1]}:{tmp[2]}{tmp[3]}:{tmp[4]}{tmp[5]}:{tmp[6]}{tmp[7]}:{tmp[8]}{tmp[9]}:{tmp[10]}{tmp[11]}";
            tB_Mac.Text = _MAC;
            return true;
        }

        /// <summary>
        /// Запускає оновлення інформації про lease для введеної MAC-адреси.
        /// </summary>
        private async void b_Find_Click(object sender, EventArgs e)
        {
            if (!NormalizeMac())
            {
                _MAC = string.Empty;
                NullStatusForm();
                tB_Monitor.Text = "MAC address entry error";
                return;
            }

            await RefreshLeaseInfoAsync();
        }

        /// <summary>
        /// Публічна для обробників обгортка: блокує UI та викликає основну логіку оновлення.
        /// </summary>
        private async Task RefreshLeaseInfoAsync()
        {
            LockControls();

            try
            {
                await RefreshLeaseInfoCoreAsync(_MAC);
            }
            finally
            {
                if (!IsDisposed)
                    UnlockControls();
            }
        }

        /// <summary>
        /// Шукає lease на роутері, завантажує деталі та оновлює доступні дії у формі.
        /// </summary>
        private async Task RefreshLeaseInfoCoreAsync(string mac)
        {
            NullStatusForm();

            var response = await _manager.ExecuteCommandWithResultAsync(
                RouterCommands.GetDHCPLeaseByMAC(mac));

            if (!response.Success)
            {
                if (StopIfConnectionBroken())
                    return;

                tB_Monitor.Text = _manager.LastError;
                return;
            }

            StringBuilder sb = new();

            foreach (string line in response.Result.Split(new[] { "\r\n" }, StringSplitOptions.None))
            {
                if (line.Length > 0)
                    sb.AppendLine(line);
            }

            if (sb.Length == 0)
            {
                tB_Monitor.Text = "There is no device with this MAC address.";
                return;
            }

            LeaseInfo lease = new(mac);

            if (!await lease.FillLeaseInfoAsync(_manager))
            {
                if (StopIfConnectionBroken())
                    return;

                tB_Monitor.Text = _manager.LastError;
                return;
            }

            sb.AppendLine(lease.ShowInfo());
            if (!await FillLeaseInfoAsync(lease))
                return;

            if (StopIfConnectionBroken())
                return;

            tB_Monitor.Text = sb.ToString();
        }

        /// <summary>
        /// Заповнює елементи форми відповідно до актуального стану DHCP Lease.
        /// </summary>
        private async Task<bool> FillLeaseInfoAsync(LeaseInfo lease)
        {
            L_IP_Now.Text = lease.Ip;
            L_AddressList.Text = lease.AddressList;

            if (!lease.Dynamic && lease.AddressList == "not list")
            {
                if (!await LoadAddressListsAsync())
                    return false;

                if (StopIfConnectionBroken())
                    return false;
            }

            _currentLease = lease;
            UpdateControlsState();
            return true;
        }

        /// <summary>
        /// Завантажує з роутера назви address-list і додає їх у ComboBox для вибору.
        /// </summary>
        private async Task<bool> LoadAddressListsAsync()
        {
            comBox_AddAddressList.Items.Clear();
            _addressListsLoaded = false;

            var response = await _manager.ExecuteCommandWithResultAsync(
                RouterCommands.ListOfAddressLists());

            if (!response.Success)
            {
                if (StopIfConnectionBroken())
                    return false;

                tB_Monitor.Text = _manager.LastError;
                return false;
            }

            List<string> splitText = response.Result
                .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Distinct()
                .ToList();

            foreach (string line in splitText)
                comBox_AddAddressList.Items.Add(line);

            _addressListsLoaded = true;
            return true;
        }

        /// <summary>
        /// Очищає форму та забуває поточну MAC-адресу.
        /// </summary>
        private void b_ClearForm_Click(object sender, EventArgs e)
        {
            _MAC = string.Empty;
            tB_Mac.Clear();
            NullStatusForm();
        }

        /// <summary>
        /// Виконує зміну на роутері, а після успіху повторно завантажує актуальний стан lease.
        /// </summary>
        private async Task ExecuteAndRefreshAsync(string command, string mac)
        {
            LockControls();

            try
            {
                if (await _manager.ExecuteCommandAsync(command))
                    await RefreshLeaseInfoCoreAsync(mac);
                else if (StopIfConnectionBroken())
                    return;
                else
                    tB_Monitor.AppendText(Environment.NewLine + _manager.LastError);
            }
            finally
            {
                if (!IsDisposed)
                    UnlockControls();
            }
        }

        /// <summary>
        /// Робить знайдений DHCP lease статичним.
        /// </summary>
        private async void b_MakeStatic_Click(object sender, EventArgs e)
        {
            string mac = _MAC;
            await ExecuteAndRefreshAsync(RouterCommands.MakeStatic(mac), mac);
        }

        /// <summary>
        /// Видаляє знайдений DHCP lease з роутера.
        /// </summary>
        private async void b_RemoveIP_Click(object sender, EventArgs e)
        {
            string mac = _MAC;
            await ExecuteAndRefreshAsync(RouterCommands.RemoveLease(mac), mac);
        }

        /// <summary>
        /// Додає вибраний address-list до знайденого lease.
        /// </summary>
        private async void b_AddAddressList_Click(object sender, EventArgs e)
        {
            if (comBox_AddAddressList.SelectedItem is not string selectedList
                || string.IsNullOrWhiteSpace(selectedList))
                return;

            string mac = _MAC;
            await ExecuteAndRefreshAsync(RouterCommands.SetAddressList(mac, selectedList), mac);
        }

        /// <summary>
        /// Очищає address-list у знайденому lease.
        /// </summary>
        private async void b_RemoveAddressList_Click(object sender, EventArgs e)
        {
            string mac = _MAC;
            await ExecuteAndRefreshAsync(RouterCommands.ClearAddressList(mac), mac);
        }
    }
}
