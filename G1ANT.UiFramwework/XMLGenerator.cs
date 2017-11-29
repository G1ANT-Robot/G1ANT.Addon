using System;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Xml;

namespace G1ANT.UiFramework
{
    public class XMLGenerator
    {
        /// <summary>
        /// Method used to generate XML structure of given window
        /// </summary>
        /// <param name="element">Automation Element - current window</param>

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static bool XmlIt(AutomationElement element, Boolean id)
        {
            logger.Info($"Ui Framework: Making Xml Structure of the application {element?.Current.Name}");
            if (element == null)
            {
                MessageBox.Show("This application is not supported yet, or application is not runing");
                logger.Error($"UiFramework: Cannot find window to make xml structure, element == null");
                return false;
            }
            else
            {
                try
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.CheckCharacters = false;
                    settings.CloseOutput = true;
                    XmlWriter writer = XmlWriter.Create("window.xml", settings);
                    writer.WriteStartElement(element.Current.ControlType.ProgrammaticName.Remove(0, 12).ToString());
                    if (!id)
                        writer.WriteAttributeString("Id", element.Current.AutomationId.ToString());
                    writer.WriteAttributeString("Type", element.Current.ControlType.Id.ToString());
                    writer.WriteAttributeString("Name", element.Current.Name.ToString());
                    WriteChildren(element, writer, id);
                    writer.WriteEndElement();
                    writer.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    logger.Error($"UiFramework: {ex.Message}, while making xml structure of aplication");
                    return false;
                }
            }
        }

        /// <summary>
        /// Method used to find children of element
        /// </summary>
        /// <param name="ae">Element we are looking chilren for</param>
        /// <returns>AutomationElementCollection with children of given element</returns>
        private static AutomationElementCollection FindChildren(AutomationElement ae)
        {
            return ae.FindAll(TreeScope.Children, Condition.TrueCondition);
        }
        /// <summary>
        /// Method to write children of element to xml
        /// </summary>
        /// <param name="element"></param>
        /// <param name="writer"></param>
        private static void WriteChildren(AutomationElement element, XmlWriter writer, bool id)
        {
            foreach (AutomationElement elem in FindChildren(element))
            {
                if (isFindable(elem, id))
                {
                    writer.WriteStartElement(elem.Current.ControlType.ProgrammaticName.Remove(0, 12));
                    if (elem.Current.ControlType.ProgrammaticName.Remove(0, 12) == "ListItem")
                    {
                        var b = elem.Current.Name;
                    }

                    if (!id)
                        if (!string.IsNullOrEmpty(elem.Current.AutomationId))
                            writer.WriteAttributeString("Id", elem.Current.AutomationId);

                    if (!string.IsNullOrEmpty(elem.Current.ControlType.Id.ToString()))
                        writer.WriteAttributeString("Type", elem.Current.ControlType.Id.ToString());

                    if (!string.IsNullOrEmpty(elem.Current.Name))
                        writer.WriteAttributeString("Name", elem.Current.Name);

                    if (haveTwin(elem, element, out int index, id))
                    {
                        writer.WriteAttributeString("Index", index.ToString());
                    }

                    WriteChildren(elem, writer, id);
                    writer.WriteEndElement();
                }
                else if (!isFindable(elem, id))
                {
                    writer.WriteStartElement(elem.Current.ControlType.ProgrammaticName.Remove(0, 12));
                    if (!id)
                        if (!string.IsNullOrEmpty(elem.Current.AutomationId))
                            writer.WriteAttributeString("Id", elem.Current.AutomationId);

                    if (!string.IsNullOrEmpty(elem.Current.ControlType.Id.ToString()))
                        writer.WriteAttributeString("Type", elem.Current.ControlType.Id.ToString());

                    if (!string.IsNullOrEmpty(elem.Current.Name))
                        writer.WriteAttributeString("Name", elem.Current.Name);

                    var b = (haveTwin(elem, element, out int index, id));

                    writer.WriteAttributeString("Index", index.ToString());


                    WriteChildren(elem, writer, id);
                    writer.WriteEndElement();
                }
                else WriteChildren(elem, writer, id);
            }
        }
        private static bool isFindable(AutomationElement elem, Boolean id)
        {
            int i = 0;

            if (!(string.IsNullOrEmpty(elem.Current.Name))) i++;
            if (!id)
                if (!(string.IsNullOrEmpty(elem.Current.AutomationId))) i++;
            if (!(string.IsNullOrEmpty(elem.Current.ControlType?.ProgrammaticName))) i++;
            if (i < 2) return false;
            else return true;
        }
        private static bool haveTwin(AutomationElement elem, AutomationElement Parent, out int index, bool id)
        {
            AutomationElementCollection found;
            if (id)
            {
                PropertyCondition condition22 = new PropertyCondition(AutomationElement.NameProperty, elem.Current.Name);
                PropertyCondition condition23 = new PropertyCondition(AutomationElement.ControlTypeProperty, elem.Current.ControlType);
                found = Parent.FindAll(TreeScope.Children, new AndCondition(condition22, condition23));
            }
            else
            {
                PropertyCondition condition21 = new PropertyCondition(AutomationElement.AutomationIdProperty, elem.Current.AutomationId);
                PropertyCondition condition22 = new PropertyCondition(AutomationElement.NameProperty, elem.Current.Name);
                PropertyCondition condition23 = new PropertyCondition(AutomationElement.ControlTypeProperty, elem.Current.ControlType);
                found = Parent.FindAll(TreeScope.Children, new AndCondition(condition21, condition22, condition23));
            }


            if (found.Count == 1)
            {
                index = 0;
                return false;
            }

            else
            {
                for (int i = 0; i < found.Count; i++)
                {
                    if (found[i] == elem)
                    {
                        index = i;
                        return true;
                    }
                }
                index = -1;
                return false;
            }
        }
    }
}