using G1ANT.Addon.Mscrm.Properties;
using G1ANT.Language;
using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatiN.Core;
using WatiN.Core.Comparers;
using WatiN.Core.Constraints;
using WatiN.Core.Native;
using WatiN.Core.Native.InternetExplorer;


namespace G1ANT.Addon.Mscrm
{
    public class MsCrmWrapper
    {
        public int Id { get; set; }

        public IE Ie { get; private set; }

        public string Title
        {
            get { return Ie == null ? "" : Ie.Title; }
        }

        public bool MsCrmRecordingSwitch { get; set; } = false;

        public string Address { get; set; }
        public MsCrmWrapper(int id)
        {
            this.Id = id;
        }


        private string MsCrmWizardInjectionScript
        {
            get
            {
                return Resources.MsCrmWizardInjection;
            }
        }


        public MsCrmWrapper(int id, string name, string by = "title", bool mscrmRecorder = false)
        {
            Ie = null;
            try
            {
                this.Id = id;
                string search = name.ToLower();
                IECollection list = IE.InternetExplorers();
                foreach (var item in list)
                {
                    if (by.ToLower() == "title")
                    {
                        if (item.Title.ToLower().Contains(search))
                        {
                            Ie = item;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Url.ToLower().Contains(search))
                        {
                            Ie = item;
                            break;
                        }
                    }
                }

                if (Ie != null)
                {
                    Address = Ie.Url;
                }
            }
            catch
            {
                throw new TimeoutException("Timeout occured while attaching to Internet Explorer / CRM");
            }
            if (mscrmRecorder && Ie?.InternetExplorer != null)
            {
                MsCrmRecordingSwitch = true;
                SHDocVw.InternetExplorer webBrowser = (SHDocVw.InternetExplorer)Ie.InternetExplorer;
                webBrowser.DocumentComplete += ReinjectJavascriptListeners;
                object unimportant = null;
                ReinjectJavascriptListeners(null, ref unimportant);
            }
        }

        private void ReinjectJavascriptListeners(object pDisp, ref object URL)
        {
            {
                var injectionThread = new Thread(() =>
                {
                    string javaScriptCode = MsCrmWizardInjectionScript;
                    try
                    {
                        Ie.NativeDocument.RunScript(javaScriptCode, "javascript");
                    }
                    catch { }
                })
                { IsBackground = true };
                injectionThread.SetApartmentState(ApartmentState.STA);
                injectionThread.Start();
            }
        }

        public void TypeText(string text)
        {
            Language.KeyboardTyper.TypeWithSendInput(text, string.Empty, Ie.hWnd, IntPtr.Zero,400, true, 10);
        }

        public void ActivateTab(string by, string phrase)
        {
            if (Ie != null)
            {
                new TabActivator(Ie.hWnd).ActivateTab(by, phrase);
            }
        }

        public void ClickByElement(string search, string by = "id", string iFrame = "", bool trigger = false, bool noWait = false)
        {
            Element continueLink = FindElement(search, by, iFrame);

            if (!noWait)
            {
                continueLink.WaitUntilExists();
                continueLink.WaitForComplete();
            }
            ListItem li;
            if (continueLink.ClassName.Contains("ms-crm-CommandBarItem") &&
                continueLink.ClassName.Contains("ms-crm-CommandBar-Menu") &&
                continueLink.ClassName.Contains("ms-crm-CommandBar-Button"))
            {
                while (Ie.Eval(WatiNHelper.AreCommandBarsInitialized).ToLower() != "true")
                {
                    Thread.Sleep(50);
                }
                Thread.Sleep(3000);
                continueLink = FindElement(search, by, iFrame);
                li = continueLink.DomContainer.ListItem(Find.ById(search));
                if (li.Links.Count > 0)
                {
                    continueLink.DomContainer.WaitForComplete();
                    var link = li.Links[0];
                    continueLink = link ?? continueLink;
                    continueLink.WaitUntilExists();
                }
            }            

            if (continueLink == null)
            {
                throw new ApplicationException("Element to click not found");
            }
            else if (trigger)
            {
                if (noWait)
                {
                    continueLink?.FireEventNoWait("onclick");
                }
                else
                {
                    continueLink?.FireEvent("onclick");
                }
            }
            else
            {
                if (noWait)
                {
                    continueLink?.ClickNoWait();
                }
                else
                {
                    continueLink?.Click();
                }
            }
            Thread.Sleep(500);
        }

