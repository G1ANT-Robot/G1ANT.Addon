/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using System.Diagnostics;
using System.IO;

namespace G1ANT.Addon.Net
{
    [Command(Name = "vnc.connect",Tooltip = "This command connects to a remote machine with a running VNC server, using a remote desktop connection", NeedsDelay = true)]
    public class VncConnectCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "IP or URL address of the remote machine")]
            public TextStructure Host { get; set; }

            [Argument(Required = true, Tooltip = "Port used to connect to the remote machine")]
            public TextStructure Port { get; set; }

            [Argument(Required = true, Tooltip = "Password used to connect to the remote machine")]
            public TextStructure Password { get; set; }

            [Argument(DefaultVariable = "timeoutremotedesktop", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(10);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }
        public VncConnectCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public string pathToVNC = Path.Combine(AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName,
             @"VNC.exe");
        Process testerApp;

        public void Execute(Arguments arguments)
        {
            string host = arguments.Host.Value;
            string port = arguments.Port.Value;
            string pass = arguments.Password.Value;

            if (host == string.Empty || port == string.Empty || pass == string.Empty)
                throw new ApplicationException("Host or port or pass is empty");

            testerApp = System.Diagnostics.Process.Start(pathToVNC, "-Scaling Fit -Encryption Server " + host + " " + port + " " + pass);
            bool result = RobotWin32.ShowWindow(testerApp.MainWindowHandle, RobotWin32.ShowWindowEnum.ShowNormal);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(result));
        }
    }
}
