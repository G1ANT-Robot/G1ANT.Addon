using G1ANT.Language;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.save",Tooltip = "This command saves currently active word document.", NeedsDelay = true, IsUnderConstruction = false)]

    public class WordSaveCommand : Command
	{
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "specifies word document save path")]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

        }

        public WordSaveCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            WordWrapper wordWrapper = WordManager.CurrentWord;
            wordWrapper.Save(arguments.Path.Value);
        }
    }
}
