namespace GryloscAppLauncher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            SoftListBox = new ListBox();
            AddSoftwareButton = new Button();
            UninstallButton = new Button();
            LaunchButton = new Button();
            OpenDireButton = new Button();
            menuStrip1 = new MenuStrip();
            SettingTSMI = new ToolStripMenuItem();
            ResetTSMI = new ToolStripMenuItem();
            SoftTitle = new LinkLabel();
            IconBox = new PictureBox();
            SearchBox = new TextBox();
            label1 = new Label();
            UpdateButton = new Button();
            VersionText = new Label();
            StatusText = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)IconBox).BeginInit();
            SuspendLayout();
            // 
            // SoftListBox
            // 
            SoftListBox.Enabled = false;
            SoftListBox.Font = new Font("Yu Gothic UI", 15F);
            SoftListBox.FormattingEnabled = true;
            SoftListBox.ItemHeight = 28;
            SoftListBox.Location = new Point(12, 40);
            SoftListBox.Name = "SoftListBox";
            SoftListBox.Size = new Size(409, 396);
            SoftListBox.TabIndex = 0;
            SoftListBox.SelectedIndexChanged += SoftListBox_SelectedIndexChanged;
            // 
            // AddSoftwareButton
            // 
            AddSoftwareButton.Font = new Font("Yu Gothic UI", 13F);
            AddSoftwareButton.Location = new Point(587, 388);
            AddSoftwareButton.Name = "AddSoftwareButton";
            AddSoftwareButton.Size = new Size(152, 48);
            AddSoftwareButton.TabIndex = 1;
            AddSoftwareButton.Text = "新規インストール";
            AddSoftwareButton.UseVisualStyleBackColor = true;
            AddSoftwareButton.Click += AddSoftwareButton_Click;
            // 
            // UninstallButton
            // 
            UninstallButton.Enabled = false;
            UninstallButton.Font = new Font("Yu Gothic UI", 13F);
            UninstallButton.Location = new Point(587, 334);
            UninstallButton.Name = "UninstallButton";
            UninstallButton.Size = new Size(152, 48);
            UninstallButton.TabIndex = 2;
            UninstallButton.Text = "アンインストール";
            UninstallButton.UseVisualStyleBackColor = true;
            UninstallButton.Click += UninstallButton_Click;
            // 
            // LaunchButton
            // 
            LaunchButton.Enabled = false;
            LaunchButton.Font = new Font("Yu Gothic UI", 13F);
            LaunchButton.Location = new Point(427, 334);
            LaunchButton.Name = "LaunchButton";
            LaunchButton.Size = new Size(152, 48);
            LaunchButton.TabIndex = 3;
            LaunchButton.Text = "起動";
            LaunchButton.UseVisualStyleBackColor = true;
            LaunchButton.Click += LaunchButton_Click;
            // 
            // OpenDireButton
            // 
            OpenDireButton.Font = new Font("Yu Gothic UI", 13F);
            OpenDireButton.Location = new Point(427, 388);
            OpenDireButton.Name = "OpenDireButton";
            OpenDireButton.Size = new Size(152, 48);
            OpenDireButton.TabIndex = 4;
            OpenDireButton.Text = "参照";
            OpenDireButton.UseVisualStyleBackColor = true;
            OpenDireButton.Click += OpenDireButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { SettingTSMI });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(751, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // SettingTSMI
            // 
            SettingTSMI.DropDownItems.AddRange(new ToolStripItem[] { ResetTSMI });
            SettingTSMI.Name = "SettingTSMI";
            SettingTSMI.Size = new Size(43, 20);
            SettingTSMI.Text = "設定";
            // 
            // ResetTSMI
            // 
            ResetTSMI.Name = "ResetTSMI";
            ResetTSMI.Size = new Size(108, 22);
            ResetTSMI.Text = "リセット";
            ResetTSMI.Click += ResetTSMI_Click;
            // 
            // SoftTitle
            // 
            SoftTitle.Enabled = false;
            SoftTitle.Font = new Font("Yu Gothic UI", 15F);
            SoftTitle.Location = new Point(537, 40);
            SoftTitle.Name = "SoftTitle";
            SoftTitle.Size = new Size(205, 64);
            SoftTitle.TabIndex = 7;
            SoftTitle.TabStop = true;
            SoftTitle.Text = "Not Selected";
            SoftTitle.LinkClicked += SoftTitle_LinkClicked;
            // 
            // IconBox
            // 
            IconBox.Location = new Point(427, 40);
            IconBox.Name = "IconBox";
            IconBox.Size = new Size(100, 100);
            IconBox.SizeMode = PictureBoxSizeMode.StretchImage;
            IconBox.TabIndex = 6;
            IconBox.TabStop = false;
            // 
            // SearchBox
            // 
            SearchBox.Enabled = false;
            SearchBox.Font = new Font("Yu Gothic UI", 15F);
            SearchBox.Location = new Point(427, 294);
            SearchBox.Name = "SearchBox";
            SearchBox.Size = new Size(312, 34);
            SearchBox.TabIndex = 8;
            SearchBox.TextChanged += SearchBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 15F);
            label1.Location = new Point(427, 263);
            label1.Name = "label1";
            label1.Size = new Size(52, 28);
            label1.TabIndex = 9;
            label1.Text = "検索";
            // 
            // UpdateButton
            // 
            UpdateButton.Enabled = false;
            UpdateButton.Location = new Point(664, 143);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(75, 23);
            UpdateButton.TabIndex = 10;
            UpdateButton.Text = "更新";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Visible = false;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // VersionText
            // 
            VersionText.Font = new Font("Yu Gothic UI", 13F);
            VersionText.Location = new Point(537, 114);
            VersionText.Name = "VersionText";
            VersionText.Size = new Size(205, 26);
            VersionText.TabIndex = 11;
            VersionText.Text = "Not Selected";
            VersionText.TextAlign = ContentAlignment.MiddleRight;
            VersionText.UseMnemonic = false;
            // 
            // StatusText
            // 
            StatusText.Font = new Font("Yu Gothic UI", 15F);
            StatusText.Location = new Point(427, 231);
            StatusText.Name = "StatusText";
            StatusText.Size = new Size(312, 32);
            StatusText.TabIndex = 12;
            StatusText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(751, 450);
            Controls.Add(StatusText);
            Controls.Add(VersionText);
            Controls.Add(UpdateButton);
            Controls.Add(label1);
            Controls.Add(SearchBox);
            Controls.Add(SoftTitle);
            Controls.Add(IconBox);
            Controls.Add(OpenDireButton);
            Controls.Add(LaunchButton);
            Controls.Add(UninstallButton);
            Controls.Add(AddSoftwareButton);
            Controls.Add(SoftListBox);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "GryloscAppLauncher";
            Shown += Form1_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)IconBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox SoftListBox;
        private Button AddSoftwareButton;
        private Button UninstallButton;
        private Button LaunchButton;
        private Button OpenDireButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem SettingTSMI;
        private ToolStripMenuItem ResetTSMI;
        private LinkLabel SoftTitle;
        private PictureBox IconBox;
        private TextBox SearchBox;
        private Label label1;
        private Button UpdateButton;
        private Label VersionText;
        private Label StatusText;
    }
}
