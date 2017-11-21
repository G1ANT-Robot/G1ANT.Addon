using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.plaintext", Tooltip = "This command extract text from processed document")]
    public class OcrAbbyyPlainTextCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public IntegerStructure DocumentID { get; set; } = null;

            [Argument(Tooltip = "Method of text recognition to use. Either 'linebyline' or 'structured'. By default, 'structured'.")]
            public TextStructure Method { get; set; } = new TextStructure("structured");

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");

 
        }
        public OcrAbbyyPlainTextCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            AbbyyManager manager = AbbyyManager.Instance;
            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;
            var doc = manager.GetDocument(docID);
            string output = string.Empty;
            switch (arguments.Method.Value.ToLower())
            {
                case "structured":
                    output = doc.GetAllText();
                    break;

                case "linebyline":
                    output = doc.GetLinesText();
                    break;

                default:
                    throw new ArgumentException("Wrong method argument. It accepts either 'structured' or 'linebyline' value.");
            }
           Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(output));
        }
    }
}
