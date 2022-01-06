namespace EliteDangerousMacroDeckPlugin.GUI
{
    partial class PluginConfig
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
            this.EliteDangerousLogo = new System.Windows.Forms.PictureBox();
            this.CreateProfile = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.CreateProfileDescription = new System.Windows.Forms.Label();
            this.PluginGitHubLink = new System.Windows.Forms.LinkLabel();
            this.EliteJournalReaderLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.InputSimulatorStandardLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.EliteDangerousLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // EliteDangerousLogo
            // 
            this.EliteDangerousLogo.Image = global::EliteDangerousMacroDeckPlugin.Properties.Resources.elite_dangerous_logo;
            this.EliteDangerousLogo.Location = new System.Drawing.Point(4, 4);
            this.EliteDangerousLogo.Name = "EliteDangerousLogo";
            this.EliteDangerousLogo.Size = new System.Drawing.Size(190, 164);
            this.EliteDangerousLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EliteDangerousLogo.TabIndex = 0;
            this.EliteDangerousLogo.TabStop = false;
            // 
            // CreateProfile
            // 
            this.CreateProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.CreateProfile.BorderRadius = 8;
            this.CreateProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateProfile.FlatAppearance.BorderSize = 0;
            this.CreateProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateProfile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CreateProfile.ForeColor = System.Drawing.Color.White;
            this.CreateProfile.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(184)))));
            this.CreateProfile.Icon = null;
            this.CreateProfile.Location = new System.Drawing.Point(200, 4);
            this.CreateProfile.Name = "CreateProfile";
            this.CreateProfile.Progress = 0;
            this.CreateProfile.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(94)))));
            this.CreateProfile.Size = new System.Drawing.Size(207, 40);
            this.CreateProfile.TabIndex = 8;
            this.CreateProfile.Text = "Create \"Elite Dangerous\" Profile";
            this.CreateProfile.UseVisualStyleBackColor = false;
            // 
            // CreateProfileDescription
            // 
            this.CreateProfileDescription.Location = new System.Drawing.Point(217, 47);
            this.CreateProfileDescription.Name = "CreateProfileDescription";
            this.CreateProfileDescription.Size = new System.Drawing.Size(190, 41);
            this.CreateProfileDescription.TabIndex = 9;
            this.CreateProfileDescription.Text = "Creates a new profile with a set of default buttons.";
            // 
            // PluginGitHubLink
            // 
            this.PluginGitHubLink.AutoSize = true;
            this.PluginGitHubLink.LinkColor = System.Drawing.Color.Gray;
            this.PluginGitHubLink.Location = new System.Drawing.Point(200, 88);
            this.PluginGitHubLink.Name = "PluginGitHubLink";
            this.PluginGitHubLink.Size = new System.Drawing.Size(144, 16);
            this.PluginGitHubLink.TabIndex = 10;
            this.PluginGitHubLink.TabStop = true;
            this.PluginGitHubLink.Text = "Plugin Source on GitHub";
            this.PluginGitHubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PluginLink_LinkClicked);
            // 
            // EliteJournalReaderLink
            // 
            this.EliteJournalReaderLink.AutoSize = true;
            this.EliteJournalReaderLink.LinkColor = System.Drawing.Color.Gray;
            this.EliteJournalReaderLink.Location = new System.Drawing.Point(217, 134);
            this.EliteJournalReaderLink.Name = "EliteJournalReaderLink";
            this.EliteJournalReaderLink.Size = new System.Drawing.Size(113, 16);
            this.EliteJournalReaderLink.TabIndex = 11;
            this.EliteJournalReaderLink.TabStop = true;
            this.EliteJournalReaderLink.Text = "EliteJournalReader";
            this.EliteJournalReaderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EliteJournalReaderLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Thanks To:";
            // 
            // InputSimulatorStandardLink
            // 
            this.InputSimulatorStandardLink.AutoSize = true;
            this.InputSimulatorStandardLink.LinkColor = System.Drawing.Color.Gray;
            this.InputSimulatorStandardLink.Location = new System.Drawing.Point(217, 150);
            this.InputSimulatorStandardLink.Name = "InputSimulatorStandardLink";
            this.InputSimulatorStandardLink.Size = new System.Drawing.Size(143, 16);
            this.InputSimulatorStandardLink.TabIndex = 13;
            this.InputSimulatorStandardLink.Text = "InputSimulatorStandard";
            this.InputSimulatorStandardLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.InputSimulatorStandard_LinkClicked);
            // 
            // PluginConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(457, 175);
            this.Controls.Add(this.InputSimulatorStandardLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EliteJournalReaderLink);
            this.Controls.Add(this.PluginGitHubLink);
            this.Controls.Add(this.CreateProfileDescription);
            this.Controls.Add(this.CreateProfile);
            this.Controls.Add(this.EliteDangerousLogo);
            this.Name = "PluginConfig";
            this.Text = "Elite Dangerous Plugin";
            this.Controls.SetChildIndex(this.EliteDangerousLogo, 0);
            this.Controls.SetChildIndex(this.CreateProfile, 0);
            this.Controls.SetChildIndex(this.CreateProfileDescription, 0);
            this.Controls.SetChildIndex(this.PluginGitHubLink, 0);
            this.Controls.SetChildIndex(this.EliteJournalReaderLink, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.InputSimulatorStandardLink, 0);
            ((System.ComponentModel.ISupportInitialize)(this.EliteDangerousLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox eliteDangerousLogo;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary CreateProfile;
        private System.Windows.Forms.Label CreateProfileDescription;
        private System.Windows.Forms.PictureBox EliteDangerousLogo;
        private System.Windows.Forms.LinkLabel PluginGitHubLink;
        private System.Windows.Forms.LinkLabel EliteJournalReaderLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel InputSimulatorStandardLink;
    }
}