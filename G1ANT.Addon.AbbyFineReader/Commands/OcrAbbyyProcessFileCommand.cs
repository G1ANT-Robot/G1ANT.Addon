using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FREngine;
using System.Text.RegularExpressions;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.processfile", Tooltip = "This command reads file and process it by Abbyy fine reader engine", IsUnderConstruction = true)]
    public class OcrAbbyyProcessFileCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path to a file to be processed")]
            public TextStructure Path { get; set; }

            [Argument(Required = false, Tooltip = "Indecies of pages to be processed")]
            public ListStructure Pages { get; set; } = null;

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument]
            public TextStructure TablesCountResult { get; set; } = new TextStructure("tablescountresult");

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public TextStructure Language { get; set; } = null;

            [Argument]
            public IntegerStructure LanguageWeight { get; private set; } = new IntegerStructure(100);

            [Argument(Tooltip = "List of suggested words that will have higher priority than random strings.")]
            public ListStructure Dictionary { get; set; } = null;

            [Argument]
            public IntegerStructure DictionaryWeight { get; private set; } = new IntegerStructure(100);

 
        }
        public OcrAbbyyProcessFileCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            if ((arguments.Language?.Value == null) && (arguments.Dictionary?.Value == null))
            {
                arguments.Language = new TextStructure("English");
            }

            AbbyyManager manager = AbbyyManager.Instance;

            FineReaderDocument document = manager.CreateDocument(arguments.Path.Value);

            List<int> pageIndices = null;
            if (arguments.Pages != null && arguments.Pages.Value != null)
            {
                pageIndices = new List<int>(arguments.Pages.Value.Count);
                foreach (Structure o in arguments.Pages.Value)
                    pageIndices.Add(((IntegerStructure)o).Value - 1);
            } 

            manager.ProcessDocument(document, pageIndices, arguments.Language?.Value, arguments.LanguageWeight.Value, arguments.DictionaryWeight.Value, ListConverter.ExtractDictionary(arguments.Dictionary?.Value));
            var a = document.Tables.Count;
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(document.ID));
            Scripter.Variables.SetVariableValue(arguments.TablesCountResult.Value, new IntegerStructure(document.Tables.Count));
        }
    }
}
