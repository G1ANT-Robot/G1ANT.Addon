/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.IExplorer
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using WatiN.Core;
using WatiN.Core.Constraints;
using System.Linq;
using System.Windows.Automation;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Specialized;
using WatiN.Core.Exceptions;

namespace G1ANT.Addon.IExplorer
{
    public class IEWrapper
    {
        private WatiN.Core.IE ie = null;

        public IEWrapper(int id)
        {
            this.Id = id;
        }

        public IEWrapper(int id, string name, string by) : this(id)
        {
            BrowserSearchBy searchBy;
            Constraint constraint = null;
            if (Enum.TryParse<BrowserSearchBy>(by.CapitalizeFirstLetter(), out searchBy) == false)
            {
                throw new ArgumentException("Argument 'by' is not correct");
            }
            switch (searchBy)
            {
                case BrowserSearchBy.Title:
                    constraint = Find.ByTitle(name);
                    break;

                case BrowserSearchBy.Url:
                    constraint = Find.ByUrl(x => (x ?? string.Empty).ToLower().Contains(name.ToLower()));
                    break;
            }
            try
            {
                Ie = WatiN.Core.IE.AttachToNoWait<WatiN.Core.IE>(constraint, 5);
            }
            catch (Exception ex)
            {
                throw new System.TimeoutException($"Timeout occured while attaching to Internet Explorer. Message: {ex.Message}");
            }
        }

        public int Id { get; set; }

        public WatiN.Core.IE Ie
        {
            get
            {
                if (ie == null)
                {
                    throw new ApplicationException("InternetExplorer instance has to be attached first");
                }
                return ie;
            }
            set
            {
                ie = value;
            }
        }

        public static string MsCrmFindViaQueryAllSelector
        {
            get
            {
                return global::G1ANT.Addon.IExplorer.Properties.Resources.FindViaDocumentQuerySelector;
            }
        }

        public void ActivateTab(string by, string phrase)
        {
            new TabActivator(Ie.hWnd).ActivateTab(by, phrase);
        }

        public void GoToUrl(string address, bool noWait, int timeoutMs)
        {
            try
            {
                Navigate(address, noWait, timeoutMs);

            }
            catch (WatiN.Core.Exceptions.TimeoutException)
            {
                Thread.Sleep(5000);
                Navigate(address, noWait, timeoutMs);
            }
        }

        private void Navigate(string address, bool noWait, int timeoutMs)
        {
            if (timeoutMs > 0)
            {
                Settings.WaitForCompleteTimeOut = timeoutMs / 1000;
            }
            if (noWait == false)
            {
                Ie.GoTo(address);
            }
            else
            {
                Ie.GoToNoWait(address);
            }
        }