        public void ClickElement(Element element)
        {

        }
        public void Dettach(bool stopMsCrmRecorder = false)
        {
            if (Ie != null)
            {
                Ie.AutoClose = false;
                Ie.Dispose();
            }
        }

        public Document GetCurrentIframe(string iFrame = "")
        {
            Document result = null;
            if (iFrame == "none")
            {
                result = Ie;
            }
            else if (iFrame == "")
            {
                result = Ie.Frames.Filter(Find.By("title", "Content Area")).Filter(Find.ByStyle("visibility", "visible")).FirstOrDefault();
            }
            else if (iFrame != null)
            {
                iFrame = iFrame.Replace("#", "");
                result = Ie.Frames.Filter(Find.ById(iFrame)).FirstOrDefault();
            }
            if (result == null)
                throw new ApplicationException($"Iframe {iFrame} could not be found.");
            return result;
        }

        public Constraint GetFindContraint(string search, string by = "id")
        {
            Constraint constraint = null;
            switch (by)
            {
                case "name":
                    constraint = Find.ByName(search);
                    break;
                case "text":
                    constraint = Find.ByText(search);
                    break;
                case "title":
                    constraint = Find.ByTitle(search);
                    break;
                case "class":
                    constraint = Find.BySelector(search);
                    break;
                case "id":
                    search = search.Replace("#", "");
                    constraint = Find.ById(search);
                    break;
                case "selector":
                    constraint = Find.BySelector(search);
                    break;
            }
            return constraint;
        }

        public Element FindElement(string search, string by = "id", string iFrame = "")
        {
            Document doc = GetCurrentIframe(iFrame);
            Element element = null;
            switch (by)
            {
                case "name":
                case "text":
                case "title":
                    element = doc.Element(GetFindContraint(search, by));
                    break;
                case "class":
                    string selector = search.StartsWith(".") ? search : $".{search}";
                    var id = Ie.FindIdViaJQuery(selector, iFrame, 5000);
                    element = doc.Element(Find.ById(id));
                    break;
                case "id":
                    element = doc.Element(Find.ById(search));
                    break;
                case "selector":
                    if (search.StartsWith("#"))
                        search = search.TrimStart(new char[] { '#' });
                    element = doc.Element(Find.ById(Ie.FindIdViaJQuery(search, iFrame, 5000)));
                    break;
            }
            return element;
        }

        public int SetValInLookupField(Document iFrame, Element element, string value)
        {
            var dialogMenu = iFrame.Elements.Filter(Find.ById("Dialog_" + element.Id + "_IMenu"))
                                                      .FirstOrDefault();
            if (dialogMenu?.Style.Display == "block")
            {
                // when menu is rolled down
                TypeText("⋘esc⋙");
            }
            var editNode = element.Elements().Filter(Find.ByClass("ms-crm-Inline-Edit ms-crm-Inline-Lookup", false)).FirstOrDefault();
            if (editNode?.Style.Display == "block")
            {
                // we're in edit mode already
                TypeText("⋘ctrl⋙⋘ctrl+a⋙⋘del⋙");
            }
            else
            {
                // clicking (when there is an error and click occurs, we get menu rolled down again)
                element.Click();
            }
            if (dialogMenu?.Style.Display == "block")
            {
                // we have to get rid of menu (that appeared because of an error)
                TypeText("⋘esc⋙");
                TypeText("⋘ctrl⋙⋘ctrl+a⋙⋘del⋙");
            }

            TypeText(value);
            TypeText("⋘enter⋙");
            Element menu = null;

            while (menu == null && Command.CheckTimeout())
            {
                menu = iFrame.Elements.Filter(Find.ById(element.Id + "_IMenu")).FirstOrDefault();
            }
            if (menu == null)
            {
                throw new ApplicationException("Timeout occured while waiting for lookup menu to popup");
            }
            string resultsFound = menu.Parent.NextSibling.Text;
            resultsFound = Regex.Replace(resultsFound, "[A-Za-z ]", "").Trim();
            int resultsCount = 0;
            int.TryParse(resultsFound, out resultsCount);
            int finalResult = resultsCount == 1 ? 1 : resultsCount > 1 ? 2 : 0;
            if (finalResult == 1)
            {
                Element selectedRecord = null;
                while (selectedRecord == null && Command.CheckTimeout())
                {
                    selectedRecord = menu.Elements().Filter(Find.ByClass("ms-crm-MenuItem-NoOutline ms-crm-IL-MenuItem ms-crm-IL-MenuItem-Hover")).FirstOrDefault();
                }
                TypeText("⋘enter⋙");
            }
            else
            {
                TypeText("⋘esc⋙");
            }
            return finalResult;
        }

