
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "outlook.send",Tooltip = "This command to send a prepared earlier message by 'outlook.newmessage'.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookSendCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }

        }
        public OutlookSendCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.CurrentOutlook; ;
            outlookManager.Send();
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
        }
    }
}
