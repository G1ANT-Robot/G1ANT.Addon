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
    [Command(Name = "ping", Tooltip = "This command allows to ping specified IP and receives approximate round-trip time in milli-seconds.")]
    public class PingCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "IP address or hostname of pinging server")]
            public TextStructure Ip { get; set; } = new TextStructure("8.8.8.8");

            [Argument(Tooltip = "Allows to ping multiple times. Command returns rounded value of all pings. Default = 1")]
            public IntegerStructure Repeats { get; set; } = new IntegerStructure(1);

            [Argument(DefaultVariable = "timeoutconnect", Tooltip = "Defines timeout for connecting")]
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(1000);

            [Argument]
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
