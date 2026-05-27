namespace SoundSwitch
{
    partial class FormSoundSwitch
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSoundSwitch));
            comboBox1 = new ComboBox();
            btnSettings = new Button();
            contextMenuStrip = new ContextMenuStrip(components);
            optionsToolStripMenuItem = new ToolStripMenuItem();
            mostTopToolStripMenuItem = new ToolStripMenuItem();
            autorunToolStripMenuItem = new ToolStripMenuItem();
            showInTaskbarToolStripMenuItem = new ToolStripMenuItem();
            oppacityToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            instantProgressBar1 = new InstantProgressBar();
            notifyIcon = new NotifyIcon(components);
            trayContextMenuStrip = new ContextMenuStrip(components);
            traySettingToolStripMenuItem = new ToolStripMenuItem();
            trayCloseToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            trayContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.ContextMenuStrip = contextMenuStrip;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(218, 29);
            comboBox1.TabIndex = 0;
            //
            // btnSettings
            //
            btnSettings.ContextMenuStrip = contextMenuStrip;
            btnSettings.Font = new Font("Segoe UI", 9F);
            btnSettings.Location = new Point(234, 12);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(60, 29);
            btnSettings.TabIndex = 2;
            btnSettings.Text = "Setting";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            //
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem, closeToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            contextMenuStrip.Size = new Size(128, 52);
            contextMenuStrip.Opening += contextMenuStrip1_Opening;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mostTopToolStripMenuItem, autorunToolStripMenuItem, showInTaskbarToolStripMenuItem, oppacityToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(127, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // mostTopToolStripMenuItem
            // 
            mostTopToolStripMenuItem.Name = "mostTopToolStripMenuItem";
            mostTopToolStripMenuItem.Size = new Size(180, 24);
            mostTopToolStripMenuItem.Text = "Most top";
            mostTopToolStripMenuItem.Click += mostTopToolStripMenuItem_Click;
            // 
            // autorunToolStripMenuItem
            // 
            autorunToolStripMenuItem.Name = "autorunToolStripMenuItem";
            autorunToolStripMenuItem.Size = new Size(180, 24);
            autorunToolStripMenuItem.Text = "Autorun";
            autorunToolStripMenuItem.Click += autorunToolStripMenuItem_Click;
            // 
            // showInTaskbarToolStripMenuItem
            // 
            showInTaskbarToolStripMenuItem.Name = "showInTaskbarToolStripMenuItem";
            showInTaskbarToolStripMenuItem.Size = new Size(180, 24);
            showInTaskbarToolStripMenuItem.Text = "Show in taskbar";
            showInTaskbarToolStripMenuItem.Click += showInTaskbarToolStripMenuItem_Click;
            // 
            // oppacityToolStripMenuItem
            // 
            oppacityToolStripMenuItem.Name = "oppacityToolStripMenuItem";
            oppacityToolStripMenuItem.Size = new Size(180, 24);
            oppacityToolStripMenuItem.Text = "Oppacity";
            oppacityToolStripMenuItem.Click += oppacityToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(127, 24);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // instantProgressBar1
            // 
            instantProgressBar1.ContextMenuStrip = contextMenuStrip;
            instantProgressBar1.Location = new Point(12, 47);
            instantProgressBar1.Minimum = 0;
            instantProgressBar1.Name = "instantProgressBar1";
            instantProgressBar1.Size = new Size(282, 28);
            instantProgressBar1.TabIndex = 1;
            instantProgressBar1.Text = "instantProgressBar1";
            instantProgressBar1.Value = 0;
            instantProgressBar1.MouseDown += instantProgressBar1_MouseDown;
            instantProgressBar1.MouseMove += instantProgressBar1_MouseMove;
            instantProgressBar1.MouseUp += instantProgressBar1_MouseUp;
            //
            // notifyIcon
            //
            notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
            notifyIcon.Text = "SoundSwitch";
            notifyIcon.ContextMenuStrip = trayContextMenuStrip;
            notifyIcon.Visible = false;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            //
            // trayContextMenuStrip
            //
            trayContextMenuStrip.Items.AddRange(new ToolStripItem[] { traySettingToolStripMenuItem, trayCloseToolStripMenuItem });
            trayContextMenuStrip.Name = "trayContextMenuStrip";
            trayContextMenuStrip.Size = new Size(120, 52);
            //
            // traySettingToolStripMenuItem
            //
            traySettingToolStripMenuItem.Name = "traySettingToolStripMenuItem";
            traySettingToolStripMenuItem.Size = new Size(119, 24);
            traySettingToolStripMenuItem.Text = "Setting";
            traySettingToolStripMenuItem.Click += btnSettings_Click;
            //
            // trayCloseToolStripMenuItem
            //
            trayCloseToolStripMenuItem.Name = "trayCloseToolStripMenuItem";
            trayCloseToolStripMenuItem.Size = new Size(119, 24);
            trayCloseToolStripMenuItem.Text = "Exit";
            trayCloseToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            //
            // FormSoundSwitch
            //
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 83);
            ContextMenuStrip = contextMenuStrip;
            Controls.Add(instantProgressBar1);
            Controls.Add(btnSettings);
            Controls.Add(comboBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormSoundSwitch";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "SoundSwitch";
            Activated += FormSoundSwitch_Activated;
            Deactivate += FormSoundSwitch_Deactivate;
            FormClosing += FormSoundSwitch_FormClosing;
            Load += FormSoundSwitch_Load;
            contextMenuStrip.ResumeLayout(false);
            trayContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboBox1;
        private InstantProgressBar instantProgressBar1;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem mostTopToolStripMenuItem;
        private ToolStripMenuItem autorunToolStripMenuItem;
        private ToolStripMenuItem showInTaskbarToolStripMenuItem;
        private ToolStripMenuItem oppacityToolStripMenuItem;
        private Button btnSettings;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayContextMenuStrip;
        private ToolStripMenuItem traySettingToolStripMenuItem;
        private ToolStripMenuItem trayCloseToolStripMenuItem;
    }
}
