using G1ANT.Language;


namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.close", Tooltip = "This command close all documents processed by abbyy engine")]
    public class OcrAbbyyCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of a document to be closed, if not specified close all documents and unloads abbyy engine")]
            public IntegerStructure Document { get; set; }
        }
        public OcrAbbyyCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            if (arguments.Document != null)
            {
                AbbyyManager.Instance.GetDocument(arguments.Document.Value).Close();
            }
            else
            {
                AbbyyManager.Instance.Dispose();
            }
        }
    }
}
