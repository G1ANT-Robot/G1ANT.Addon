using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

using G1ANT.UiFramework;
using G1ANT.UiFramework.Utils;
using System.Drawing;
using G1ANT.Language.Ui.Wizards;
using G1ANT.Language.Ui.Exceptions;

namespace G1ANT.Language.Ui.Api
{
    public static class UiManager
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static readonly List<UiElement> uis = new List<UiElement>();
        public static UiElement CurrentUi { get; private set; }

        private static int GetNextElemId()
        {
            return uis.Any() ? uis.Max(x => x.Id) + 1 : 0;
        }

        public static int Attach(string windowname)
        {

            UiElement wrapper;
            int assignedId = GetNextElemId();
            try
            {
                wrapper = new UiElement(assignedId,
                  AutomationElement.FromHandle(UiFramework.WindowsApi.WinApi.FindWindowByCaption(IntPtr.Zero, windowname)));
            }
            catch
            {
                wrapper = new UiElement(assignedId, G1antUiCore.FindMain(windowname));
            }

            uis.Add(wrapper);
            CurrentUi = wrapper;
            if (wrapper.Element == null)
                return -1;
            return wrapper.Id;
        }

        internal static bool State(string wpath)
        {
            try
            {
                AutomatedItem ai = new AutomatedItem(wpath, CurrentUi.Element, UiWizard.Instance.checkBox1.Checked);
                if (ai.Element != null)
                {
                    try
                    {
                        TogglePattern togPattern;
                        object objPattern;
                        if (true == ai.Element.TryGetCurrentPattern(TogglePattern.Pattern, out objPattern))
                        {
                            togPattern = objPattern as TogglePattern;
                            return togPattern.Current.ToggleState == ToggleState.On;
                        }
                        else
                        {
                            logger.Error($"Ui Error: This is not a state control");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Ui Error: Cannot get state {ex.Message}");
                        return false;
                    }
                }
                else
                {
                    logger.Error($"Ui Error:Element was not found");
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.Error($"Ui Error:Element was not found{ex.Message}");
                throw ex;
            }
        }

        internal static Point GetRelativePosition(string value)
        {
            Point pos = new Point();
            try
            {
                AutomatedItem ai = new AutomatedItem(value, CurrentUi.Element, UiWizard.Instance.checkBox1.Checked);
                if (ai.Element != null)
                {
                    pos.X = (int)(ai.Element.Current.BoundingRectangle.X - CurrentUi.Element.Current.BoundingRectangle.X) + (int)(ai.Element.Current.BoundingRectangle.Size.Width / 2);
                    pos.Y = (int)(ai.Element.Current.BoundingRectangle.Y - CurrentUi.Element.Current.BoundingRectangle.Y) + (int)(ai.Element.Current.BoundingRectangle.Size.Height / 2);
                }
                return pos;
            }
            catch (Exception ex)
            {
                logger.Error($"Ui Error: Problem finding relative position: {ex.Message}");
                throw ex;
            }
        }

        public static bool Wait(string path)
        {
            AutomatedItem ai = new AutomatedItem(path, CurrentUi.Element, UiWizard.Instance.checkBox1.Checked);
            if (ai.Element != null)
                return true;
            else
                return false;
        }

        internal static Point GetGlobalPosition(string value)
        {
            {
                Point pos = new Point();
                try
                {
                    AutomatedItem ai = new AutomatedItem(value, CurrentUi.Element, UiWizard.Instance.checkBox1.Checked);
                    if (ai.Element != null)
                    {
                        pos.X = (int)(ai.Element.Current.BoundingRectangle.X) + (int)(ai.Element.Current.BoundingRectangle.Size.Width / 2);
                        pos.Y = (int)(ai.Element.Current.BoundingRectangle.Y) + (int)(ai.Element.Current.BoundingRectangle.Size.Height / 2);
                    }
                    return pos;
                }
                catch (Exception ex)
                {
                    logger.Error($"Ui Error: Problem while finding global position: {ex.Message}");
                    throw ex.GetBaseException();
                }
            }
        }

        public static bool SwitchUi(int id)
        {
            var tmpXls = uis.Where(x => x.Id == id).FirstOrDefault();
            CurrentUi = tmpXls ?? CurrentUi;
            return tmpXls != null;
        }

        public static void SetValue(string value, string wpath)
        {
            try
            {
                CurrentUi.SetValue(value,wpath);
            }
            catch (ControlNotFoundException ex)
            {
                throw ex;
            }
        }

        public static string GetValue(string wpath)
        {
            try
            {
               return CurrentUi.GetValue(wpath);
            }
            catch (ControlNotFoundException ex)
            {
                throw ex;
            }
        }

        public static void Click(string wpath)
        {
            try
            {
                CurrentUi.Click(wpath);
            }
            catch (ControlNotFoundException ex)
            {

                throw ex;
            }
        }
    }
}