        public void ClickElement(string search, string by, int timeoutMs, bool noWait)
        {
            Element element = FindElement(search, by, timeoutMs);
            try
            {
                if (noWait)
                {
                    element?.ClickNoWait();
                }
                else
                {
                    element.WaitUntilExists(timeoutMs);
                    element?.Click();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Could not click specified element. Message: {ex.Message}", ex);
            }
        }

        public void FireEvent(string eventName, List<string> arguments, string search, string by, int timeoutMs, bool noWait)
        {
            NameValueCollection collection = new NameValueCollection();
            if (arguments != null && arguments.Count > 0)
            {
                foreach (string argument in arguments)
                {
                    collection.Add(null, argument);
                }
            }
            Element element = FindElement(search, by, timeoutMs);
            try
            {
                if (noWait)
                {
                    element.FireEventNoWait(eventName, collection);
                }
                else
                {
                    element.WaitUntilExists(timeoutMs);
                    element.WaitForComplete();
                    element.FireEvent(eventName, collection);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Could not fire '{eventName}' event. Message: {ex.Message}", ex);
            }
        }

        public void SetAttribute(string attributeName, string value, string search, string by, int timeoutMs, bool noWait)
        {
            Element element = FindElement(search, by, timeoutMs);
            try
            {
                if (noWait)
                {
                    element?.SetAttributeValue(attributeName, value);
                }
                else
                {
                    element.WaitUntilExists(timeoutMs);
                    element?.SetAttributeValue(attributeName, value);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Could not set value '{value ?? string.Empty}' in attribute '{attributeName}'. Message: {ex.Message}", ex);
            }
        }

        public string GetAttribute(string attributeName, string search, string by, int timeoutMs, bool noWait)
        {
            Element element = FindElement(search, by, timeoutMs);
            try
            {
                if (noWait)
                {
                    return element?.GetAttributeValue(attributeName) ?? string.Empty;
                }
                else
                {
                    element.WaitUntilExists(timeoutMs);
                    return element?.GetAttributeValue(attributeName) ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Could not get attribute's '{attributeName}' value. Message: {ex.Message}", ex);
            }
        }

        public Element FindElement(string search, string by, int timeoutMs)
        {
            Element element = null;
            ElementSearchBy searchBy;
            if (Enum.TryParse<ElementSearchBy>(by.CapitalizeFirstLetter(), out searchBy) == false)
            {
                throw new ArgumentException("Argument 'By' was not recognized");
            }
            switch (searchBy)
            {
                case ElementSearchBy.Name:
                    element = Ie.Element(Find.ByName(search));
                    break;
                case ElementSearchBy.Text:
                    element = Ie.Element(Find.ByText(search));
                    break;
                case ElementSearchBy.Title:
                    element = Ie.Element(Find.ByTitle(search));
                    break;
                case ElementSearchBy.Class:
                    element = Ie.Element(Find.ByClass(search));
                    break;
                case ElementSearchBy.Id:
                    element = Ie.Element(Find.ById(search));
                    break;
                case ElementSearchBy.Selector:
                    element = Ie.Element(Find.BySelector(search));
                    break;
                case ElementSearchBy.Query:
                    var id = Ie.FindIdViaQuery(ReplaceQueryQuotes(search), "query", timeoutMs);
                    element = Ie.Element(Find.ById(id));
                    break;
                case ElementSearchBy.Jquery:
                    id = Ie.FindIdViaQuery(ReplaceQueryQuotes(search), "jquery", timeoutMs);
                    element = Ie.Element(Find.ById(id));
                    break;
            }
            try
            {
                element.WaitUntilExists(timeoutMs / 1000);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Exception occured while waiting for element. Search: '{search}', By: '{by}'. Message: {ex.Message}");
            }
            return element;
        }

        private string ReplaceQueryQuotes(string input)
        {
            return input.Replace("'", "\"");
        }

        public string InsertJavaScriptAndTakeResult(string script)
        {
            string result = Ie.Eval(script);
            return result;
        }

        public void DownLoadFile(string path = "")
        {
            AutomationElementCollection ParentElements = AutomationElement.FromHandle(Ie.hWnd).FindAll(TreeScope.Children, Condition.TrueCondition);
            foreach (AutomationElement ParentElement in ParentElements)
            {
                // Identidfy Download Manager Window in Internet Explorer
                if (ParentElement.Current.ClassName == "Frame Notification Bar")
                {
                    AutomationElementCollection ChildElements = ParentElement.FindAll(TreeScope.Children, Condition.TrueCondition);
                    // Idenfify child window with the name Notification Bar or class name as DirectUIHWND 
                    foreach (AutomationElement ChildElement in ChildElements)
                    {
                        if (ChildElement.Current.Name == "Notification bar" || ChildElement.Current.ClassName == "DirectUIHWND")
                        {

                            AutomationElementCollection DownloadCtrls = ChildElement.FindAll(TreeScope.Children, Condition.TrueCondition);
                            foreach (AutomationElement ctrlButton in DownloadCtrls)
                            {
                                //Now we try to find save button
                                if (ctrlButton.Current.Name.ToLower() == "save")
                                {
                                    //after that we need to click the arrow to expand hidden options       
                                    var subButton = ctrlButton.FindAll(TreeScope.Subtree, Condition.TrueCondition);
                                    var invokeSubButton = subButton[1].GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                                    invokeSubButton.Invoke();
                                    G1ANT.Language.KeyboardTyper.Type("⋘down⋙⋘enter⋙", string.Empty, null, IntPtr.Zero, 400, false, 20, false);
                                    Thread.Sleep(1000);
                                    Language.KeyboardTyper.Type(path, string.Empty, null, IntPtr.Zero, 400, false, 20, false);
                                    Language.KeyboardTyper.Type("⋘enter⋙", string.Empty, null, IntPtr.Zero, 400, false, 20, false);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void WaitForLoad(int timeoutSeconds)
        {
            Ie.WaitForComplete(timeoutSeconds);
        }

        public void Refresh(int timeoutMs)
        {
            Settings.WaitForCompleteTimeOut = timeoutMs / 1000;
            Ie.Refresh();
        }

        public void Close()
        {
            if (Ie?.NativeBrowser != null)
            {
                Ie.Dispose();
            }
        }
    }
}
