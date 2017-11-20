
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.close",Tooltip = "This command closes word pogram that is currently active.", NeedsDelay = true, IsUnderConstruction = false)]
    public class WordCloseCommand : Command
	{
        public class Arguments : CommandArguments
        {
            [Argument]
            public BooleanStructure If { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure ErrorJump { get; set; }

            [Argument]
            public TextStructure ErrorMessage { get; set; }
        }
        public WordCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            WordWrapper wordWrapper = WordManager.CurrentWord;
            wordWrapper.Close();
        }
    }
}
