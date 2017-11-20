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
    [Command(Name = "ocrabbyy.processfile", ToolTip = "This command reads file and process it by Abbyy fine reader engine", IsUnderConstruction = true)]
    public class OcrAbbyyProcessFile : CommandBase<OcrAbbyyProcessFile.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path to a file to be processed")]
            public GStructures.String Path { get; set; }

            [Argument(Required = false, Tooltip = "Indecies of pages to be processed")]
            public GStructures.List Pages { get; set; } = null;

            [Argument]
            public GStructures.String Result { get; set; } = new GStructures.String("result");

            [Argument]
            public GStructures.String TablesCountResult { get; set; } = new GStructures.String("tablescountresult");

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public GStructures.String Language { get; set; } = null;

            [Argument]
            public GStructures.Integer LanguageWeight { get; private set; } = new GStructures.Integer(100);

            [Argument]
            public GStructures.List Dictionary { get; set; } = null;

            [Argument]
            public GStructures.Integer DictionaryWeight { get; private set; } = new GStructures.Integer(100);

            [Argument]
            public GStructures.Bool If { get; set; } = new GStructures.Bool(true);

            [Argument]
            public GStructures.String ErrorJump { get; set; }

            [Argument]
            public GStructures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            if ((arguments.Language?.Value == null) && (arguments.Dictionary?.Value == null))
            {
                arguments.Language = new GStructures.String("English");
            }

            AbbyyManager manager = AbbyyManager.Instance;

            FineReaderDocument document = manager.CreateDocument(arguments.Path.Value);

            List<int> pageIndices = null;
            if (arguments.Pages != null && arguments.Pages.Value != null)
            {
                pageIndices = new List<int>(arguments.Pages.Value.Count);
                foreach (GStructures.Structure o in arguments.Pages.Value)
                    pageIndices.Add(((GStructures.Integer)o).Value - 1);
            }

            manager.ProcessDocument(document, pageIndices, arguments.Language?.Value, arguments.LanguageWeight.Value, arguments.DictionaryWeight.Value, ListConverter.ExtractDictionary(arguments.Dictionary?.Value));
            var a = document.Tables.Count;
            SetVariableValue(arguments.Result.Value, new GStructures.Integer(document.ID));
            SetVariableValue(arguments.TablesCountResult.Value, new GStructures.Integer(document.Tables.Count));
        }
    }
}
