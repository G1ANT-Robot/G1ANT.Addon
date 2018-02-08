﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Mscrm
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using G1ANT.Language;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.activate", Tooltip = "This command brings Microsoft Dynamics CRM Internet Explorer instance to the foreground.")]
    public class MsCrmActivateCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Arguments style defines the style of a window – maximized, minimized or restored (restore from minimized state)")]
            public TextStructure Style { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public MsCrmActivateCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var wrapper = MsCrmManager.CurrentCRM;
            if (wrapper == null)
                throw new ApplicationException("Could not activate Dynamics CRM instance. It has to be attached first.");
            IntPtr iHandle = wrapper.Ie.NativeBrowser.hWnd;
            Scripter.Log.Log(AbstractLogger.Level.Info, "Window '" + wrapper.Title + "' has been found");
            Scripter.LastWindow = (iHandle);
            RobotWin32.BringWindowToFront(iHandle);
            if (arguments.Style.Value.ToLower() == "maximize")
                RobotWin32.ShowWindow(iHandle, RobotWin32.ShowWindowEnum.Maximize);
            else if (arguments.Style.Value.ToLower() == "minimize")
                RobotWin32.ShowWindow(iHandle, RobotWin32.ShowWindowEnum.Minimize);
            else if (arguments.Style.Value.ToLower() == "normal")
                RobotWin32.ShowWindow(iHandle, RobotWin32.ShowWindowEnum.Restore);
        }
    }
}
