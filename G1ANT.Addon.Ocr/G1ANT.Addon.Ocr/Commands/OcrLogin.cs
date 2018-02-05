using G1ANT.Language;
using System.Collections.Generic;
using System.Linq;

namespace G1ANT.Language.Ocr
{
    [Command(Name = "ocr.login",
        Tooltip = "This command allows to login to the Google text recognition service.",
        IsUnderConstruction = true)]
    public class OcrLoginCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Json credential obtained from Google text recognition service.")]
            public TextStructure JsonCredential { get; set; }
        }

        public OcrLoginCommand(AbstractScripter scripter) : base(scripter) { }
        
        public void Execute(Arguments arguments)
        {
            GoogleCloudApi.JsonCredential = arguments.JsonCredential.Value;
        }
    }
}
