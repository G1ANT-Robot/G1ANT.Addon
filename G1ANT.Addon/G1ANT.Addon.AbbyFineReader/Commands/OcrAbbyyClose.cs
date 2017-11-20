using GStructures = G1ANT.Language.Structures;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;


namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.close", ToolTip = "This command close all documents processed by abbyy engine")]
    public class OcrAbbyyClose : CommandBase<OcrAbbyyClose.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Id of a document to be closed, if not specified close all documents and unloads abbyy engine")]
            public GStructures.Integer Document { get; set; }

            [Argument]
            public GStructures.Bool If { get; set; } = new GStructures.Bool(true);

            [Argument]
            public GStructures.String ErrorJump { get; set; }

            [Argument]
            public GStructures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
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
