using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using GStructures = G1ANT.Language.Structures;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.getcellinfo", ToolTip = "This command allows to retrive information about table cell.")]
    public class OcrAbbyyGetCellInfo : CommandBase<OcrAbbyyGetCellInfo.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public GStructures.Integer DocumentID { get; set; } = null;

            [Argument(Required = false, Tooltip = "Index of a table in document")]
            public GStructures.Integer TableIndex { get; set; } = new GStructures.Integer(1);

            [Argument(Required = true, Tooltip = "Position of the cell in the table in format row,column")]
            public GStructures.String Position { get; set; } = null;

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
            FineReaderDocument document = manager.GetDocument(docID);
            int row = 0;
            int column = 0;

            string[] position = arguments.Position.Value.Split(',');

            row = int.Parse(position[0]);
            column = int.Parse(position[1]);

            System.Drawing.Point cellSpans = new System.Drawing.Point(
                document.Tables[arguments.TableIndex.Value - 1][row - 1, column - 1].RowSpan,
                document.Tables[arguments.TableIndex.Value - 1][row - 1, column - 1].ColumnSpan);

            SetVariableValue(arguments.Result.Value, new GStructures.Point(cellSpans));
        }
    }
}
