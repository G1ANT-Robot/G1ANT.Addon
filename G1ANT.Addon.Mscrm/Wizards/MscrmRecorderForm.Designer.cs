namespace G1ANT.Addon.Mscrm
{
    partial class MscrmRecorderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MscrmRecorderForm));
            this.StartRecordingButton = new System.Windows.Forms.Button();
            this.RecordedScriptRichTextBox = new System.Windows.Forms.RichTextBox();
            this.UrlPhraseTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.InsertScriptToMainWindow = new System.Windows.Forms.Button();
            this.AddMscrmAttachIntroCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // StartRecordingButton
            // 
            this.StartRecordingButton.Location = new System.Drawing.Point(294, 9);
            this.StartRecordingButton.Name = "StartRecordingButton";
            this.StartRecordingButton.Size = new System.Drawing.Size(104, 23);
            this.StartRecordingButton.TabIndex = 0;
            this.StartRecordingButton.Text = "Start recording";
            this.StartRecordingButton.UseVisualStyleBackColor = true;
            this.StartRecordingButton.Click += new System.EventHandler(this.StartRecordingButton_Click);
            // 
            // RecordedScriptRichTextBox
            // 
            this.RecordedScriptRichTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RecordedScriptRichTextBox.Location = new System.Drawing.Point(0, 42);
            this.RecordedScriptRichTextBox.Name = "RecordedScriptRichTextBox";
            this.RecordedScriptRichTextBox.Size = new System.Drawing.Size(891, 246);
            this.RecordedScriptRichTextBox.TabIndex = 1;
            this.RecordedScriptRichTextBox.Text = "";
            // 
            // UrlPhraseTextBox
            // 
            this.UrlPhraseTextBox.Location = new System.Drawing.Point(150, 11);
            this.UrlPhraseTextBox.Name = "UrlPhraseTextBox";
            this.UrlPhraseTextBox.Size = new System.Drawing.Size(138, 20);
            this.UrlPhraseTextBox.TabIndex = 2;
            this.UrlPhraseTextBox.Text = "g1ant.crm11.dynamics.com";
            this.UrlPhraseTextBox.TextChanged += new System.EventHandler(this.UrlPhraseTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dynamics CRM url phrase:";
            // 
            // InsertScriptToMainWindow
            // 
            this.InsertScriptToMainWindow.Location = new System.Drawing.Point(798, 9);
            this.InsertScriptToMainWindow.Name = "InsertScriptToMainWindow";
            this.InsertScriptToMainWindow.Size = new System.Drawing.Size(81, 23);
            this.InsertScriptToMainWindow.TabIndex = 4;
            this.InsertScriptToMainWindow.Text = "Insert";
            this.InsertScriptToMainWindow.UseVisualStyleBackColor = true;
            this.InsertScriptToMainWindow.Click += new System.EventHandler(this.InsertScriptToMainWindow_Click);
            // 
            // AddMscrmAttachIntroCheckBox
            // 
            this.AddMscrmAttachIntroCheckBox.AutoSize = true;
            this.AddMscrmAttachIntroCheckBox.Location = new System.Drawing.Point(588, 13);
            this.AddMscrmAttachIntroCheckBox.Name = "AddMscrmAttachIntroCheckBox";
            this.AddMscrmAttachIntroCheckBox.Size = new System.Drawing.Size(204, 17);
            this.AddMscrmAttachIntroCheckBox.TabIndex = 5;
            this.AddMscrmAttachIntroCheckBox.Text = "Add initializing commands to the script";
            this.AddMscrmAttachIntroCheckBox.UseVisualStyleBackColor = true;
            // 
            // MscrmRecorderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 288);
            this.Controls.Add(this.AddMscrmAttachIntroCheckBox);
            this.Controls.Add(this.InsertScriptToMainWindow);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UrlPhraseTextBox);
            this.Controls.Add(this.RecordedScriptRichTextBox);
            this.Controls.Add(this.StartRecordingButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MscrmRecorderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dynamics CRM Recorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MscrmRecorderForm_FormClosing);
            this.LocationChanged += new System.EventHandler(this.MscrmRecorderForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartRecordingButton;
        private System.Windows.Forms.RichTextBox RecordedScriptRichTextBox;
        private System.Windows.Forms.TextBox UrlPhraseTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button InsertScriptToMainWindow;
        private System.Windows.Forms.CheckBox AddMscrmAttachIntroCheckBox;
    }
}