        public int SetValInOptionSet(Document iFrame, Element element, string value)
        {
            element.Click();
            Element menu = null;
            while (menu == null && Command.CheckTimeout())
            {
                menu = element.Elements().Filter(Find.ByClass(p => p.Contains("ms-crm-Inline-Edit ms-crm-Inline-OptionSet noScroll"))).FirstOrDefault();
            }
            if (menu == null)
            {
                throw new ApplicationException("Timeout occured while waiting for optionset menu to popup");
            }
            TypeText(value);

            string currentTitle = null;
            currentTitle = element.Parent.Title.ToLower() == value.ToLower() ? value.ToLower() : null;

            TypeText(currentTitle != null ? "⋘enter⋙" : "⋘esc⋙");
            return currentTitle != null ? 1 : 0;
        }

        public int SetValInTreeOptionSet(Document iFrame, Element element, string value)
        {
            Div div = element as Div;
            var arrowImg = div.Elements.Filter(Find.ById($"{div.Id}_i")).FirstOrDefault();
            if (arrowImg.Exists == true)
            {
                arrowImg.Click();
            }
            else
            {
                if (div?.Divs.Count > 1)
                {
                    if (div.Divs[0].Style.GetAttributeValue("display") == "block")
                    {
                        element.Click();
                        Thread.Sleep(100);
                    }
                    if (div.Divs[1].Style.GetAttributeValue("display") == "block")
                    {
                        arrowImg?.Click();
                    }
                }
            }
            Div treeDiv = iFrame.Div(element.Id + "_treediv_flyOut");
            treeDiv.WaitUntilExists(Command.GetTimeoutLeftSeconds());

            if (treeDiv == null)
                throw new ApplicationException("Timeout occured while waiting for tree optionset menu to popup");

            TypeText("⋘down⋙");
            TypeText("⋘right 60⋙");

            treeDiv.WaitUntil(x => x.Elements?.Count > 0, Command.GetTimeoutLeftSeconds());
            ElementCollection options = null;
            while (options == null && Command.CheckTimeout())
                options = treeDiv.Elements.Filter(Find.By("HREF", "#"));
            while (options.Count == 0) { }

            foreach (var option in options)
            {
                string text1 = option.OuterText.ToLower().Trim();
                string text2 = value.ToLower().Trim();
                if (option.OuterText.ToLower().Trim() == value.ToLower().Trim())
                {
                    option.Click();
                    return 1;
                }
            }
            TypeText("⋘esc⋙");
            return 0;
        }

