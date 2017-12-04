namespace G1ANT.Addon.Ui
{
    partial class UiWizard
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSetVal = new System.Windows.Forms.TextBox();
            this.buttonSetVal = new System.Windows.Forms.Button();
            this.buttonGetVal = new System.Windows.Forms.Button();
            this.buttonClick = new System.Windows.Forms.Button();
            this.treeViewAppStructure = new System.Windows.Forms.TreeView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxWindowName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAttach = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.checkBoxLocation = new System.Windows.Forms.CheckBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonWaitFor = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "value to set";
            // 
            // textBoxSetVal
            // 
            this.textBoxSetVal.Location = new System.Drawing.Point(77, 532);
            this.textBoxSetVal.Name = "textBoxSetVal";
            this.textBoxSetVal.Size = new System.Drawing.Size(617, 20);
            this.textBoxSetVal.TabIndex = 37;
            // 
            // buttonSetVal
            // 
            this.buttonSetVal.Location = new System.Drawing.Point(703, 530);
            this.buttonSetVal.Name = "buttonSetVal";
            this.buttonSetVal.Size = new System.Drawing.Size(75, 23);
            this.buttonSetVal.TabIndex = 34;
            this.buttonSetVal.Text = "set value";
            this.buttonSetVal.UseVisualStyleBackColor = true;
            this.buttonSetVal.Click += new System.EventHandler(this.ButtonSetVal_Click);
            // 
            // buttonGetVal
            // 
            this.buttonGetVal.Location = new System.Drawing.Point(703, 501);
            this.buttonGetVal.Name = "buttonGetVal";
            this.buttonGetVal.Size = new System.Drawing.Size(75, 23);
            this.buttonGetVal.TabIndex = 33;
            this.buttonGetVal.Text = "get value";
            this.buttonGetVal.UseVisualStyleBackColor = true;
            this.buttonGetVal.Click += new System.EventHandler(this.ButtonGetVal_Click);
            // 
            // buttonClick
            // 
            this.buttonClick.Location = new System.Drawing.Point(703, 472);
            this.buttonClick.Name = "buttonClick";
            this.buttonClick.Size = new System.Drawing.Size(75, 23);
            this.buttonClick.TabIndex = 32;
            this.buttonClick.Text = "click";
            this.buttonClick.UseVisualStyleBackColor = true;
            this.buttonClick.Click += new System.EventHandler(this.ButtonClick_Click);
            // 
            // treeViewAppStructure
            // 
            this.treeViewAppStructure.Location = new System.Drawing.Point(12, 32);
            this.treeViewAppStructure.Name = "treeViewAppStructure";
            this.treeViewAppStructure.Size = new System.Drawing.Size(766, 270);
            this.treeViewAppStructure.TabIndex = 29;
            this.treeViewAppStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewAppStructure_AfterSelect);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 337);
            this.richTextBox1.MaximumSize = new System.Drawing.Size(700, 200);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(682, 189);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // textBoxWindowName
            // 
            this.textBoxWindowName.Enabled = false;
            this.textBoxWindowName.Location = new System.Drawing.Point(336, 7);
            this.textBoxWindowName.Name = "textBoxWindowName";
            this.textBoxWindowName.Size = new System.Drawing.Size(271, 20);
            this.textBoxWindowName.TabIndex = 39;
            this.textBoxWindowName.TextChanged += new System.EventHandler(this.textBoxWindowName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Window Name";
            // 
            // buttonAttach
            // 
            this.buttonAttach.Location = new System.Drawing.Point(700, 337);
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.Size = new System.Drawing.Size(75, 23);
            this.buttonAttach.TabIndex = 41;
            this.buttonAttach.Text = "Add attach";
            this.buttonAttach.UseVisualStyleBackColor = true;
            this.buttonAttach.Click += new System.EventHandler(this.ButtonAttach_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Chose Control";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(12, 308);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonSelect.TabIndex = 43;
            this.buttonSelect.Text = "Select App";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // checkBoxLocation
            // 
            this.checkBoxLocation.AutoSize = true;
            this.checkBoxLocation.Checked = true;
            this.checkBoxLocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLocation.Location = new System.Drawing.Point(643, 9);
            this.checkBoxLocation.Name = "checkBoxLocation";
            this.checkBoxLocation.Size = new System.Drawing.Size(135, 17);
            this.checkBoxLocation.TabIndex = 44;
            this.checkBoxLocation.Text = "Show real time location";
            this.checkBoxLocation.UseVisualStyleBackColor = true;
            this.checkBoxLocation.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(93, 308);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 45;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonWaitFor
            // 
            this.buttonWaitFor.Location = new System.Drawing.Point(700, 366);
            this.buttonWaitFor.Name = "buttonWaitFor";
            this.buttonWaitFor.Size = new System.Drawing.Size(75, 23);
            this.buttonWaitFor.TabIndex = 46;
            this.buttonWaitFor.Text = "wait for...";
            this.buttonWaitFor.UseVisualStyleBackColor = true;
            this.buttonWaitFor.Click += new System.EventHandler(this.buttonWaitFor_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(195, 312);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 17);
            this.checkBox1.TabIndex = 48;
            this.checkBox1.Text = "Ignore id";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(700, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 49;
            this.button1.Text = "Get position";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // UiWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 567);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonWaitFor);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.checkBoxLocation);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonAttach);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxWindowName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSetVal);
            this.Controls.Add(this.buttonSetVal);
            this.Controls.Add(this.buttonGetVal);
            this.Controls.Add(this.buttonClick);
            this.Controls.Add(this.treeViewAppStructure);
            this.Controls.Add(this.richTextBox1);
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.Name = "UiWizard";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "G1ant Ui Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UiWizard_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UiWizard_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSetVal;
        private System.Windows.Forms.Button buttonSetVal;
        private System.Windows.Forms.Button buttonGetVal;
        private System.Windows.Forms.Button buttonClick;
        private System.Windows.Forms.TreeView treeViewAppStructure;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSelect;
        public System.Windows.Forms.TextBox textBoxWindowName;
        private System.Windows.Forms.CheckBox checkBoxLocation;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonWaitFor;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
    }
}