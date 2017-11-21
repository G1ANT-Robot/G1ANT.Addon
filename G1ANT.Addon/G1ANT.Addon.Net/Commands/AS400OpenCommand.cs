﻿using G1ANT.Language;
using System;
using System.Diagnostics;

namespace G1ANT.Addon.Net
{
    [Command(Name = "as400.open", Tooltip= "This command allows to open terminal to work with IBM AS/400 platform.", NeedsDelay = true, IsUnderConstruction = true)]
    public class AS400open : Command
    {
        public class Arguments : CommandArguments
        {

            [Argument(Required = true, Tooltip = "IP or Hostname required to connection")]
            public TextStructure Host { get; set; }

        }
        public string pathToTelnet = System.IO.Path.Combine(Environment.CurrentDirectory,
             @"..\..\..\G1ANT.Robot\Resources\telnet.exe");
        public string pathToPutty = System.IO.Path.Combine(Environment.CurrentDirectory,
             @"..\..\..\G1ANT.Robot\Resources\putty.exe");
        Process putty;
        public AS400open(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            string errorJump = arguments.ErrorJump != null && arguments.ErrorJump != null ? arguments.ErrorJump.ToString() : string.Empty;
            string host = arguments.Host.Value;
            //telnet = System.Diagnostics.Process.Start(pathToTelnet, host);
            putty = System.Diagnostics.Process.Start(pathToPutty, "-load AS400");
            System.Threading.Thread.Sleep(2000);
            //RobotWin32.ShowWindow(telnet.MainWindowHandle, RobotWin32.ShowWindowEnum.ShowNormal);          
            //IntPtr iHandle = RobotWin32.FindWindow(null, telnet.MainWindowTitle);
            RobotWin32.ShowWindow(putty.MainWindowHandle, RobotWin32.ShowWindowEnum.ShowNormal);
            IntPtr iHandle = RobotWin32.FindWindow(null, putty.MainWindowTitle);
            RobotWin32.SetForegroundWindow(iHandle);
        }
    }
}

