
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "outlook.close", Tooltip = "This command allows to close active outlook pogram. It must be initiated at the end of a process.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookCloseCommand : Command
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
        public OutlookCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(false));
            var outlookManager = OutlookManager.CurrentOutlook;
            outlookManager.Close();
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
        }
    }
}
