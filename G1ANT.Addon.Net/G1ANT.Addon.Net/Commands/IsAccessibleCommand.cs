/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Command(Name = "is.accessible", Tooltip = "This command checks if a host is accessible")]
    public class IsAccessibleCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of a host to be accessed")]
            public TextStructure Hostname { get; set; }

            [Argument(DefaultVariable = "timeoutconnect", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(1000);

            [Argument(Tooltip = "Name of a variable where the command's result will be stored: `true` if a host is accessible, `false` if it’s not")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
            
        }
        public IsAccessibleCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingReply reply = null;
            try
            {
                reply = pingSender.Send(arguments.Hostname.Value, timeout);
            }
            catch
            {
                throw new ArgumentException("Given host is not supported or cannot be used");
            }
            finally
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
            }

            bool result = reply != null && reply.Status == IPStatus.Success;
            if (result == false)
            {
                result = CheckConnection(arguments.Hostname.Value, 80, timeout);
            }

            if (result == false)
            {
                result = CheckConnection(arguments.Hostname.Value, 443, timeout);
            }

            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(result));
        }

        private bool CheckConnection(string host, int port, int timeout)
        {
            TcpClient tc = new TcpClient();
            try
            {
                return tc.ConnectAsync(host, port).Wait(timeout);
            }
            catch (Exception ex)
            {
                Scripter.Log.Log(AbstractLogger.Level.Error,"http connection :" + ex.Message);
            }
            finally
            {
                tc.Close();
            }
            return false;
        }
    }
}
