
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.replace",Tooltip = "This command allows to replace any word in document.", NeedsDelay = true, IsUnderConstruction = true)]
    public class WordReplaceCommand : Command
	{
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Word to be found in document")]
            public TextStructure From { get; set; } = new TextStructure(string.Empty);

            [Argument(Required = true, Tooltip = "Word to be replaced in document")]
            public TextStructure To { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "If true then case sensitive")]
            public BooleanStructure MatchCase { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "If true tries to replace even if word is found as substring")]
            public BooleanStructure WholeWords { get; set; } = new BooleanStructure(false);

            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }

        }
        public WordReplaceCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var wrapper = WordManager.CurrentWord;
            wrapper.ReplaceWord(arguments.From.Value, arguments.To.Value, arguments.MatchCase.Value, arguments.WholeWords.Value);
        }
    }
}
