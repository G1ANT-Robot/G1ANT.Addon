using System;
using System.Windows.Forms;
using G1ANT.Language;
using System.Windows.Automation;
using System.Text;
using System.Drawing;

namespace G1ANT.Addon.UI.Panels
{
    [PanelAttribute(Name = "Windows Tree", DockingSide = DockingSide.Right, InitialAppear = false, Width = 400)]
    public partial class UIControlsPanel : RobotPanel
    {
        private Form rectangleForm; 

        public UIControlsPanel()
        {
            InitializeComponent();
            ControlType.Button.GetType();
        }

        public override void Initialize(IMainForm mainForm)
        {
            base.Initialize(mainForm);
            InitRootElement();
        }

        public override void RefreshContent()
        {
        }

        private void InitRootElement()
        {
            controlsTree.Nodes.Clear();
            var root = AutomationElement.RootElement;
            var rootNode = controlsTree.Nodes.Add(root?.Current.Name);
            rootNode.Tag = root;
            rootNode.Nodes.Add("");
            rootNode.Expand();
        }

        private void controlsTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {

        }

        private string CutControlType(string name)
        {
            return string.IsNullOrEmpty(name) ? "" : name.Replace("ControlType.", "");
        }

        private string GetTreeNodeName(AutomationElement element)
        {
            if (element == null)
                return "";
            string id = "";
            if (string.IsNullOrWhiteSpace(element.Current.AutomationId) == false)
                id = $" #{element.Current.AutomationId}";
            return $"{CutControlType(element.Current.ControlType.ProgrammaticName)}{id} \"{element.Current.Name}\"";
        }

        private string GetTreeNodeTooltip(AutomationElement element, int index)
        {
            if (element == null)
                return null;
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(element.Current.AutomationId))
                result.AppendLine($"id: {element.Current.AutomationId}");
            if (element.Current.ControlType != null)
            {
                result.AppendLine($"type: {CutControlType(element.Current.ControlType.ProgrammaticName)}");
                result.AppendLine($"typeid: {element.Current.ControlType.Id}");
            }
            if (!string.IsNullOrWhiteSpace(element.Current.ClassName))
                result.AppendLine($"class: {element.Current.ClassName}");
            if (!string.IsNullOrWhiteSpace(element.Current.Name))
                result.AppendLine($"name: {element.Current.Name}");
            result.AppendLine($"control index: {index}");
            return result.ToString();
        }

        private void controlsTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            try
            {
                if (e.Node.Tag is AutomationElement element)
                {
                    AutomationElement elem = TreeWalker.ControlViewWalker.GetFirstChild(element);
                    int i = 0;
                    while (elem != null)
                    {
                        var node = e.Node.Nodes.Add(GetTreeNodeName(elem));
                        node.ToolTipText = GetTreeNodeTooltip(elem, i++);
                        node.Tag = elem;
                        node.Nodes.Add("");

                        elem = TreeWalker.ControlViewWalker.GetNextSibling(elem);
                    }
                }
            }
            catch
            { }
        }

        private void InsertWPathIntoScript()
        {
            try
            {
                if (controlsTree.SelectedNode != null)
                {
                    if (controlsTree.SelectedNode.Tag is AutomationElement automationElement)
                    {
                        UIElement uiELement = new UIElement(automationElement);
                        MainForm.InsertTextIntoCurrentEditor($"{SpecialChars.Text}{uiELement.ToWPath().ToString()}{SpecialChars.Text}");
                    }
                }
            }
            catch
            { }
        }

        private void controlsTree_DoubleClick(object sender, EventArgs e)
        {
            InsertWPathIntoScript();
        }

        private void insertWPathButton_Click(object sender, EventArgs e)
        {
            InsertWPathIntoScript();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            InitRootElement();
        }

        #region RectangleForm

        /// <summary>
        /// Retrieves the top-level window that contains the specified UI Automation element.
        /// </summary>
        /// <param name="element">The contained element.</param>
        /// <returns>The containing top-level window element.</returns>
        private AutomationElement GetTopLevelWindow(AutomationElement element)
        {
            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement elementParent;
            AutomationElement node = element;
            do
            {
                elementParent = walker.GetParent(node);
                if (elementParent == AutomationElement.RootElement) break;
                node = elementParent;
            }
            while (true);
            return node;
        }

        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (controlsTree.SelectedNode != null)
                {
                    if (controlsTree.SelectedNode.Tag is AutomationElement automationElement)
                    {
                        UIElement uiELement = new UIElement(automationElement);
                        var element = UIElement.FromWPath(uiELement.ToWPath());
                        if (element != null)
                        {
                            var window = GetTopLevelWindow(automationElement);
                            if (window != null)
                            {
                                var iHandle = new IntPtr(window.Current.NativeWindowHandle);
                                if (iHandle != IntPtr.Zero)
                                {
                                    RobotWin32.BringWindowToFront(iHandle);
                                }
                            }
                            var rect = element.GetRectangle();
                            if (rect != null) { initializeRectangleForm(rect); }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RobotMessageBox.Show(ex.Message,"Error");
            }
        }
        
        private Timer blinkTimer;
        private void ClosingTimer_Tick(object sender, EventArgs e)
        {
            closingTimer.Enabled = false;
            blinkTimer.Enabled = false;
            rectangleForm.Close();
        }

        private void initializeRectangleForm(System.Windows.Rect rect)
        {
            rectangleForm = new Form();
            Panel transparentPanel = new Panel();
            transparentPanel.BackColor = Color.Pink;
            transparentPanel.Location = new Point(3, 3);
            transparentPanel.Padding = new System.Windows.Forms.Padding(30);
            transparentPanel.Parent = rectangleForm;
            rectangleForm.Controls.Add(transparentPanel);
            rectangleForm.TransparencyKey = Color.Pink;
            rectangleForm.BackColor = Color.Red;
            rectangleForm.ForeColor = Color.Red;
            rectangleForm.TopMost = true;
            rectangleForm.FormBorderStyle = FormBorderStyle.None;
            rectangleForm.ControlBox = false;
            rectangleForm.Text = string.Empty;
            rectangleForm.StartPosition = FormStartPosition.Manual;
            rectangleForm.MinimumSize = new Size(10, 10);
            rectangleForm.Location = new Point((int)rect.Left, (int)rect.Top);
            rectangleForm.Size = new Size((int)(rect.Right - rect.Left), (int)(rect.Bottom - rect.Top));

            transparentPanel.Size = new Size(rectangleForm.Size.Width - 6, rectangleForm.Size.Height - 6);
            rectangleForm.Shown += RectangleForm_Shown;
            rectangleForm.Show();
        }

        private Timer closingTimer;
        private void RectangleForm_Shown(object sender, EventArgs e)
        {
            blinkTimer = new Timer();
            blinkTimer.Interval = 300;
            blinkTimer.Tick -= BlinkTimer_Tick;
            blinkTimer.Tick += BlinkTimer_Tick;
            blinkTimer.Enabled = true;

            closingTimer = new Timer();
            closingTimer.Interval = 3000;
            closingTimer.Tick -= ClosingTimer_Tick;
            closingTimer.Tick += ClosingTimer_Tick;
            closingTimer.Enabled = true;
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            rectangleForm.Visible = !rectangleForm.Visible;
        }

        private void controlsTree_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
        #endregion

        private void controlsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            controlsTree.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show(this, new Point(e.X, e.Y));
            }
        }
    }
}
