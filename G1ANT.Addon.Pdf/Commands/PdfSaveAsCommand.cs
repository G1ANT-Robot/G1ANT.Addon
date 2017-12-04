using G1ANT.Language;

namespace G1ANT.Addon.Pdf
{
    [Command(Name = "pdf.saveas")]
    public class PdfSaveAsCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);
        }
        public PdfSaveAsCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            PdfManager.SaveAs(arguments.Path.Value);
        }
    }
}
