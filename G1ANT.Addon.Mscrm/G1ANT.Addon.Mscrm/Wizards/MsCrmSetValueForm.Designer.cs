namespace G1ANT.Addon.Mscrm
{
    partial class MsCrmSetValueForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsCrmSetValueForm));
            this.FieldSearchingPhrase = new System.Windows.Forms.Label();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.LabelAction = new System.Windows.Forms.Label();
            this.ActionComboBox = new System.Windows.Forms.ComboBox();
            this.AddCommandButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ValueComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ValidationErrorCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ValidationErrorMsgTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FieldIdLabel
            // 
            this.FieldSearchingPhrase.AutoSize = true;
            this.FieldSearchingPhrase.Location = new System.Drawing.Point(12, 9);
            this.FieldSearchingPhrase.Name = "FieldIdLabel";
            this.FieldSearchingPhrase.Size = new System.Drawing.Size(46, 13);
            this.FieldSearchingPhrase.TabIndex = 10;
            this.FieldSearchingPhrase.Text = "Field ID:";
            // 
            // ValueLabel
            // 
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Location = new System.Drawing.Point(13, 65);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(37, 13);
            this.ValueLabel.TabIndex = 9;
            this.ValueLabel.Text = "Value:";
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(56, 62);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(164, 20);
            this.ValueTextBox.TabIndex = 0;
            // 
            // LabelAction
            // 
            this.LabelAction.AutoSize = true;
            this.LabelAction.Location = new System.Drawing.Point(12, 38);
            this.LabelAction.Name = "LabelAction";
            this.LabelAction.Size = new System.Drawing.Size(40, 13);
            this.LabelAction.TabIndex = 11;
            this.LabelAction.Text = "Action:";
            // 
            // ActionComboBox
            // 
            this.ActionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionComboBox.FormattingEnabled = true;
            this.ActionComboBox.Items.AddRange(new object[] {
            "SetValue",
            "Click"});
            this.ActionComboBox.Location = new System.Drawing.Point(55, 34);
            this.ActionComboBox.Name = "ActionComboBox";
            this.ActionComboBox.Size = new System.Drawing.Size(121, 21);
            this.ActionComboBox.TabIndex = 4;
            this.ActionComboBox.SelectedIndexChanged += new System.EventHandler(this.ActionComboBox_SelectedIndexChanged);
            // 
            // AddCommandButton
            // 
            this.AddCommandButton.Location = new System.Drawing.Point(12, 149);
            this.AddCommandButton.Name = "AddCommandButton";
            this.AddCommandButton.Size = new System.Drawing.Size(187, 23);
            this.AddCommandButton.TabIndex = 1;
            this.AddCommandButton.Text = "Add command";
            this.AddCommandButton.UseVisualStyleBackColor = true;
            this.AddCommandButton.Click += new System.EventHandler(this.AddCommandButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(205, 149);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(122, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ValueComboBox
            // 
            this.ValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ValueComboBox.FormattingEnabled = true;
            this.ValueComboBox.Location = new System.Drawing.Point(55, 62);
            this.ValueComboBox.Name = "ValueComboBox";
            this.ValueComboBox.Size = new System.Drawing.Size(165, 21);
            this.ValueComboBox.TabIndex = 12;
            this.ValueComboBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Handle validation error:";
            // 
            // ValidationErrorCheckBox
            // 
            this.ValidationErrorCheckBox.AutoSize = true;
            this.ValidationErrorCheckBox.Location = new System.Drawing.Point(135, 95);
            this.ValidationErrorCheckBox.Name = "ValidationErrorCheckBox";
            this.ValidationErrorCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ValidationErrorCheckBox.TabIndex = 14;
            this.ValidationErrorCheckBox.UseVisualStyleBackColor = true;
            this.ValidationErrorCheckBox.CheckedChanged += new System.EventHandler(this.ValidationErrorCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Validation error message:";
            // 
            // ValidationErrorMsgTextBox
            // 
            this.ValidationErrorMsgTextBox.Location = new System.Drawing.Point(144, 118);
            this.ValidationErrorMsgTextBox.Name = "ValidationErrorMsgTextBox";
            this.ValidationErrorMsgTextBox.ReadOnly = true;
            this.ValidationErrorMsgTextBox.Size = new System.Drawing.Size(186, 20);
            this.ValidationErrorMsgTextBox.TabIndex = 16;
            this.ValidationErrorMsgTextBox.LocationChanged += new System.EventHandler(this.ValidationErrorMsgTextBox_LocationChanged);
            // 
            // MsCrmSetValueForm
            // 
            this.AcceptButton = this.AddCommandButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 184);
            this.Controls.Add(this.ValidationErrorMsgTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ValidationErrorCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ValueComboBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AddCommandButton);
            this.Controls.Add(this.ActionComboBox);
            this.Controls.Add(this.LabelAction);
            this.Controls.Add(this.ValueTextBox);
            this.Controls.Add(this.ValueLabel);
            this.Controls.Add(this.FieldSearchingPhrase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MsCrmSetValueForm";
            this.Text = "Dynamics CRM SetValue Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MsCrmSetValueForm_FormClosing);
            this.Shown += new System.EventHandler(this.MsCrmSetValueForm_Shown);
            this.LocationChanged += new System.EventHandler(this.MsCrmSetValueForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FieldSearchingPhrase;
        private System.Windows.Forms.Label ValueLabel;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.Label LabelAction;
        private System.Windows.Forms.ComboBox ActionComboBox;
        private System.Windows.Forms.Button AddCommandButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox ValueComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ValidationErrorCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ValidationErrorMsgTextBox;
    }
}