using System;
using System.Windows.Automation;

namespace G1ANT.UiFramework.Utils
{
    public class EventMonitor
    {
        object h;
        private EventMonitor() { }

        public static EventMonitor SubscribeToPropertyChangeEvent(AutomationElement element, Action<object, AutomationEventArgs> methodtocall, params AutomationProperty[] properties)
        {
            EventMonitor monitor = new EventMonitor();
            monitor.h = new AutomationPropertyChangedEventHandler(methodtocall);
            Automation.AddAutomationPropertyChangedEventHandler(element, TreeScope.Element, (AutomationPropertyChangedEventHandler)monitor.h, properties);
            return monitor;
        }
        public static EventMonitor SubscribeToTextSelectionChangedEvent(AutomationElement element, Action<object, AutomationEventArgs> methodtocall)
        {
            EventMonitor monitor = new EventMonitor();
            monitor.h = new AutomationEventHandler(methodtocall);
            Automation.AddAutomationEventHandler(TextPattern.TextSelectionChangedEvent,
                                    element,
                                    TreeScope.Element,
                                     (AutomationEventHandler)monitor.h);
            return monitor;
        }
        public static EventMonitor SubscribeToTextChangedEvent(AutomationElement element, Action<object, AutomationEventArgs> methodtocall)
        {//todo repair
            AutomationPattern[] pats = element.GetSupportedPatterns();
            AutomationProperty[] props = element.GetSupportedProperties();
            EventMonitor monitor = new EventMonitor();
            monitor.h = new AutomationEventHandler(methodtocall);
            Automation.AddAutomationEventHandler(TextPattern.TextChangedEvent,
                                    element,
                                    TreeScope.Subtree,
                                     (AutomationEventHandler)monitor.h);
            return monitor;
        }

        public static EventMonitor SubscribeToMenuOpenEvent(AutomationElement element, Action<object, AutomationEventArgs> methodtocall)
        {
            EventMonitor monitor = new EventMonitor();
            monitor.h = new AutomationEventHandler(methodtocall);
            Automation.AddAutomationEventHandler(AutomationElement.MenuOpenedEvent,
                                    element,
                                    TreeScope.Descendants | TreeScope.Element,
                                    (AutomationEventHandler)monitor.h);
            return monitor;
        }

        public static EventMonitor SubscribeToMenuCloseEvent(AutomationElement element, AutomationEventHandler UIAEventHandler, Action<object, AutomationEventArgs> methodtocall)
        {
            EventMonitor monitor = new EventMonitor();
            monitor.h = new AutomationEventHandler(methodtocall);
            Automation.AddAutomationEventHandler(AutomationElement.MenuClosedEvent,
                                    element,
                                    TreeScope.Descendants | TreeScope.Element,
                                    (AutomationEventHandler)monitor.h);
            return monitor;
        }

        public static EventMonitor SubscribeToFocusChangedEvent(Action<object, AutomationEventArgs> methodtocall)
        {
            EventMonitor monitor = new EventMonitor();
            monitor.h = new AutomationFocusChangedEventHandler(methodtocall);
            Automation.AddAutomationFocusChangedEventHandler((AutomationFocusChangedEventHandler)monitor.h);
            return monitor;
        }
#region overlayWindow
        public static void Monitorsize(AutomationElement element, Action<object, AutomationEventArgs> methodtocall)
        {
            AutomationPropertyChangedEventHandler UIAEventHandler = new AutomationPropertyChangedEventHandler(methodtocall);

            Automation.AddAutomationPropertyChangedEventHandler(element,TreeScope.Element, UIAEventHandler, AutomationElement.BoundingRectangleProperty);
        }
#endregion
    }
}