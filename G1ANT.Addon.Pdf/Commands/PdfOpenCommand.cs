using G1ANT.Language;

namespace G1ANT.Addon.Pdf
{
    [Command(Name = "pdf.open")]
    public class PdfOpenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Path { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public PdfOpenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            int id = PdfManager.Open(arguments.Path.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(id));
        }
    }
}
