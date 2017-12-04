using G1ANT.Language;

namespace G1ANT.Addon.Pdf
{
    [Command(Name = "pdf.close")]
    public class PdfCloseCommand : Command
    {
        public class Arguments : CommandArguments
        {
        }
        public PdfCloseCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            PdfManager.Close();
        }
    }
}
