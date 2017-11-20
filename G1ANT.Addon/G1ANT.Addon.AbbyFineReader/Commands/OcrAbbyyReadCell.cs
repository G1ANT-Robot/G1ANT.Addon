using G1ANT.Interop;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using GStructures = G1ANT.Language.Structures;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;
using G1ANT.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FREngine;
using System.Text.RegularExpressions;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.readcell", ToolTip = "This command allows to read row column indexed cell from specific table in the document")]
    public class OcrAbbyyReadCell : CommandBase<OcrAbbyyReadCell.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public GStructures.Integer DocumentID { get; set; } = null;

            [Argument(Required = true, Tooltip = "Index of a table in document")]
            public GStructures.Integer TableIndex { get; set; } = null;

            //[Argument(Required = true, Tooltip = "Index of a row in the table")]
            //public GStructures.Integer Row { get; set; } = null;

            //[Argument(Required = true, Tooltip = "Index of a column in the table")]
            //public GStructures.Integer Column { get; set; } = null;

            [Argument(Required = true, Tooltip = "offset to be added to get proper value in format row,column")]
            public GStructures.String Position { get; set; } = null;

            [Argument(Tooltip = "offset to be added to get proper value in format row,column")]
            public GStructures.String Offset { get; set; } = null;

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
            int docID = arguments.DocumentID?.Value ?? manager.CurentDocumentCount;
            FineReaderDocument document = manager.GetDocument(docID);
            int row = 0;
            int column = 0;
            int rowOffset = 0;
            int columnOffset = 0;

            var position = arguments.Position.Value.Split(',');

            row = int.Parse(position[0]);
            column = int.Parse(position[1]);

            if (!string.IsNullOrEmpty(arguments.Offset?.Value))
            {
                var positionOffset = arguments.Offset.Value.Split(',');
                rowOffset = int.Parse(positionOffset[0]);
                columnOffset = int.Parse(positionOffset[1]);
            }

            string cellTextValue = string.Empty;
            try
            {
                cellTextValue = document.Tables[arguments.TableIndex.Value - 1][row - 1 + rowOffset, column - 1 + columnOffset].Text ?? string.Empty;
            }
            catch
            {
            }

            GStructures.String cellText = new GStructures.String(cellTextValue);

            SetVariableValue(arguments.Result.Value, cellText);
        }
    }
}
