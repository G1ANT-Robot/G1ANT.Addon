using System.Net.NetworkInformation;

using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Command(Name = "ping",Tooltip ="This command allows to ping specified IP and receives approximate round-trip time in milli-seconds.")]
    public class PingCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "IP address or hostname of pinging server")]
            public TextStructure Ip { get; set; } = new TextStructure("8.8.8.8");

            [Argument(DefaultVariable = "timeoutconnect", Tooltip = "Defines timeout for connecting")]
            public override int Timeout { get; set; }

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");
        }
        public PingCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingReply reply = pingSender.Send(arguments.Ip.Value, arguments.Timeout);

            if (reply.Status == IPStatus.Success)
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure((int)reply.RoundtripTime));
            }
            else
            {
                throw new PingException("Destination host unreachable");
            }
        }
    }
}
