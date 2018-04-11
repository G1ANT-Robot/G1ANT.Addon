using System;
using System.Windows.Forms;
using G1ANT.Language;
using System.Windows.Automation;
using System.Text;

namespace G1ANT.Addon.UI.Panels
{
    [PanelAttribute(Title = "UI Applications", DockingSide = DockingSide.Right, InitialAppear = true, Width = 400)]
    public partial class UIControlsPanel : RobotPanel
    {
        public UIControlsPanel()
        {
            InitializeComponent();
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

        private string GetTreeNodeName(AutomationElement element)
        {
            if (element == null)
                return "";
            string id = "";
            if (string.IsNullOrWhiteSpace(element.Current.AutomationId) == false)
                id = $" #{element.Current.AutomationId}";
            return $"{element.Current.LocalizedControlType}{id} \"{element.Current.Name}\"";
        }

        private string GetTreeNodeTooltip(AutomationElement element)
        {
            if (element == null)
                return null;
            StringBuilder result = new StringBuilder();

            if (string.IsNullOrWhiteSpace(element.Current.AutomationId) == false)
                result.AppendLine($"Id: {element.Current.AutomationId}");
            if (string.IsNullOrWhiteSpace(element.Current.LocalizedControlType) == false)
                result.AppendLine($"ControlType: {element.Current.LocalizedControlType}");
            if (string.IsNullOrWhiteSpace(element.Current.ClassName) == false)
                result.AppendLine($"ClassName: {element.Current.ClassName}");
            if (string.IsNullOrWhiteSpace(element.Current.Name) == false)
                result.AppendLine($"Name: {element.Current.Name}");

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
                    while (elem != null)
                    {
                        var node = e.Node.Nodes.Add(GetTreeNodeName(elem));
                        node.ToolTipText = GetTreeNodeTooltip(elem);
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
    }
}
