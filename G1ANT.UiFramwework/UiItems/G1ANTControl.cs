using G1ANT.UiFramework.Utils;

using System;
using System.Windows.Automation;

namespace G1ANT.UiFramework.UiItems
{   /// <summary>
/// Main class with defaul implementation of ui methods
/// </summary>
    public class G1ANTControl
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="controlTypeID"></param>
     /// <param name="automatedItem"></param>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public G1ANTControl(int controlTypeID, AutomatedItem automatedItem)
        {
            Ptr = automatedItem;
            G1antControlType = ControlType.LookupById(controlTypeID);
            if (G1antControlType == null)
                logger.Error($"Ui Framework: Cannot find control type {controlTypeID}");
        }
        public static ControlType G1antControlType { get; protected set; }

        public AutomatedItem Ptr { get; set; }
        /// <summary>
        /// Method used to get value from control
        /// </summary>
        /// <returns>Value as String</returns>
        public virtual string Read()
        {
            try
            {
                var valuePattern = Ptr.Element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                return valuePattern.Current.Value;
            }
            catch (Exception ex)
            {
                logger.Error($"Ui Framework: Cannot get value by value pattern: {ex.Message}");
                return G1antUiCore.GetTextFromField(Ptr.Handler);
            }
        }
        /// <summary>
        /// Metod used to set value in control
        /// </summary>
        /// <param name="mes">Value to set</param>
        public virtual void Write(string mes)
        {
            try
            {
                var valuePattern = Ptr.Element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                valuePattern.SetValue(mes);
            }
            catch (Exception ex)
            {
                logger.Error($"Ui Framework: Cannot set value by value pattern: {ex.Message}");
                G1antUiCore.InsertText(Ptr.Handler, mes);
            }
        }
        /// <summary>
        /// Method used to Clic on controls
        /// </summary>
        /// 
        public virtual void Click()
        {
            try
            {
                if (Ptr.Element != null)
                {
                    if (Ptr.Handler != IntPtr.Zero)
                    {
                        logger.Info($"Ui Framework: {Ptr.Element.Current.AutomationId} Trying to click by click");
                        G1antUiCore.Click(Ptr.Handler);
                    }
                    else
                    {
                        logger.Error($"Ui Framework: {Ptr.Element.Current.AutomationId} Handler ERROR IntPtr.Zero in click method: try to invoke patern");
                        var invokePattern = Ptr.Element.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                        logger.Info($"Ui Framework: {Ptr.Element.Current.AutomationId} Trying to click by invoke patern");
                        invokePattern.Invoke();
                        logger.Info($"Ui Framework: {Ptr.Element.Current.AutomationId} Clicked by invoke patern");
                    }
                }
            }
            catch
            {
                logger.Error($"Ui Framework: Filed to click element {Ptr.Element.Current.Name} {Ptr.Element.Current.AutomationId}");
            }
        }
        /// <summary>
        /// Method used to clic in child element of curent control
        /// </summary>
        /// <param name="childid">Id of child to click</param>
        public virtual void ClickChild(int childid)
        {
            if (Ptr.Children.Count > 0)
            {
                var invokePattern = Ptr.Children[childid].GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                invokePattern.Invoke();
            }
        }

    }
}