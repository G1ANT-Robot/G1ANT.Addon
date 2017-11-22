
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "outlook.open", Tooltip = "This command allows to open the outlook program. It must be always executed before other outlook command will be used.", NeedsDelay = true, IsUnderConstruction = false)]
    public class OutlookOpenCommand : Command
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
        public OutlookOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.AddOutlook();
            outlookManager.Open();
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
        }
    }
}
