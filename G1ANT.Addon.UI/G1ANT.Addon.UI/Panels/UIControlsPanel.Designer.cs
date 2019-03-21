namespace G1ANT.Addon.UI.Panels
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.insertWPathButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshButton = new System.Windows.Forms.ToolStripButton();
            this.controlsTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.highlightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertWPathButton,
            this.toolStripSeparator1,
            this.refreshButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(222, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // insertWPathButton
            // 
            this.insertWPathButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertWPathButton.Image = global::G1ANT.Addon.UI.Resources.insert_into;
            this.insertWPathButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertWPathButton.Name = "insertWPathButton";
            this.insertWPathButton.Size = new System.Drawing.Size(23, 22);
            this.insertWPathButton.Text = "Insert WPath";
            this.insertWPathButton.ToolTipText = "Insert WPath of selected control";
            this.insertWPathButton.Click += new System.EventHandler(this.insertWPathButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // refreshButton
            // 
            this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshButton.Image = global::G1ANT.Addon.UI.Resources.refresh;
            this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(23, 22);
            this.refreshButton.Text = "Refresh controls";
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // controlsTree
            // 
            this.controlsTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlsTree.Location = new System.Drawing.Point(0, 28);
            this.controlsTree.Name = "controlsTree";
            this.controlsTree.ShowNodeToolTips = true;
            this.controlsTree.Size = new System.Drawing.Size(222, 389);
            this.controlsTree.TabIndex = 1;
            this.controlsTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.controlsTree_AfterCollapse);
            this.controlsTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.controlsTree_BeforeExpand);
            this.controlsTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.controlsTree_NodeMouseClick);
            this.controlsTree.DoubleClick += new System.EventHandler(this.controlsTree_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highlightToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(125, 26);
            // 
            // highlightToolStripMenuItem
            // 
            this.highlightToolStripMenuItem.Name = "highlightToolStripMenuItem";
            this.highlightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.highlightToolStripMenuItem.Text = "Highlight";
            this.highlightToolStripMenuItem.Click += new System.EventHandler(this.highlightToolStripMenuItem_Click);
            // 
            // UIControlsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlsTree);
            this.Controls.Add(this.toolStrip);
            this.Name = "UIControlsPanel";
            this.Size = new System.Drawing.Size(222, 420);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TreeView controlsTree;
        private System.Windows.Forms.ToolStripButton insertWPathButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem highlightToolStripMenuItem;
    }
}
