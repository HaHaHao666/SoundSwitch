using NAudio.CoreAudioApi;

namespace SoundSwitch
{
    public partial class FormSettings : Form
    {
        private Dictionary<string, string> data;
        private FormSoundSwitch mainForm;
        private uint hotkeyModifiers = 0;
        private uint hotkeyKey = 0;

        private const uint MOD_ALT = 0x0001;
        private const uint MOD_CTRL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;
        private const uint MOD_WIN = 0x0008;

        class DeviceItem
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public override string ToString() => Name;
        }

        public FormSettings(Dictionary<string, string> data, FormSoundSwitch mainForm)
        {
            this.data = data;
            this.mainForm = mainForm;
            InitializeComponent();
            LoadDevices();
            LoadCurrentSettings();
        }

        private void LoadDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            string[] selectedIds = Array.Empty<string>();
            if (data.ContainsKey("SelectedDevices") && !string.IsNullOrEmpty(data["SelectedDevices"]))
            {
                selectedIds = data["SelectedDevices"].Split(',');
            }

            checkedListBoxDevices.Items.Clear();
            for (int i = 0; i < devices.Count; i++)
            {
                var device = devices[i];
                var item = new DeviceItem { Name = device.FriendlyName, Id = device.ID };
                bool isChecked = selectedIds.Contains(device.ID);
                checkedListBoxDevices.Items.Add(item, isChecked);
            }

            RebuildDefaultDeviceDropdown();
        }

        private void LoadCurrentSettings()
        {
            if (data.ContainsKey("HotkeyModifiers") && data.ContainsKey("HotkeyKey"))
            {
                hotkeyModifiers = uint.Parse(data["HotkeyModifiers"]);
                hotkeyKey = uint.Parse(data["HotkeyKey"]);
                txtHotkey.Text = HotkeyToString(hotkeyModifiers, hotkeyKey);
            }

            if (data.ContainsKey("DefaultDevice"))
            {
                string defaultId = data["DefaultDevice"];
                for (int i = 0; i < cmbDefaultDevice.Items.Count; i++)
                {
                    if (((DeviceItem)cmbDefaultDevice.Items[i]).Id == defaultId)
                    {
                        cmbDefaultDevice.SelectedIndex = i;
                        break;
                    }
                }
            }

            chkAutorun.Checked = mainForm.IsInAutorun();
        }

        private void RebuildDefaultDeviceDropdown()
        {
            string previousDefaultId = null;
            if (cmbDefaultDevice.SelectedItem is DeviceItem selected)
            {
                previousDefaultId = selected.Id;
            }

            cmbDefaultDevice.Items.Clear();
            for (int i = 0; i < checkedListBoxDevices.Items.Count; i++)
            {
                if (checkedListBoxDevices.GetItemChecked(i))
                {
                    cmbDefaultDevice.Items.Add(checkedListBoxDevices.Items[i]);
                }
            }

            if (previousDefaultId != null)
            {
                for (int i = 0; i < cmbDefaultDevice.Items.Count; i++)
                {
                    if (((DeviceItem)cmbDefaultDevice.Items[i]).Id == previousDefaultId)
                    {
                        cmbDefaultDevice.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void checkedListBoxDevices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (IsHandleCreated)
                BeginInvoke(() => RebuildDefaultDeviceDropdown());
        }

        private void txtHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                hotkeyModifiers = 0;
                hotkeyKey = 0;
                txtHotkey.Text = "";
                return;
            }

            // Ignore bare modifier keys
            if (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey ||
                e.KeyCode == Keys.Menu || e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
                return;

            // Require at least one modifier
            uint mod = 0;
            if (e.Control) mod |= MOD_CTRL;
            if (e.Alt) mod |= MOD_ALT;
            if (e.Shift) mod |= MOD_SHIFT;

            if (mod == 0)
                return;

            hotkeyModifiers = mod;
            hotkeyKey = (uint)e.KeyCode;
            txtHotkey.Text = HotkeyToString(hotkeyModifiers, hotkeyKey);
        }

        private void btnClearHotkey_Click(object sender, EventArgs e)
        {
            hotkeyModifiers = 0;
            hotkeyKey = 0;
            txtHotkey.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Count checked devices
            int checkedCount = 0;
            for (int i = 0; i < checkedListBoxDevices.Items.Count; i++)
            {
                if (checkedListBoxDevices.GetItemChecked(i))
                    checkedCount++;
            }

            if (checkedCount < 2)
            {
                MessageBox.Show("Please select at least 2 devices for cycling.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDefaultDevice.SelectedItem == null)
            {
                MessageBox.Show("Please select a default device.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Collect selected device IDs
            var selectedIds = new List<string>();
            for (int i = 0; i < checkedListBoxDevices.Items.Count; i++)
            {
                if (checkedListBoxDevices.GetItemChecked(i))
                {
                    selectedIds.Add(((DeviceItem)checkedListBoxDevices.Items[i]).Id);
                }
            }

            data["SelectedDevices"] = string.Join(",", selectedIds);
            data["DefaultDevice"] = ((DeviceItem)cmbDefaultDevice.SelectedItem).Id;
            data["HotkeyModifiers"] = hotkeyModifiers.ToString();
            data["HotkeyKey"] = hotkeyKey.ToString();

            // Update autorun registry
            if (chkAutorun.Checked && !mainForm.IsInAutorun())
                mainForm.AddToAutorun();
            else if (!chkAutorun.Checked && mainForm.IsInAutorun())
                mainForm.RemoveFromAutorun();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private static string HotkeyToString(uint modifiers, uint key)
        {
            if (key == 0) return "";

            var parts = new List<string>();
            if ((modifiers & MOD_CTRL) != 0) parts.Add("Ctrl");
            if ((modifiers & MOD_ALT) != 0) parts.Add("Alt");
            if ((modifiers & MOD_SHIFT) != 0) parts.Add("Shift");
            if ((modifiers & MOD_WIN) != 0) parts.Add("Win");

            Keys k = (Keys)key;
            parts.Add(k.ToString());

            return string.Join("+", parts);
        }
    }
}