        public int SetValInTwoOptionsOptionSet(Document iFrame, Element element, string value)
        {
            Div div = element as Div;
            if (div != null)
            {
                while (Command.CheckTimeout())
                {
                    WatiN.Core.Label label = div.Labels?.FirstOrDefault();
                    if (label != null)
                    {
                        if (label?.Elements?.Count == 2)
                        {
                            var firstElement = label.Elements[0];
                            string currentValue = firstElement.TextAfter.ToLower();
                            if (currentValue != value.ToLower())
                            {
                                element.Click();
                                Thread.Sleep(50);
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public int SetValInTextField(Document iFrame, Element element, string value, int timeout)
        {
            element.Click();
            var div = element as Div;
            if (div != null && div.TextFields.Count() > 0)
            {
                //div.TextFields[0].Click();
            }
            TypeText("⋘ctrl⋙⋘ctrl+a⋙⋘del⋙");
            TypeText(value);
            TypeText("⋘enter⋙");
            return 1;
        }

        public int SetValInDateTimeTextField(Document iFrame, Element element, string value, int timeout)
        {
            element.Click();
            var div = element as Div;
            if (div.Divs.Count > 0)
            {
                if (div.Style.GetAttributeValue("display") != "none")
                {
                    element.Click();
                }
            }
            TypeText("⋘ctrl⋙⋘ctrl+a⋙⋘del⋙");
            TypeText(value);
            TypeText("⋘enter⋙");
            return 1;
        }

        public Element GetInputElement(string search, string by = "id", string frameId = "", bool ignoreVisibility = false)
        {
            Element element = null;
            var iFrame = GetCurrentIframe(frameId);
            iFrame.Div(GetFindContraint(search, by)).WaitUntilExists(Command.GetTimeoutLeftSeconds());
            while (element == null && Command.CheckTimeout())
            {
                element = iFrame.Elements.Filter(GetFindContraint(search, by))?.FirstOrDefault();
            }
            bool visible = false;
            while (element != null && !visible && Command.CheckTimeout())
            {
                var div = element as Div;
                if ((div?.DomContainer as IE)?.Visible ?? false)
                {
                    visible = true;
                }
            }
            if (element == null || (!ignoreVisibility && !visible))
            {
                throw new ApplicationException("Timeout occured while looking for specified element");
            }
            return element;
        }

        public FieldType RecognizeFieldType(Element element, Document frame, ref List<string> availableOptions)
        {
            availableOptions = null;
            FieldType result;
            var div = element as Div;
            if (div?.Divs.Where(x => x.ClassName != null && x.ClassName.ToLower().Contains("ms-crm-Inline-Locked".ToLower())).FirstOrDefault() != null)
            {
                result = FieldType.Locked;
            }
            else if (element.GetAttributeValue("lookupstyle") == "single")
            {
                result = FieldType.Lookup;
            }
            else if (frame.SelectList(element.Id + "_i")?.Options?.Count > 0)
            {
                var options = frame.SelectList(element.Id + "_i")?.Options;
                availableOptions = options.ToList().Select(x => x.Text).Where(x => x != null).ToList();
                if ($" {element.ClassName.ToLower()} ".Contains(" bit "))
                    result = FieldType.TwoOptionsOptionSet;
                else
                    result = FieldType.OptionSet;
            }
            else if (element.GetAttributeValue("lookupstyle") == "subject")
            {
                result = FieldType.TreeOptionSet;
            }
            else
            {
                if ($" {element.ClassName.ToLower()} ".Contains(" datetime "))
                    result = FieldType.DateTimeTextField;
                else
                    result = FieldType.TextField;
            }
            return result;
        }

        public int SetVal(string search, string value, string by = "id", string frameId = "")
        {
            int result = 0;
            Element element = GetInputElement(search, by, frameId);
            var iFrame = GetCurrentIframe(frameId);
            element.WaitForComplete();
            List<string> availableOptions = null;
            var fieldType = RecognizeFieldType(element, iFrame, ref availableOptions);
            switch (fieldType)
            {
                case FieldType.Lookup:
                    result = SetValInLookupField(iFrame, element, value);
                    break;

                case FieldType.OptionSet:
                    result = SetValInOptionSet(iFrame, element, value);
                    break;

                case FieldType.TreeOptionSet:
                    result = SetValInTreeOptionSet(iFrame, element, value);
                    break;

                case FieldType.TwoOptionsOptionSet:
                    result = SetValInTwoOptionsOptionSet(iFrame, element, value);
                    break;

                case FieldType.TextField:
                    result = SetValInTextField(iFrame, element, value, 0);
                    break;

                case FieldType.DateTimeTextField:
                    result = SetValInDateTimeTextField(iFrame, element, value, 0);
                    break;

                case FieldType.Locked:
                    return 0;
            }
            return result;
        }

        public void WaitForLoad(string search, string by = "id", string iFrame = "", int timeout = 10000)
        {
            Ie.WaitForComplete();
            Element element = FindElement(search, by, iFrame);
            if (element != null)
            {
                element.WaitUntilExists(timeout / 1000 / 2);
                Settings.WaitForCompleteTimeOut = timeout / 2;
                element.WaitForComplete();
            }
            else
            {
                throw new ApplicationException("Element not found");
            }
        }

        public bool FieldIsValid(Element element)
        {
            if (element.InnerHtml.Contains("ms-crm-Inline-HasError"))
            {
                return false;
            }
            return true;
        }

        public bool FieldIsValid(string id, string by = "id", string frameId = "")
        {
            Element element = GetInputElement(id, by, frameId, true);
            return FieldIsValid(element);
        }

        public bool IsElementVisible(string id, string by = "id", string frameId = "")
        {
            Element element = FindElement(id, by, frameId);
            bool visible = element.Style.GetAttributeValue("display") != "none" &&
                           element.Style.GetAttributeValue("visibility") != "hidden";
            while (element != null && visible == true)
            {
                element = element.Parent;
                if (element != null)
                {
                    visible = element.Style.GetAttributeValue("display") != "none" &&
                              element.Style.GetAttributeValue("visibility") != "hidden";
                }
            }
            return visible;
        }

        public string GetState()
        {
            string result = string.Empty;
            var frame = GetCurrentIframe();
            if (frame != null)
            {
                var label = frame.Label("State_label");
                var lines = label.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
                result = lines.Last()?.ToLower()?.Trim() ?? string.Empty;
            }
            return result;
        }
    }

    public static class WatiNHelper
    {
        public static string MsCrmFindViaJquerySelector
        {
            get
            {
                return Resources.FindViaJquerySelector;
            }
        }

        public static string AreCommandBarsInitialized
        {
            get
            {
                return Resources.AreCommandBarsInitialized.Replace("\r", "").Replace("\n", "");
            }
        }

        public static T Eval<T>(this INativeDocument document, string code, string type)
        {
            IExpando window = GetWindow(document);
            PropertyInfo property = CreateOrGetProperty(window, "__lastEvalResult");
            document.RunScript("window.__lastEvalResult = " + code + ";", type);
            var result = (T)property.GetValue(window, null);
            return result;
        }

        private static PropertyInfo CreateOrGetProperty(IExpando expando, string name)
        {
            var currentProperty = expando.GetProperty(name, BindingFlags.GetProperty);
            return currentProperty ?? expando.AddProperty(name);
        }

        private static void RemoveProperty(IExpando expando, PropertyInfo property)
        {
            expando.RemoveMember(property);
        }

        private static IExpando GetWindow(INativeDocument document)
        {
            IEDocument ieDoc = document as IEDocument;
            IHTMLDocument2 htmlDoc = ieDoc.HtmlDocument;
            return htmlDoc.parentWindow as IExpando;
        }

        public static ElementCollection Elements(this Element self)
        {
            return (self as List)?.Elements ??
                   (self as Div)?.Elements ??
                   (self as ListItem)?.Elements ??
                   (self as ElementContainer<ListItem>)?.Elements;
        }

        public static bool IsJavascriptEvaluateTrue(this Browser browser, string javascript)
        {
            var evalResult = browser.Eval("if (" + javascript + ") {true;} else {false;}");
            return evalResult == "true";
        }

        public static string FindIdViaJQuery(this Browser browser, string cssSelector, string iFrame, int timeoutMs)
        {
            int start = Environment.TickCount;
            while (Environment.TickCount - start <= timeoutMs)
            {
                if (!browser.IsJavascriptEvaluateTrue("typeof('WatinSearchHelper') == 'function'"))
                {
                    var js = MsCrmFindViaJquerySelector.Replace("\n", "").Replace("\r", "");
                    browser.Eval(js);
                }
                var elementId = browser.Eval(string.Format("WatinSearchHelper.getElementId(\"{0}\", \"{1}\")", cssSelector, iFrame));

                if (elementId != "_no_element_" && !string.IsNullOrEmpty(elementId))
                    return elementId;
            }
            throw new ArgumentException("Unable to find element on page with selector '" + cssSelector + "'", "cssSelector");
        }
    }
}
