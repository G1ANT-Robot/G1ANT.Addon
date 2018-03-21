using System;
using CodePlex.XPathParser;
using System.Windows.Automation;
using System.Collections.Generic;

namespace G1ANT.Addon.UI
{
    public class UIElement
    {
        public static UIElement RootElement { get; set; } = null;

        protected AutomationElement automationElement;

        private UIElement()
        {
        }

        public UIElement(AutomationElement element)
        {
            if (element == null)
                throw new NullReferenceException("Cannot create UIElement class from empty AutomationElement");
            automationElement = element;
        }

        public static UIElement FromWPath(WPathStructure wpath)
        {
            object xe = new XPathParser<object>().Parse(wpath.Value, new XPathUIElementBuilder(RootElement?.automationElement));
            if (xe is AutomationElement element)
            {
                return new UIElement()
                {
                    automationElement = element
                };
            }
            throw new NullReferenceException($"Cannot find UI element described by \"{wpath.Value}\".");
        }

        public class NodeDescription
        {
            public string id;
            public string name;
            public string className;
            public ControlType type;
        }

        public WPathStructure ToWPath()
        {
            Stack<NodeDescription> stack = new Stack<NodeDescription>();
            TreeWalker walker = TreeWalker.ControlViewWalker;
            AutomationElement elementParent;
            AutomationElement node = automationElement;
            do
            {
                stack.Push(new NodeDescription()
                {
                    id = node.Current.AutomationId,
                    name = node.Current.Name,
                    className = node.Current.ClassName,
                    type = node.Current.ControlType
                });
                elementParent = walker.GetParent(node);
                if (elementParent == RootElement.automationElement)
                    break;
                node = elementParent;
            }
            while (true);

            bool parentIsEmpty = false;
            string wpath = "";
            foreach (var elem in stack)
            {
                if (string.IsNullOrEmpty(elem.id) && string.IsNullOrEmpty(elem.name))
                    parentIsEmpty = true;
                else
                {
                    string xpath = "";
                    if (parentIsEmpty)
                        xpath += "descendant::";
                    if (string.IsNullOrEmpty(elem.id) == false)
                        xpath += $"ui[@id='{elem.id}']";
                    else if (string.IsNullOrEmpty(elem.name) == false)
                        xpath += $"ui[@name='{elem.name}']";
                    wpath += $"/{xpath}";
                    parentIsEmpty = false;
                }
            }
            return new WPathStructure(wpath);
        }

        public void Click()
        {
            InvokePattern pattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            if (pattern != null)
                pattern.Invoke();
        }

        public void SetText(string text)
        {
            if (automationElement.Current.NativeWindowHandle != 0)
            {

            }
        }

        public string GetText()
        {
            return null;
        }
    }
}
