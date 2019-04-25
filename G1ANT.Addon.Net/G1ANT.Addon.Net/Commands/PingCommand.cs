/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System.Net.NetworkInformation;
using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Command(Name = "ping", Tooltip = "This command pings a specified IP address and returns an approximate round-trip time in milliseconds")]
    public class PingCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "IP address or a host name of a pinged server")]
            public TextStructure Ip { get; set; }

            [Argument(Tooltip = "Allows to ping multiple times; the command returns a rounded average of all pings")]
            public IntegerStructure Repeats { get; set; } = new IntegerStructure(1);

            [Argument(DefaultVariable = "timeoutconnect", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(1000);

            [Argument(Tooltip = "Name of a variable where approximate round trip time in milliseconds will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public PingCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            long sum = 0;
            for (int i = 0; i < arguments.Repeats.Value; i++)
            {

                PingReply reply = pingSender.Send(arguments.Ip.Value, (int)arguments.Timeout.Value.TotalMilliseconds);
                if (reply.Status == IPStatus.Success)
                {
                    sum += reply.RoundtripTime;
                }
                else if (reply.Status == IPStatus.TimedOut)
                {
                    throw new PingException("Destination host do not respond to ping");
                }
                else if (reply.Status == IPStatus.TimeExceeded)
                {
                    throw new PingException("Destination host connection has timed out");
                }
                else
                {
                    throw new PingException("Destination host unreachable");
                }
            }
            var roundedReplyTime = sum / arguments.Repeats.Value;
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure((int)roundedReplyTime));
        }
    }
}
