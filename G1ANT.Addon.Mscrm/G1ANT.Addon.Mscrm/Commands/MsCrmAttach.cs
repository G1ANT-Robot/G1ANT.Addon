/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Mscrm
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using G1ANT.Language;
using System;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.attach", Tooltip = "This command connects to open instance of CRM in Internet Explorer.")]
    public class MsCrmAttachCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Phrase { get; set; } = new TextStructure("crm4.dynamics.com");

            [Argument]
            public TextStructure By { get; set; } = new TextStructure("url"); //can be also url

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result"); // 

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }


        }
        public MsCrmAttachCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var wrapper = MsCrmManager.AttachToExistingCRM(arguments.Phrase.Value, arguments.By.Value);
            if (wrapper != null && wrapper.Ie != null)
            {
                OnScriptEnd = () => { MsCrmManager.Detach(wrapper); };
                Scripter.Variables.Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
                wrapper.ActivateTab(arguments.Phrase.Value, arguments.By.Value);
                ActivateBrowserWindow(wrapper);
            }
            else
            {
                throw new ApplicationException("Cannot attach to MS Dynamics CRM, please check is IE with " + arguments.Phrase.Value + " opened.");
            }
        }

        private void ActivateBrowserWindow(MsCrmWrapper wrapper)
        {
            if (wrapper == null)
                throw new ApplicationException("Could not activate Dynamics CRM instance. It has to be attached first.");
            IntPtr iHandle = wrapper.Ie.NativeBrowser.hWnd;

            Scripter.Log.Log(AbstractLogger.Level.Info, "Window '" + wrapper.Title + "' has been found");
            Scripter.LastWindow = (iHandle);
            RobotWin32.BringWindowToFront(iHandle);
        }
    }
}
