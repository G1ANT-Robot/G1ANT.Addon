using System;
using CodePlex.XPathParser;
using System.Windows.Automation;
using System.Collections.Generic;
using System.Linq;

using CompareFunc = System.Func<
    System.Windows.Automation.AutomationElement,
    bool>;
using GetElementFunc = System.Func<
    System.Windows.Automation.AutomationElement,
    System.Windows.Automation.AutomationElement>;
using FindElementFunc = System.Func<
    System.Windows.Automation.AutomationElement,
    System.Func<System.Windows.Automation.AutomationElement, bool>,
    System.Windows.Automation.AutomationElement>;

namespace G1ANT.Addon.UI
{
    public class XPathUIElementBuilder : IXPathBuilder<object>
    {
        protected AutomationElement FindDescendant(AutomationElement elem, CompareFunc compare)
        {
            AutomationElement elementNode = TreeWalker.ControlViewWalker.GetFirstChild(elem);
            while (elementNode != null)
            {
                if (compare(elementNode))
                    return elementNode;
                var descendantElement = FindDescendant(elementNode, compare);
                if (descendantElement != null)
                    return descendantElement;
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }
            return null;
        }

        protected AutomationElement FindChild(AutomationElement elem, CompareFunc compare)
        {
            AutomationElement elementNode = TreeWalker.ControlViewWalker.GetFirstChild(elem);
            while (elementNode != null)
            {
                if (compare(elementNode))
                    return elementNode;
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }
            //return null;
            //var childrens = elem.FindAll(TreeScope.Children, PropertyCondition.TrueCondition);
            //foreach (AutomationElement child in childrens)
            //{
            //    if (compare(child))
            //        return child;
            //}
            throw new ElementNotAvailableException();
        }

        protected AutomationElement FindFollowingSibling(AutomationElement elem, CompareFunc compare)
        {
            AutomationElement elementNode = TreeWalker.ControlViewWalker.GetFirstChild(elem);

            while (elementNode != null)
            {
                if (compare(elementNode))
                    return elementNode;
                elementNode = TreeWalker.ControlViewWalker.GetNextSibling(elementNode);
            }
            throw new ElementNotAvailableException();
        }

        protected AutomationElement FindDescendantOrSelf(AutomationElement elem, CompareFunc compare)
        {
            if (compare(elem))
                return elem;
            return FindDescendant(elem, compare);
        }

        public AutomationElement Root { get; } = AutomationElement.RootElement;
        public XPathUIElementBuilder(AutomationElement root = null)
        {
            if (root != null)
                Root = root;
        }

        public void StartBuild()
        {
        }

        public object EndBuild(object result)
        {
            return result;
        }

        public object String(string value)
        {
            return value;
        }

        public object Number(string value)
        {
            throw new NotImplementedException("Method 'Number' is not implemented.");
        }

        public object Operator(XPathOperator op, object left, object right)
        {
            if (op == XPathOperator.Eq)
            {
                if (left is AutomationProperty property)
                {
                    CompareFunc func = (elem) =>
                    {
                        var propValue = elem.GetCurrentPropertyValue(property, true);
                        return propValue.Equals(right);
                    };
                    return func;
                }
            }
            throw new NotSupportedException($"Operator {op.ToString()} is not supported.");
        }


        public object Axis(XPathAxis xpathAxis, System.Xml.XPath.XPathNodeType nodeType, string prefix, string name)
        {
            if (xpathAxis == XPathAxis.Root)
                return Root;
            if (nodeType == System.Xml.XPath.XPathNodeType.Element)
            {
                if (name != "ui")
                    throw new NotSupportedException($"{name} element is not supportet.");
            }
            if (xpathAxis == XPathAxis.Descendant)
            {
                FindElementFunc func = FindDescendant;
                return func;
            }
            if (xpathAxis == XPathAxis.DescendantOrSelf)
            {
                FindElementFunc func = FindDescendantOrSelf;
                return func;

            }
            if (xpathAxis == XPathAxis.FollowingSibling)
            {
                FindElementFunc func = FindFollowingSibling;
                return func;
            }
            if (xpathAxis == XPathAxis.Child)
            {
                FindElementFunc func = FindChild;
                return func;
            }
            if (xpathAxis == XPathAxis.Attribute)
            {
                if (name == "id")
                    return AutomationElement.AutomationIdProperty;
                if (name == "name")
                    return AutomationElement.NameProperty;
                if (name == "class")
                    return AutomationElement.ClassNameProperty;
                if (name == "type")
                    return AutomationElement.ControlTypeProperty;
                throw new NotSupportedException($"Attribute {name} is not supportet.");
            }
            return null;
        }

        public object JoinStep(object left, object right)
        {
            if (left is AutomationElement elem &&
                right is GetElementFunc func)
            {
                return func(elem);
            }
            if (left is GetElementFunc inner &&
                right is GetElementFunc outer)
            {
                GetElementFunc retFunc = (element) =>
                {
                    var ret = inner(element);
                    if (ret == null)
                        throw new ElementNotAvailableException();
                    return outer(ret);
                };
                return retFunc;
            }
            return null;
        }

        public object Predicate(object node, object condition, bool reverseStep)
        {
            if (node is FindElementFunc outer &&
                condition is CompareFunc inner)
            {
                GetElementFunc func = (elem) =>
                {
                    return outer(elem, inner);
                };
                return func;
            }
            return null;
        }

        public object Variable(string prefix, string name)
        {
            throw new NotImplementedException("Method 'Variable' is not implemented.");
        }

        public object Function(string prefix, string name, IList<object> args)
        {
            if (name.ToLower() == "contains" && args.Count == 2)
            {
                if (args[0] is AutomationProperty property &&
                    args[1] is string text)
                {
                    CompareFunc func = (elem) =>
                    {
                        if (elem.GetCurrentPropertyValue(property, true) is string str)
                        {
                            if (str.Contains(text))
                                return true;
                        }
                        return false;
                    };
                    return func;
                }
            }
            throw new NotSupportedException($"Function {name} is not supportet.");
        }
    }
}
