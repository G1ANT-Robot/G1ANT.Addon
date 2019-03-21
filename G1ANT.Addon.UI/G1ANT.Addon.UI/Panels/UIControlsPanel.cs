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
        private Form blinkingRectForm; 

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

        private AutomationElement GetTopLevelWindow(AutomationElement element)
        {
            AutomationElement elementParent = TreeWalker.ControlViewWalker.GetParent(element);
            return elementParent == AutomationElement.RootElement ? element : GetTopLevelWindow(elementParent);
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
                                    var rect = element.GetRectangle();
                                    if (rect != null)
                                    {
                                        InitializeRectangleForm(rect);
                                    }
                                }
                            }
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
        private int blinkTimes;
        private void InitializeRectangleForm(System.Windows.Rect rect)
        {
            blinkingRectForm = new Form();
            Panel transparentPanel = new Panel();
            transparentPanel.BackColor = Color.Pink;
            transparentPanel.Location = new Point(3, 3);
            transparentPanel.Padding = new System.Windows.Forms.Padding(30);
            transparentPanel.Parent = blinkingRectForm;
            blinkingRectForm.Controls.Add(transparentPanel);
            blinkingRectForm.ShowInTaskbar = false;
            blinkingRectForm.TransparencyKey = Color.Pink;
            blinkingRectForm.BackColor = Color.Red;
            blinkingRectForm.ForeColor = Color.Red;
            blinkingRectForm.TopMost = true;
            blinkingRectForm.FormBorderStyle = FormBorderStyle.None;
            blinkingRectForm.ControlBox = false;
            blinkingRectForm.Text = string.Empty;
            blinkingRectForm.StartPosition = FormStartPosition.Manual;
            blinkingRectForm.MinimumSize = new Size(10, 10);
            blinkingRectForm.Location = new Point((int)rect.Left, (int)rect.Top);
            blinkingRectForm.Size = new Size((int)(rect.Right - rect.Left), (int)(rect.Bottom - rect.Top));
            transparentPanel.Size = new Size(blinkingRectForm.Size.Width - 6, blinkingRectForm.Size.Height - 6);
            blinkingRectForm.Shown += RectangleForm_Shown;
            blinkingRectForm.Show();
        }

        private void RectangleForm_Shown(object sender, EventArgs e)
        {
            blinkTimer = new Timer();
            blinkTimer.Interval = 300;
            blinkTimes = 10;
            blinkTimer.Tick -= BlinkTimer_Tick;
            blinkTimer.Tick += BlinkTimer_Tick;
            blinkTimer.Enabled = true;
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            blinkingRectForm.Visible = !blinkingRectForm.Visible;
            if(blinkTimes-- == 0)
            {
                blinkTimer.Enabled = false;
                blinkingRectForm.Close();
            }
        }

        #endregion

        private void controlsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            controlsTree.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show(Control.MousePosition);
            }
        }
    }
}
