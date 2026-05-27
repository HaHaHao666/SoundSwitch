using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SoundSwitch
{
    public partial class FormSoundSwitch : Form
    {
        private MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        private MMDeviceCollection devices;
        private MMDevice selectedDevice;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;
        private const int WM_HOTKEY = 0x0312;

        private bool isDragging = false;

        private VolumeController vc = new VolumeController();

        private Dictionary<string, string> data = new Dictionary<string, string>();

        private const string AutorunKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private static string AppName => Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);

        public float defaultOpacity = 0.7F;

        int clientWidth = 0;
        int clientHeight = 0;
        int totalWidth = 0;
        int totalHeight = 0;
        int leftPos = 0;
        int topPos = 0;

        class AudioDeviceItem
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public override string ToString() => Name;
        }

        // INIT
        public FormSoundSwitch()
        {
            InitializeComponent();
            LoadAudioDevices();
            vc.SetCurrentDevice();

            instantProgressBar1.Value = vc.GetVolume();

            LoadData();

            if (data.ContainsKey("TopMost"))
            {
                this.TopMost = data["TopMost"] == "1";
            }

            if (data.ContainsKey("ShowInTaskbar"))
            {
                this.ShowInTaskbar = data["ShowInTaskbar"] == "1";
            }

            if (data.ContainsKey("Opacity"))
            {
                this.Opacity = float.Parse(data["Opacity"]);
            }

            if (data.ContainsKey("Left"))
            {
                this.Left = Int32.Parse(data["Left"]);

                if (this.Left < 0)
                {
                    this.Left = 0;
                }
            }

            if (data.ContainsKey("Top"))
            {
                this.Top = Int32.Parse(data["Top"]);
                if (this.Top < 0)
                {
                    this.Top = 0;
                }
            }

            this.MaximizeBox = false;

            // HIDE TITLE BAR
            clientWidth = this.ClientSize.Width;
            clientHeight = this.ClientSize.Height;
            totalWidth = this.Width;
            totalHeight = this.Height;
            leftPos = this.Left;
            topPos = this.Top;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            vc.VolumeChanged += OnSystemVolumeChanged;
        }

        // PREVENT MAXIMALIZE
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MAXIMIZE = 0xF030;
            const int SC_MINIMIZE = 0xF020;

            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() == SC_MAXIMIZE))
                return;

            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() == SC_MINIMIZE))
            {
                MinimizeToTray();
                return;
            }

            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
            {
                CycleToNextDevice();
                return;
            }

            base.WndProc(ref m);
        }

        // MINIMIZE TO SYSTEM TRAY
        private void MinimizeToTray()
        {
            this.Hide();
            notifyIcon.Visible = true;
        }

        // RESTORE FROM SYSTEM TRAY
        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            notifyIcon.Visible = false;
        }

        // TRAY ICON DOUBLE CLICK
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RegisterConfiguredHotkey();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            base.OnHandleDestroyed(e);
        }

        private void RegisterConfiguredHotkey()
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);

            if (data.ContainsKey("HotkeyKey") && data.ContainsKey("HotkeyModifiers"))
            {
                uint mod = uint.Parse(data["HotkeyModifiers"]);
                uint key = uint.Parse(data["HotkeyKey"]);
                if (key != 0)
                {
                    bool success = RegisterHotKey(this.Handle, HOTKEY_ID, mod, key);
                    if (!success)
                    {
                        MessageBox.Show("Failed to register hotkey. It may be in use by another application.",
                            "Hotkey Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void CycleToNextDevice()
        {
            if (!data.ContainsKey("SelectedDevices") || string.IsNullOrEmpty(data["SelectedDevices"]))
                return;

            string[] cycleDeviceIds = data["SelectedDevices"].Split(',');
            if (cycleDeviceIds.Length < 2) return;

            var currentDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            string currentId = currentDefault.ID;

            int currentIndex = Array.IndexOf(cycleDeviceIds, currentId);

            for (int attempt = 0; attempt < cycleDeviceIds.Length; attempt++)
            {
                int tryIndex = (currentIndex + 1 + attempt) % cycleDeviceIds.Length;
                try
                {
                    AudioDeviceSwitcher.SetDefaultDevice(cycleDeviceIds[tryIndex]);
                    vc.SetCurrentDevice();
                    LoadAudioDevices();
                    instantProgressBar1.Value = vc.GetVolume();
                    break;
                }
                catch
                {
                    continue;
                }
            }
        }

        //EVENT LOAD FORM
        private void FormSoundSwitch_Load(object sender, EventArgs e)
        {

        }

        // EVENT ACTIVATE FORM
        private void FormSoundSwitch_Activated(object sender, EventArgs e)
        {
            LoadAudioDevices();
            instantProgressBar1.Value = vc.GetVolume();

            if (this.WindowState != FormWindowState.Minimized)
            {
                if (this.FormBorderStyle == FormBorderStyle.None)
                {
                    this.Left = leftPos;
                    this.Top = topPos;
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    if (clientWidth == 0)
                    {
                        clientWidth = this.ClientSize.Width;
                        clientHeight = this.ClientSize.Height;
                        totalWidth = this.Width;
                        totalHeight = this.Height;
                    }
                }

                if (this.Width < 321)
                {
                    this.Width = 321;
                }

                if (this.Height < 122)
                {
                    this.Height = 122;
                }

                Rectangle formBounds = this.Bounds;
                bool isVisibleOnAnyScreen = Screen.AllScreens
                    .Any(screen => screen.WorkingArea.IntersectsWith(formBounds));

                if (!isVisibleOnAnyScreen)
                {
                    Screen currentScreen = Screen.FromPoint(Cursor.Position);
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(
                        currentScreen.WorkingArea.Left + (currentScreen.WorkingArea.Width - this.Width) / 2,
                        currentScreen.WorkingArea.Top + (currentScreen.WorkingArea.Height - this.Height) / 2
                    );
                }
            }
        }

        // EVENT DEACTIVATE FORM
        private void FormSoundSwitch_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                if (this.FormBorderStyle == FormBorderStyle.FixedSingle)
                {
                    leftPos = this.Left;
                    topPos = this.Top;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.Left += (totalWidth - clientWidth) / 2;
                    int borderWidth = (totalWidth - clientWidth) / 2;
                    this.Top += (totalHeight - clientHeight) - borderWidth;
                }
            }
        }

        // EVENT FORM CLOSING
        private void FormSoundSwitch_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;

            if (this.WindowState == FormWindowState.Minimized || !this.Visible)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }

            data["Left"] = this.Left.ToString();
            data["Top"] = this.Top.ToString();
            

            data["TopMost"] = this.TopMost ? "1" : "0";
            data["ShowInTaskbar"] = this.ShowInTaskbar ? "1" : "0";
            data["Opacity"] = this.Opacity.ToString();

            SaveData();
        }

        // OPTIONS LOAD XML
        void LoadData()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SoundSwitch", "options.xml");

            data.Clear();

            if (!File.Exists(path))
                return;

            var root = XElement.Load(path);

            foreach (var entry in root.Elements("item"))
            {
                var key = entry.Attribute("Key")?.Value;
                var value = entry.Attribute("Value")?.Value;

                if (key != null)
                    data[key] = value;
            }


            return;
        }

        // OPTIONS SAVE XML 
        void SaveData()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SoundSwitch");
            Directory.CreateDirectory(folder);
            string path = Path.Combine(folder, "options.xml");

            var root = new XElement("root");

            foreach (var pair in data)
            {
                var entry = new XElement("item",
                    new XAttribute("Key", pair.Key),
                    new XAttribute("Value", pair.Value));
                root.Add(entry);
            }

            root.Save(path);
        }

        // AUDIO DEVICES LIST
        private void LoadAudioDevices()
        {
            bool somethingChange = false;
            MMDeviceCollection currentDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            MMDevice currentDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            if (devices== null || currentDevices.Count() != devices.Count() || selectedDevice == null || selectedDevice.ID != currentDevice.ID) {
                somethingChange = true;
            }
            else {
                for (int i = 0; i < devices.Count; i++)
                {
                    if (devices[i].FriendlyName != currentDevices[i].FriendlyName ||
                        devices[i].ID != currentDevices[i].ID
                    ) {
                        somethingChange = true;
                        break;
                    }
                }
            }

            if (somethingChange) {
                devices = currentDevices;
                selectedDevice = currentDevice;
                comboBox1.Items.Clear();
                
                for (int i = 0; i < devices.Count; i++)
                {
                    var device = devices[i];
                    comboBox1.Items.Add(new AudioDeviceItem
                    {
                        Name = device.FriendlyName,
                        Id = device.ID
                    });

                    if (device.ID == currentDevice.ID)
                        comboBox1.SelectedIndex = i;
                }
            }
        }

        // COMBOBX AUDIO DEVICES LIST
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is AudioDeviceItem selected)
            {
                AudioDeviceSwitcher.SetDefaultDevice(selected.Id);
                vc.SetCurrentDevice();
                selectedDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }
        }

        // VOLUME PROGRESSBAR UPDATE
        private void UpdateProgressBarValue(int mouseX)
        {
            int width = instantProgressBar1.Width;
            int value = mouseX * (instantProgressBar1.Maximum - instantProgressBar1.Minimum) / width;

            if (value < instantProgressBar1.Minimum) value = instantProgressBar1.Minimum;
            if (value > instantProgressBar1.Maximum) value = instantProgressBar1.Maximum;

            instantProgressBar1.Value = value;

            vc.SetVolume(value);
        }

        // VOLUME SYNC FROM SYSTEM
        private void OnSystemVolumeChanged(int volume)
        {
            if (isDragging) return;
            if (IsHandleCreated)
            {
                BeginInvoke(() => instantProgressBar1.Value = volume);
            }
        }

        // VOLUME PROGRESSBAR UPDATE
        private void instantProgressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            UpdateProgressBarValue(e.X);
        }

        // VOLUME PROGRESSBAR UPDATE
        private void instantProgressBar1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                UpdateProgressBarValue(e.X);
            }
        }

        // VOLUME PROGRESSBAR UPDATE
        private void instantProgressBar1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // AUTORUN
        public bool IsInAutorun()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutorunKeyPath, false))
            {
                return key?.GetValue(AppName) != null;
            }
        }

        // AUTORUN
        public void AddToAutorun()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutorunKeyPath, true))
            {
                key.SetValue(AppName, $"\"{exePath}\"");
            }
        }

        // AUTORUN
        public void RemoveFromAutorun()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AutorunKeyPath, true))
            {
                key.DeleteValue(AppName, false);
            }
        }

        // AUTORUN
        public string GetCurrentExecutablePath()
        {
            return Process.GetCurrentProcess().MainModule.FileName;
        }

        // CONTEXTMENU OPEN
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mostTopToolStripMenuItem.Checked = this.TopMost;
            autorunToolStripMenuItem.Checked = this.IsInAutorun();
            showInTaskbarToolStripMenuItem.Checked = this.ShowInTaskbar;
            oppacityToolStripMenuItem.Checked = (this.Opacity == defaultOpacity);
        }

        // CONTEXTMENU Close
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // CONTEXTMENU Most Top
        private void mostTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            mostTopToolStripMenuItem.Checked = this.TopMost;
        }

        // CONTEXTMENU AUTORUN OPTION
        private void autorunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.IsInAutorun())
            {
                this.AddToAutorun();
            }
            else
            {
                this.RemoveFromAutorun();
            }
        }

        // CONTEXTMENU SHOW IN TASKBAR OPTION
        private void showInTaskbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = !this.ShowInTaskbar;
            showInTaskbarToolStripMenuItem.Checked = this.ShowInTaskbar;
            
        }

        // CONTEXTMENU OPACITY OPTION
        private void oppacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = (this.Opacity == 1) ? defaultOpacity : 1;
            oppacityToolStripMenuItem.Checked = (this.Opacity == defaultOpacity);
        }

        // SETTINGS BUTTON
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new FormSettings(data, this))
            {
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    RegisterConfiguredHotkey();
                    SaveData();
                }
            }
        }
    }
}
