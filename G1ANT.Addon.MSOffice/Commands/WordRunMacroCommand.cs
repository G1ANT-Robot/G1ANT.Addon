
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.runmacro",Tooltip = "This command allows to run macro in currently active word instance.", NeedsDelay = true, IsUnderConstruction = true)]
    public class WordRunMacroCommand : Command
	{
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Name of the macro")]
            public TextStructure Name { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Arguments for specified macro")]
            public TextStructure Args { get; set; } 

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }

        }

        public WordRunMacroCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            string macroName = arguments.Name.Value;

            string args = arguments.Args != null ? arguments.Args.Value : null;
            var wrapper = WordManager.CurrentWord;
            wrapper.RunMacro(macroName, args);
        }
    }
}
