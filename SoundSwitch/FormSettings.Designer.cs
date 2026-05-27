namespace SoundSwitch
{
    partial class FormSettings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblDevices = new Label();
            checkedListBoxDevices = new CheckedListBox();
            lblDefaultDevice = new Label();
            cmbDefaultDevice = new ComboBox();
            lblHotkey = new Label();
            txtHotkey = new TextBox();
            btnClearHotkey = new Button();
            chkAutorun = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            //
            // lblDevices
            //
            lblDevices.AutoSize = true;
            lblDevices.Font = new Font("Segoe UI", 10F);
            lblDevices.Location = new Point(16, 16);
            lblDevices.Name = "lblDevices";
            lblDevices.Size = new Size(280, 19);
            lblDevices.TabIndex = 0;
            lblDevices.Text = "Select devices for hotkey cycling:";
            //
            // checkedListBoxDevices
            //
            checkedListBoxDevices.CheckOnClick = true;
            checkedListBoxDevices.Font = new Font("Segoe UI", 10F);
            checkedListBoxDevices.FormattingEnabled = true;
            checkedListBoxDevices.Location = new Point(16, 40);
            checkedListBoxDevices.Name = "checkedListBoxDevices";
            checkedListBoxDevices.Size = new Size(388, 140);
            checkedListBoxDevices.TabIndex = 1;
            checkedListBoxDevices.ItemCheck += checkedListBoxDevices_ItemCheck;
            //
            // lblDefaultDevice
            //
            lblDefaultDevice.AutoSize = true;
            lblDefaultDevice.Font = new Font("Segoe UI", 10F);
            lblDefaultDevice.Location = new Point(16, 192);
            lblDefaultDevice.Name = "lblDefaultDevice";
            lblDefaultDevice.Size = new Size(105, 19);
            lblDefaultDevice.TabIndex = 2;
            lblDefaultDevice.Text = "Default device:";
            //
            // cmbDefaultDevice
            //
            cmbDefaultDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDefaultDevice.Font = new Font("Segoe UI", 10F);
            cmbDefaultDevice.FormattingEnabled = true;
            cmbDefaultDevice.Location = new Point(16, 216);
            cmbDefaultDevice.Name = "cmbDefaultDevice";
            cmbDefaultDevice.Size = new Size(388, 25);
            cmbDefaultDevice.TabIndex = 3;
            //
            // lblHotkey
            //
            lblHotkey.AutoSize = true;
            lblHotkey.Font = new Font("Segoe UI", 10F);
            lblHotkey.Location = new Point(16, 256);
            lblHotkey.Name = "lblHotkey";
            lblHotkey.Size = new Size(100, 19);
            lblHotkey.TabIndex = 4;
            lblHotkey.Text = "Global hotkey:";
            //
            // txtHotkey
            //
            txtHotkey.Font = new Font("Segoe UI", 10F);
            txtHotkey.Location = new Point(16, 280);
            txtHotkey.Name = "txtHotkey";
            txtHotkey.ReadOnly = true;
            txtHotkey.Size = new Size(300, 25);
            txtHotkey.TabIndex = 5;
            txtHotkey.PlaceholderText = "Press a key combination...";
            txtHotkey.KeyDown += txtHotkey_KeyDown;
            //
            // btnClearHotkey
            //
            btnClearHotkey.Font = new Font("Segoe UI", 9F);
            btnClearHotkey.Location = new Point(324, 280);
            btnClearHotkey.Name = "btnClearHotkey";
            btnClearHotkey.Size = new Size(80, 25);
            btnClearHotkey.TabIndex = 6;
            btnClearHotkey.Text = "Clear";
            btnClearHotkey.UseVisualStyleBackColor = true;
            btnClearHotkey.Click += btnClearHotkey_Click;
            //
            // chkAutorun
            //
            chkAutorun.AutoSize = true;
            chkAutorun.Font = new Font("Segoe UI", 10F);
            chkAutorun.Location = new Point(16, 324);
            chkAutorun.Name = "chkAutorun";
            chkAutorun.Size = new Size(180, 23);
            chkAutorun.TabIndex = 7;
            chkAutorun.Text = "Run at startup";
            chkAutorun.UseVisualStyleBackColor = true;
            //
            // btnSave
            //
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.Location = new Point(220, 368);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 32);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            //
            // btnCancel
            //
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Location = new Point(316, 368);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 32);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // FormSettings
            //
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 420);
            Controls.Add(lblDevices);
            Controls.Add(checkedListBoxDevices);
            Controls.Add(lblDefaultDevice);
            Controls.Add(cmbDefaultDevice);
            Controls.Add(lblHotkey);
            Controls.Add(txtHotkey);
            Controls.Add(btnClearHotkey);
            Controls.Add(chkAutorun);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSettings";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDevices;
        private CheckedListBox checkedListBoxDevices;
        private Label lblDefaultDevice;
        private ComboBox cmbDefaultDevice;
        private Label lblHotkey;
        private TextBox txtHotkey;
        private Button btnClearHotkey;
        private CheckBox chkAutorun;
        private Button btnSave;
        private Button btnCancel;
    }
}
