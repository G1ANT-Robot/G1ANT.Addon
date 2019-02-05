namespace G1ANT.Addon.UI
{
    partial class UIControlsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIControlsPanel));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.controlsTree = new System.Windows.Forms.TreeView();
            this.insertWPathButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.highliteControlButton = new System.Windows.Forms.ToolStripButton();
            this.setRootButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearRootButton = new System.Windows.Forms.ToolStripButton();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertWPathButton,
            this.toolStripSeparator1,
            this.refreshButton,
            this.setRootButton,
            this.clearRootButton,
            this.toolStripSeparator2,
            this.highliteControlButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(263, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // controlsTree
            // 
            this.controlsTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlsTree.Location = new System.Drawing.Point(0, 28);
            this.controlsTree.Name = "controlsTree";
            this.controlsTree.Size = new System.Drawing.Size(263, 302);
            this.controlsTree.TabIndex = 1;
            this.controlsTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.controlsTree_AfterCollapse);
            this.controlsTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.controlsTree_BeforeExpand);
            this.controlsTree.DoubleClick += new System.EventHandler(this.controlsTree_DoubleClick);
            // 
            // insertWPathButton
            // 
            this.insertWPathButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertWPathButton.Image = ((System.Drawing.Image)(resources.GetObject("insertWPathButton.Image")));
            this.insertWPathButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertWPathButton.Name = "insertWPathButton";
            this.insertWPathButton.Size = new System.Drawing.Size(23, 22);
            this.insertWPathButton.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // highliteControlButton
            // 
            this.highliteControlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.highliteControlButton.Image = ((System.Drawing.Image)(resources.GetObject("highliteControlButton.Image")));
            this.highliteControlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.highliteControlButton.Name = "highliteControlButton";
            this.highliteControlButton.Size = new System.Drawing.Size(23, 22);
            this.highliteControlButton.Text = "toolStripButton1";
            // 
            // setRootButton
            // 
            this.setRootButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.setRootButton.Image = ((System.Drawing.Image)(resources.GetObject("setRootButton.Image")));
            this.setRootButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setRootButton.Name = "setRootButton";
            this.setRootButton.Size = new System.Drawing.Size(23, 22);
            this.setRootButton.Text = "toolStripButton1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // clearRootButton
            // 
            this.clearRootButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearRootButton.Image = ((System.Drawing.Image)(resources.GetObject("clearRootButton.Image")));
            this.clearRootButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearRootButton.Name = "clearRootButton";
            this.clearRootButton.Size = new System.Drawing.Size(23, 22);
            this.clearRootButton.Text = "toolStripButton1";
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "toolStripButton1";
            // 
            // UIControlsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlsTree);
            this.Controls.Add(this.toolStrip);
            this.Name = "UIControlsPanel";
            this.Size = new System.Drawing.Size(263, 472);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton insertWPathButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton setRootButton;
        private System.Windows.Forms.ToolStripButton clearRootButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton highliteControlButton;
        private System.Windows.Forms.TreeView controlsTree;
        private System.Windows.Forms.ToolStripButton refreshButton;
    }
}
