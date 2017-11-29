using G1ANT.UiFramework.UiItems.ControlTypes;
using System;

namespace G1ANT.UiFramework.UiItems
{
    /// <summary>
    /// Class with methods to generate objects of proper class
    /// </summary>
    public static class G1ANTControlTypeFactory
    {   
    /// <summary>
    /// Method to recognise type of control and create matching object, to access proper methods
    /// </summary>
    /// <param name="controlTypeID">The Id of curent control type</param>
    /// <param name="automatedItem">Automated item connected to the control</param>
    /// <returns>Object with methods to use with control</returns>
        public static Object RecognizeAndCreate(int controlTypeID, AutomatedItem automatedItem)
        {
            switch (controlTypeID)
            {
                case 1:
                    return null;
                case 50007:
                    return new ListItem(controlTypeID, automatedItem);
                case 50011:
                    return new MenuItem(controlTypeID, automatedItem);
                case 50019:
                    return new TabItem(controlTypeID, automatedItem); 
                case 50010:
                    return new Menu(controlTypeID, automatedItem);
                default:
                    return new G1ANTControl(controlTypeID, automatedItem);
            }
        }
    }
}