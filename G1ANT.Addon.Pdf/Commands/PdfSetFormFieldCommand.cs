using G1ANT.Language;

namespace G1ANT.Addon.Pdf
{
    [Command(Name = "pdf.setformfield")]
    public class PdfSetFormFieldCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Name { get; set; } = new TextStructure("");

            [Argument]
            public TextStructure Value { get; set; } = new TextStructure("");
        }
        public PdfSetFormFieldCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            PdfManager.SetFormField(arguments.Name.Value, arguments.Value.Value);
        }
    }
}
