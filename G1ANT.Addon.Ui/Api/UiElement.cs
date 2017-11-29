using System;
using System.Windows.Automation;

using G1ANT.UiFramework;
using G1ANT.Language.Ui.Wizards;
using G1ANT.Language.Ui.Exceptions;

namespace G1ANT.Language.Ui.Api
{
    public class UiElement
    {
        AutomatedItem uiitem;
        public int Id;

        public AutomationElement Element;
        public UiElement(int id, AutomationElement ae)
        {
            Id = id;
            Element = ae;
        }
        public String Str { get; set; }

        public void Click(string path)
        {
            uiitem = new AutomatedItem(path, Element, UiWizard.Instance.checkBox1.Checked);

            try
            {
                uiitem.Actions.Click();
            }
            catch (ControlNotFoundException ex)
            {
                throw ex;
            }
        }

        public void SetValue(string s, string path)
        {
            uiitem = new AutomatedItem(path, Element, UiWizard.Instance.checkBox1.Checked);
            try
            {
                uiitem.Actions.Write(s);
            }
            catch (ControlNotFoundException ex)
            {
                throw ex;
            }
        }

        public string GetValue(string path)
        {
            uiitem = new AutomatedItem(path, Element, UiWizard.Instance.checkBox1.Checked);
            try
            {
                return uiitem.Actions.Read();
            }
            catch (ControlNotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
