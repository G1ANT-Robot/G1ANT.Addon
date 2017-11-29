using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Automation;

using G1ANT.UiFramework.UiItems;
using System.Text.RegularExpressions;

namespace G1ANT.UiFramework
{
    public class AutomatedItem
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private Process proces;
        private string automationId = string.Empty;
        private string name = string.Empty;
        private int index = -1;
        private string processName = string.Empty;
        private int controllType;
        private string rect = string.Empty;
        private bool Id;
        public AutomatedItem()
        {

        }
        public AutomatedItem(AutomationElement elem, bool id)
        {
            Element = elem;
            if (Element != null)
            {
                try
                {
                    Id = id;
                    ReadParams();
                    FindChildren();
                    Actions = (G1ANTControl)G1ANTControlTypeFactory.RecognizeAndCreate(controllType, this);
                }
                catch (Exception ex)
                {

                    logger.Error($"Ui framework: Error while creating automated item: {ex.Message}");
                }
            }
            else
                logger.Error($"Ui framework: Cannot create automated item: Given automation element was null");
        }

        //public AutomatedItem(AutomationElement elem)
        //{
        //    Element = elem;
        //    if (Element != null)
        //    {
        //        try
        //        {
        //            ReadParams();
        //            FindChildren();
        //            Actions = (G1ANTControl)G1ANTControlTypeFactory.RecognizeAndCreate(controllType, this);
        //        }
        //        catch (Exception ex)
        //        {

        //            logger.Error($"Ui framework: Error while creating automated item: {ex.Message}");
        //        }
        //    }
        //    else
        //        logger.Error($"Ui framework:Cannot create automated item: Given automation element was null");
        //}
        public AutomatedItem(IntPtr handle, bool id)
        {
            Id = id;
            Handler = handle;
            Element = AutomationElement.FromHandle(Handler);
            if (Element != null)
            {
                try
                {
                    ReadParams();
                    FindChildren();
                    Actions = (G1ANTControl)G1ANTControlTypeFactory.RecognizeAndCreate(controllType, this);
                }
                catch (Exception ex)
                {
                    logger.Error($"Ui framework: Error while creating automated item: {ex.Message}");
                }
            }
            else
                logger.Error($"Ui framework: Unable to find element from handle: {Handler}");
        }
        public AutomatedItem(string param, bool id)
        {
            ReadConnentionString(param);
            FindElement();
            if (Element != null)
            {
                try
                {
                    Handler = new IntPtr(Element.Current.NativeWindowHandle);
                    FindChildren();
                    Actions = (G1ANTControl)G1ANTControlTypeFactory.RecognizeAndCreate(controllType, this);
                }
                catch (Exception ex)
                {
                    logger.Error($"Ui framework: Error while creating automated item: {ex.Message}");
                }
            }
            else
                logger.Error($"Ui framework: Unable to find element from wPath: {param}");
        }
        //public AutomatedItem(string param, AutomationElement ae)
        //{

        //    ReadConnentionString(param);
        //    FindIn(ae);
        //    if (Element != null)
        //    {
        //        try
        //        {
        //            Handler = new IntPtr(Element.Current.NativeWindowHandle);
        //            FindChildren();
        //            Actions = (G1ANTControl)G1ANTControlTypeFactory.RecognizeAndCreate(controllType, this);
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error($"Ui framework: Error while creating automated item: {ex.Message}");
        //        }
        //    }
        //    else
        //        logger.Error($"Ui framework: Unable to find element from wPath: {param} in given element");
        //}
        public AutomatedItem(string param, AutomationElement ae, bool id)
        {
            Id = id;
            ReadConnentionString(param);
            FindIn(ae);
            if (Element != null)
            {
                try
                {
                    Handler = new IntPtr(Element.Current.NativeWindowHandle);
                    FindChildren();
                    Actions = (G1ANTControl)G1ANTControlTypeFactory.RecognizeAndCreate(controllType, this);
                }
                catch (Exception ex)
                {
                    logger.Error($"Ui framework: Error while creating automated item: {ex.Message}");
                }
            }
            else
                logger.Error($"Ui framework: Unable to find element from wPath: {param} in given element");
        }
        public IntPtr Handler { get; set; }
        public AutomationElement Element { get; set; }
        /// <summary>
        /// Element used to call actions on this control
        /// </summary>
        /// <summary>
        /// Method used to find control based on Wpath string
        /// </summary>
        public G1ANTControl Actions { get; set; }
        public AutomatedItem Parent { get; set; }
        public string ParentString { get; set; } = string.Empty;
        public AutomationElementCollection Children { get; set; }
        public string ConnectionString { get; set; }
        public void FindElement()
        {
            List<PropertyCondition> conditions = MakeConditions();
            if (ParentString != string.Empty)
            {
                FindWithParentStringEverywhere(conditions);
            }
            else
            {
                FindWithoutParentStringEverywhere(conditions);
            }
        }

