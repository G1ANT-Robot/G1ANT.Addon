using G1ANT.Interop;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Sdk.Helpers;
using GStructures = G1ANT.Language.Structures;
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
    [Command(Name = "ocrabbyy.gettableposition", ToolTip = "This command allows to find indexes.")]
    public class OcrAbbyyGetTablePosition : CommandBase<OcrAbbyyGetTablePosition.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Text that you want to find in the screen. ")]
            public GStructures.String Search { get; set; }

            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public GStructures.Integer DocumentID { get; set; } = null;

            [Argument(Required = true, Tooltip = "Index of a table in document")]
            public GStructures.Integer TableIndex { get; set; } = null;

            [Argument]
            public GStructures.String Result { get; set; } = new GStructures.String("result");

            [Argument(DefaultVariable = "timeoutOcr")]
            public override GStructures.Integer Timeout { get; set; }

            [Argument]
            public GStructures.Bool If { get; set; } = new GStructures.Bool(true);

            [Argument]
            public GStructures.String ErrorJump { get; set; }

            [Argument]
            public GStructures.String ErrorMessage { get; set; }
        }

        private AbbyyManager manager = null;

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            manager = AbbyyManager.Instance;
            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;
            var doc = manager.GetDocument(docID);
            List<string> table = doc.Tables[arguments.TableIndex.Value].ReturnWordPosition(arguments.Search.Value);
            GStructures.List cells = new GStructures.List(new List<GStructures.Structure>());
            foreach (String t in table)
                cells.Value.Add(new GStructures.String(t));
            SetVariableValue(arguments.Result.Value, cells);
        }
    }
}
