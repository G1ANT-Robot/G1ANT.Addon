﻿





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
    [Command(Name = "ocrabbyy.find", Tooltip = "This command allows to find the text on the active screen and return it's position as a 'rectangle' format. If the text will not be found, the result will be Rectangle(-1,-1,-2,-2).")]
    public class OcrAbbyyFindCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Text that you want to find in the screen. ")]
            public TextStructure Search { get; set; }

            [Argument(Required = false, Tooltip = "Id of a processed document returned by a call to processfile command. If not specified last processed document is used.")]
            public IntegerStructure DocumentID { get; set; } = null;

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

 
        }
        public OcrAbbyyFindCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        private AbbyyManager manager = null;

        public void Execute(Arguments arguments)
        {
            manager = AbbyyManager.Instance;
            int docID = arguments.DocumentID == null ? manager.CurentDocumentCount : arguments.DocumentID.Value;
            var doc = manager.GetDocument(docID);

            List<Rectangle> rectangles = doc.FindPosition(arguments.Search.Value);

            ListStructure matchesRectangles = new ListStructure(new List<Structure>());

            foreach (Rectangle r in rectangles)
                matchesRectangles.Value.Add(new RectangleStructure(r));

            Scripter.Variables.SetVariableValue(arguments.Result.Value, matchesRectangles);
        }
    }
}
