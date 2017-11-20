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
    [Command(Name = "ocrabbyy.readtables", ToolTip = "This command allows to read all tables entries and process it as a list")]
    public class OcrAbbyyReadTables : CommandBase<OcrAbbyyReadTables.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public GStructures.Integer DocumentID { get; set; } = null;

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
            document.ExtractData();

            GStructures.List cellsText = new GStructures.List(new List<GStructures.Structure>());
            foreach (FineReaderCell c in document.Cells)
                cellsText.Value.Add(new GStructures.String(c.Text));

            SetVariableValue(arguments.Result.Value, cellsText);
        }
    }
}
