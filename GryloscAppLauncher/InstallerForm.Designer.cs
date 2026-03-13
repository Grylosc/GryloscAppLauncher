namespace GryloscAppLauncher
{
    partial class InstallerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RepoListBox = new ListBox();
            AddSoftwareButton = new Button();
            IconBox = new PictureBox();
            SoftTitle = new LinkLabel();
            StatusText = new Label();
            ((System.ComponentModel.ISupportInitialize)IconBox).BeginInit();
            SuspendLayout();
            // 
            // RepoListBox
            // 
            RepoListBox.Enabled = false;
            RepoListBox.Font = new Font("Yu Gothic UI", 15F);
            RepoListBox.FormattingEnabled = true;
            RepoListBox.ItemHeight = 28;
            RepoListBox.Location = new Point(12, 12);
            RepoListBox.Name = "RepoListBox";
            RepoListBox.Size = new Size(409, 424);
            RepoListBox.TabIndex = 2;
            RepoListBox.SelectedIndexChanged += RepoListBox_SelectedIndexChanged;
            // 
            // AddSoftwareButton
            // 
            AddSoftwareButton.Enabled = false;
            AddSoftwareButton.Font = new Font("Yu Gothic UI", 13F);
            AddSoftwareButton.Location = new Point(546, 388);
            AddSoftwareButton.Name = "AddSoftwareButton";
            AddSoftwareButton.Size = new Size(152, 48);
            AddSoftwareButton.TabIndex = 3;
            AddSoftwareButton.Text = "インストール";
            AddSoftwareButton.UseVisualStyleBackColor = true;
            AddSoftwareButton.Click += AddSoftwareButton_Click;
            // 
            // IconBox
            // 
            IconBox.Location = new Point(427, 12);
            IconBox.Name = "IconBox";
            IconBox.Size = new Size(150, 150);
            IconBox.SizeMode = PictureBoxSizeMode.StretchImage;
            IconBox.TabIndex = 4;
            IconBox.TabStop = false;
            // 
            // SoftTitle
            // 
            SoftTitle.Enabled = false;
            SoftTitle.Font = new Font("Yu Gothic UI", 15F);
            SoftTitle.Location = new Point(583, 12);
            SoftTitle.Name = "SoftTitle";
            SoftTitle.Size = new Size(205, 150);
            SoftTitle.TabIndex = 5;
            SoftTitle.TabStop = true;
            SoftTitle.Text = "Not Selected";
            SoftTitle.LinkClicked += SoftTitle_LinkClicked;
            // 
            // StatusText
            // 
            StatusText.Font = new Font("Yu Gothic UI", 15F);
            StatusText.Location = new Point(427, 353);
            StatusText.Name = "StatusText";
            StatusText.Size = new Size(361, 32);
            StatusText.TabIndex = 6;
            StatusText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // InstallerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(StatusText);
            Controls.Add(SoftTitle);
            Controls.Add(IconBox);
            Controls.Add(AddSoftwareButton);
            Controls.Add(RepoListBox);
            Name = "InstallerForm";
            Text = "GryloscAppLauncher";
            Shown += InstallerForm_Shown;
            ((System.ComponentModel.ISupportInitialize)IconBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox RepoListBox;
        private Button AddSoftwareButton;
        private PictureBox IconBox;
        private LinkLabel SoftTitle;
        private Label StatusText;
    }
}