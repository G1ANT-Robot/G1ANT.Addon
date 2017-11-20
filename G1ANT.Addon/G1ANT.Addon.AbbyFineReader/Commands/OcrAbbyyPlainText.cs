using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using GStructures = G1ANT.Language.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.plaintext", ToolTip = "This command extract text from processed document")]
    public class OcrAbbyyPlainText : CommandBase<OcrAbbyyPlainText.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public GStructures.Integer DocumentID { get; set; } = null;

            [Argument(Tooltip = "Method of text recognition to use. Either 'linebyline' or 'structured'. By default, 'structured'.")]
            public GStructures.String Method { get; set; } = new GStructures.String("structured");

            [Argument]
            public GStructures.String Result { get; set; } = new GStructures.String("result");

            [Argument]
            public GStructures.Bool If { get; set; } = new GStructures.Bool(true);

            [Argument]
            public GStructures.String ErrorJump { get; set; }

            [Argument]
            public GStructures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
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
            SetVariableValue(arguments.Result.Value, new GStructures.String(output));
        }
    }
}
