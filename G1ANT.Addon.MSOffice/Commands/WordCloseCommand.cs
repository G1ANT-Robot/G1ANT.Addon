
using G1ANT.Language;



namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.close",Tooltip = "This command closes word pogram that is currently active.", NeedsDelay = true, IsUnderConstruction = false)]
    public class WordCloseCommand : Command
	{
        public class Arguments : CommandArguments
        {
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
