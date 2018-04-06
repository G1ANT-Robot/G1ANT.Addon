using System;
using CodePlex.XPathParser;
using System.Windows.Automation;
using System.Collections.Generic;
using G1ANT.Language;
using System.Drawing;

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
            object pattern;
            if (automationElement.TryGetCurrentPattern(InvokePattern.Pattern, out pattern))
            {
                InvokePattern invokePattern = pattern as InvokePattern;
                if (invokePattern != null)
                    invokePattern.Invoke();
            }
            else
            {
                var rect = GetRectangle();
                System.Windows.Point pt = new System.Windows.Point(rect.X + rect.Size.Width/2, rect.Y + rect.Size.Height / 2);
                //if (automationElement.TryGetClickablePoint(out pt))
                {
                    MouseWin32.MousePoint tempPos = MouseWin32.GetCursorPosition();
                    Point currentPos = new Point(tempPos.X, tempPos.Y);

                    Point targetPos = new Point((int)pt.X, (int)pt.Y);

                    List<MouseStr.MouseEventArgs> mouseArgs =
                        MouseStr.ToMouseEventsArgs(
                            targetPos.X,
                            targetPos.Y,
                            currentPos.X,
                            currentPos.Y,
                            "left",
                            "press",
                            1);

                    foreach (MouseStr.MouseEventArgs arg in mouseArgs)
                    {
                        MouseWin32.MouseEvent(arg.dwFlags, arg.dx, arg.dy, arg.dwData);
                        System.Threading.Thread.Sleep(10);
                    }
                }
            }
        }

        public void SetFocus()
        {
            //if (automationElement.Current.NativeWindowHandle != 0)
            //{
            //    IntPtr wndHandle = new IntPtr(automationElement.Current.NativeWindowHandle);
            //    RobotWin32.SetFocus(wndHandle);
            //}
            //else
            automationElement.SetFocus();
        }

        public void SetText(string text)
        {
            object valuePattern = null;
            if (automationElement.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
            {
                automationElement.SetFocus();
                ((ValuePattern)valuePattern).SetValue(text);
            }
            else if (automationElement.Current.NativeWindowHandle != 0)
            {
                automationElement.SetFocus();
                IntPtr wndHandle = new IntPtr(automationElement.Current.NativeWindowHandle);
                KeyboardTyper.TypeWithSendInput($"{SpecialChars.KeyBegin}ctrl+home{SpecialChars.KeyEnd}", null, wndHandle, IntPtr.Zero, 0, false, 0); // Move to start of control
                KeyboardTyper.TypeWithSendInput($"{SpecialChars.KeyBegin}ctrl+shift+end{SpecialChars.KeyEnd}", null, wndHandle, IntPtr.Zero, 0, false, 0); // Select everything
                KeyboardTyper.TypeWithSendInput(text, null, wndHandle, IntPtr.Zero, 0, false, 0);
            }
            else
                throw new NotSupportedException("SetText is not supported");
        }

        public System.Windows.Rect GetRectangle()
        {
            object boundingRectNoDefault =
                automationElement.GetCurrentPropertyValue(AutomationElement.BoundingRectangleProperty, true);
            if (boundingRectNoDefault != AutomationElement.NotSupported)
                return (System.Windows.Rect)boundingRectNoDefault;
            else if (automationElement.Current.NativeWindowHandle != 0)
            {
                RobotWin32.Rect rect = new RobotWin32.Rect();
                IntPtr wndHandle = new IntPtr(automationElement.Current.NativeWindowHandle);
                if (RobotWin32.GetWindowRectangle(wndHandle, ref rect))
                    return new System.Windows.Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }
            throw new NotSupportedException("Cannot get rectangle for that kind of UI element.");
        }

        public string GetText()
        {
            //if (automationElement.Current.NativeWindowHandle != 0)
            //{
            //    IntPtr wndHandle = new IntPtr(automationElement.Current.NativeWindowHandle);
            //    return RobotWin32.GetWindowText(wndHandle);
            //}
            return automationElement.Current.Name;
        }
    }
}
