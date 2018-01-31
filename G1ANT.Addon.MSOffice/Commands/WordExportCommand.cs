using G1ANT.Language;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "word.export", Tooltip = "This command exports document from currently active word instance to specified file (in either pdf or xps format). ", NeedsDelay = true, IsUnderConstruction = false)]

    public class WordExportCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "New path")]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

            [Argument(Tooltip = "Type to export to (it can be either pdf or xps), if not specified type is inferred from file path extension")]
            public TextStructure Type { get; set; } = new TextStructure(string.Empty);
        }
        public WordExportCommand(AbstractScripter scripter) : base(scripter)
        {
        }


        public void Execute(Arguments arguments)
        {
            string path = arguments.Path.Value;
            string type = arguments.Type != null ? arguments.Type.Value : null;
            WordWrapper wordWrapper = WordManager.CurrentWord;
            wordWrapper.Export(path, type);
        }
    }
}
