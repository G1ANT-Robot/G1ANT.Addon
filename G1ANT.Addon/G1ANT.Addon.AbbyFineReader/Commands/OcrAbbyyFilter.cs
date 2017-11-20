using System;
using System.Collections.Generic;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using FREngine;
using GStructures = G1ANT.Language.Structures;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.filter", ToolTip = "This command allows to filter text from a document by font style")]
    public class OcrAbbyyFilter : CommandBase<OcrAbbyyFilter.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public GStructures.Integer DocumentID { get; set; } = null;

            [Argument(Required = false, Tooltip = "Flags of filter to applay, separated by '|'")]
            public GStructures.String Filter { get; set; } = null;

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

            FineReaderDocument.FilterFlags paramsFilter = FineReaderDocument.FilterFlags.none;

            string[] flagsString = arguments.Filter.Value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string flag in flagsString)
            {
                FineReaderDocument.FilterFlags currentFlag = FineReaderDocument.FilterFlags.none;
                try
                {
                    currentFlag = (FineReaderDocument.FilterFlags)Enum.Parse(typeof(FineReaderDocument.FilterFlags), flag.Trim(), true);
                    paramsFilter |= (FineReaderDocument.FilterFlags)Enum.Parse(typeof(FineReaderDocument.FilterFlags), flag.Trim(), true);
                }
                catch
                {
                    throw new ArgumentOutOfRangeException(nameof(arguments.Filter), currentFlag, $"{currentFlag} is not defined filter");
                }
            }

            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;

            FineReaderDocument document = manager.GetDocument(docID);

            document.ExtractData();

            GStructures.List filteredTexts = new GStructures.List(new List<GStructures.Structure>());
            foreach (string s in document.Filter(paramsFilter))
                filteredTexts.Value.Add(new GStructures.String(s));

            SetVariableValue(arguments.Result.Value, filteredTexts);
        }
    }
}
