
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.open",Tooltip = "This command opens word program.", NeedsDelay = true, IsUnderConstruction = false)]
    public class WordOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }

        }

        public WordOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            string path = arguments.Path.Value;

            WordWrapper wordWraper = WordManager.AddWord();
            wordWraper.Open(path);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.IntegerStructure(wordWraper.Id));
        }
    }
}
