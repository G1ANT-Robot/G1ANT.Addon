using G1ANT.Language;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.switch",Tooltip = "This command allows to switch between word windows.", NeedsDelay = true, IsUnderConstruction = true)]

    public class WordSwitchCommand : Command
	{
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Specifies id of word window")]
            public IntegerStructure Id { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");          


        }
        public WordSwitchCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            int id = arguments.Id.Value;
            if (WordManager.Switch(id))
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.BooleanStructure(true));
            }
        }
    }
}
