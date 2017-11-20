using G1ANT.Interop;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using GStructures = G1ANT.Language.Structures;
using G1ANT.Sdk.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FREngine;
using System.Text.RegularExpressions;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.gettextparagraphs", ToolTip = "This command extract paragraphs containing text from specified file")]
    public class OcrAbbyyGetTextInParagraphs : CommandBase<OcrAbbyyGetTextInParagraphs.Arguments>
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
            List<FineReaderParagraph> paragraphs = document.Paragraphs;

            GStructures.List paragraphsList = new GStructures.List(new List<GStructures.Structure>());

            foreach (FineReaderParagraph p in paragraphs)
                paragraphsList.Value.Add(new GStructures.String(p.Text));

            SetVariableValue(arguments.Result.Value, paragraphsList);
        }
    }
}