        private void FindWithoutParentStringEverywhere(List<PropertyCondition> conditions)
        {
            Process[] processss = null;
            try
            {
                processss = Process.GetProcessesByName(processName);
            }
            catch (Exception ex)
            {
                logger.Error($"Ui framework: Unable to find process: {processName}, {ex.Message}");
            }

            TreeWalker walkerek = new TreeWalker(new AndCondition(conditions.ToArray()));
            for (int i = 0; i < processss.Length; i++)
            {
                try
                {
                    proces = Process.GetProcessesByName(processName)[i];
                    AutomationElement nowe = AutomationElement.FromHandle(proces.MainWindowHandle);
                    var ch = nowe.FindAll(TreeScope.Children, Condition.TrueCondition);
                    Element = walkerek.GetFirstChild(nowe);
                    if (Element == null)
                    {
                        Element = walkerek.Normalize(nowe);
                        var side = nowe.FindAll(TreeScope.Children, Condition.TrueCondition);
                        for (int counter = 0; counter < side.Count; counter++)
                        { Element = walkerek.GetNextSibling(nowe); }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    logger.Error($"Ui Framework: {ex.Message} while searching element: {this.ConnectionString}");
                }
            }
        }

        private void FindWithParentStringEverywhere(List<PropertyCondition> conditions)
        {
            Parent = new AutomatedItem(ParentString, Id);
            TreeWalker walkerek = new TreeWalker(new AndCondition(conditions.ToArray()));

            Element = walkerek.GetFirstChild(Parent.Element);
            if (Element == null)
            {
                Element = walkerek.Normalize(Parent.Element);
            }
            if (Element == null)
                logger.Error($"Ui framework: Unable to find element: {this.ConnectionString}");
        }

        /// <summary>
        /// Method used to find control in sepicified window
        /// </summary>
        /// <param name="elem">Window to search in</param>
        public void FindIn(AutomationElement elem)
        {
            List<PropertyCondition> conditions = MakeConditions();

            if (ParentString != string.Empty)
            {
                if (index != -1)
                {
                    Parent = new AutomatedItem(ParentString, elem, Id);
                    FindWithIndexInParent();
                }
                else
                {
                    try
                    {
                        FindWithParentStringIn(elem, conditions);
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Ui framework: Error while searching element in: {ex.Message}");
                    }
                }
            }
            else
            {
                if (index != -1)
                {
                    try
                    {
                        List<AutomationElement> twins = new List<AutomationElement>();
                        var Par = new AutomatedItem(elem, Id);
                        List<AutomationElement> list = new List<AutomationElement>();
                        foreach (AutomationElement a in Par.Children)
                        {
                            list.Add(a);
                        }
                        if (!Id)
                        {
                            twins = list.FindAll(x => x.Current.ControlType == ControlType.LookupById(controllType) && x.Current.Name == name && x.Current.AutomationId == automationId);
                        }
                        else
                        {
                            twins = list.FindAll(x => x.Current.ControlType == ControlType.LookupById(controllType) && x.Current.Name == name);
                        }
                        Element = twins[index];
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Ui framework: Error while searching element in: {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        FindWithoutParentStringIn(elem, conditions);
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Ui framework: Error while searching element in: {ex.Message}");
                    }
                }
            }
        }

        private void FindWithIndexInParent()
        {
            try
            {
                List<AutomationElement> list = new List<AutomationElement>();
                List<AutomationElement> twins = new List<AutomationElement>();
                foreach (AutomationElement a in Parent.Children)
                {
                    list.Add(a);
                }
                if (!Id)
                {
                    twins = list.FindAll(x => x.Current.ControlType == ControlType.LookupById(controllType) && x.Current.Name == name && x.Current.AutomationId == automationId);
                }
                else
                {
                    twins = list.FindAll(x => x.Current.ControlType == ControlType.LookupById(controllType) && x.Current.Name == name);
                }

                Element = twins[index];
            }
            catch (Exception ex)
            {
                logger.Error($"Ui framework: Error while searching element in: {ex.Message}");
            }
        }

        private void FindWithParentStringIn(AutomationElement elem, List<PropertyCondition> conditions)
        {
            Parent = new AutomatedItem(ParentString, elem, Id);
            AutomationElement par = Parent.Element;
            TreeWalker walkerek = new TreeWalker(new AndCondition(conditions.ToArray()));

            Element = walkerek.GetFirstChild(par);
            if (Element == null)
            {
                Element = walkerek.Normalize(par);
            }
            if (Element == null)
                logger.Error($"Ui framework: Unable to find element: {this.ConnectionString}");
        }

        private void FindWithoutParentStringIn(AutomationElement elem, List<PropertyCondition> conditions)
        {
            TreeWalker walkereka = new TreeWalker(new AndCondition(conditions.ToArray()));
            try
            {
                var ch = elem.FindAll(TreeScope.Children, Condition.TrueCondition);
                Element = walkereka.GetFirstChild(elem);
                if (Element == null)
                {
                    Element = walkereka.Normalize(elem);
                    if (Element == null)
                    {
                        var side = elem.FindAll(TreeScope.Children, Condition.TrueCondition);
                        for (int counter = 0; counter < side.Count; counter++)
                        { Element = walkereka.GetNextSibling(elem); }
                    }
                    if (Element == null)
                        logger.Error($"Ui framework: Unable to find element: {this.ConnectionString}");
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Ui Framework: {ex.Message} while searching element: {this.ConnectionString}");
            }
        }

        private List<PropertyCondition> MakeConditions()
        {
            List<PropertyCondition> conditions = new List<PropertyCondition>();
            if (automationId != string.Empty)
            {
                PropertyCondition condition1 = new PropertyCondition(AutomationElement.AutomationIdProperty, automationId);
                conditions.Add(condition1);
            }
            if (name != string.Empty)
            {
                PropertyCondition condition2 = new PropertyCondition(AutomationElement.NameProperty, name);
                conditions.Add(condition2);
            }
            if (controllType != 0)
            {
                PropertyCondition condition3 = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.LookupById(controllType));
                conditions.Add(condition3);
            }
            if (rect != string.Empty)
            {
                PropertyCondition condition4 = new PropertyCondition(AutomationElement.ClassNameProperty, rect);
                conditions.Add(condition4);
            }
            return conditions;
        }
        private void ReadConnentionString(string paramm)
        {
            try
            {
                Regex regex = new Regex("<(.*?)>");
                var a = paramm.Split('\\');
                var b = a[a.Length - 1];
                ConnectionString = paramm;
                if (a.Length > 1)
                {
                    for (int i = 0; i < a.Length - 1; i++)
                    {
                        ParentString = ParentString + a[i] + "\\";
                    }
                    ParentString = ParentString.Substring(0, ParentString.Length - 1);
                }
                regex = new Regex("Name=\"(.*?)\"");
                name = regex.Match(b).Groups[1].Value;
                regex = new Regex("Id=\"(.*?)\"");
                automationId = regex.Match(b).Groups[1].Value;
                regex = new Regex("Type=\"(.*?)\"");
                var test = regex.Match(b).Groups[1].Value;
                if (!string.IsNullOrEmpty(test))
                    controllType = Int32.Parse(regex.Match(b).Groups[1].Value);
                regex = new Regex("Index=\"(.*?)\"");
                Int32.TryParse(regex.Match(b).Groups[1].Value, out index);
            }
            catch (Exception ex)
            {
                logger.Error($"Ui framework: Error while reading WPath: {ex.Message} {paramm}");
            }
        }
        private void ReadParams()
        {
            if (Element.Current.AutomationId != Element.Current.NativeWindowHandle.ToString())
            {
                if (Id)
                    automationId = Element.Current.AutomationId;
            }
            name = Element.Current.Name;
            // Parent = ReadParent();
            controllType = Element.Current.ControlType.Id;
            rect = Element.Current.ClassName;
        }
        private void FindChildren()
        {
            Children = Element.FindAll(TreeScope.Children, Condition.TrueCondition);
        }
        private AutomatedItem ReadParent()
        {
            try
            {
                PropertyCondition condition21 = new PropertyCondition(AutomationElement.ProcessIdProperty, Element.Current.ProcessId);
                PropertyCondition condition22 = new PropertyCondition(AutomationElement.IsControlElementProperty, true);
                TreeWalker walker = new TreeWalker(new AndCondition(condition21, condition22));
                AutomatedItem par = new AutomatedItem(walker.GetParent(Element), Id);
                return par;
            }
            catch (Exception ex)
            {
                logger.Error($"Ui framework: Unable to find parent of: {this.ConnectionString} {ex.Message}");
                return null;
            }
        }
    }
}