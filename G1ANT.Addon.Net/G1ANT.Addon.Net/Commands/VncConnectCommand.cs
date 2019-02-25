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

namespace G1ANT.Addon.Net
{
    [Command(Name = "vnc.connect",Tooltip = "This command allows to connect to machine with running VNC server using a remote desktop.", NeedsDelay = true, IsUnderConstruction = true)]
    public class VncConnectCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Ip or url of the machine")]
            public TextStructure Host { get; set; }

            [Argument(Required = true, Tooltip = "Port used to connect and allowed on the server side")]
            public TextStructure Port { get; set; }

            [Argument(Required = true, Tooltip = "Password used to connect to the server side")]
            public TextStructure Password { get; set; }

            [Argument(DefaultVariable = "timeoutremotedesktop")]
            public  override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(10);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

        }
        public VncConnectCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public string pathToVNC = System.IO.Path.Combine(AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName,
             @"\VNC.exe");
        Process testerApp;

        public void Execute(Arguments arguments)
        {
            string host = arguments.Host.Value;
            string port = arguments.Port.Value;
            string pass = arguments.Password.Value;

            if (host == string.Empty || port == string.Empty || pass == string.Empty)
                throw new ApplicationException("Host or port or pass is empty");


            testerApp = System.Diagnostics.Process.Start(pathToVNC, "-Scaling Fit -Encryption Server " + host + " " + port + " " + pass);
            RobotWin32.ShowWindow(testerApp.MainWindowHandle, RobotWin32.ShowWindowEnum.ShowNormal);
        }
    }
}
