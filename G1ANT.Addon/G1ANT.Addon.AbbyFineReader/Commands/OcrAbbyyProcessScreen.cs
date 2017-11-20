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
    [Command(Name = "ocrabbyy.processscreen", ToolTip = "This command allows to process part of a screan for further data extraction")]
    public class OcrAbbyyProcessScreen : CommandBase<OcrAbbyyProcessScreen.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument]
            public GStructures.Rectangle Area { get; set; } = new GStructures.Rectangle(System.Windows.Forms.Screen.PrimaryScreen.Bounds);

            [Argument]
            public GStructures.Bool Relative { get; set; } = new GStructures.Bool(false);

            [Argument]
            public GStructures.String Result { get; set; } = new GStructures.String("result");

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

            Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : Helpers.ToAbsoluteCoordinates(arguments.Area.Value);
            Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);

            AbbyyManager manager = AbbyyManager.Instance;
            Structures.FineReaderDocument imageDocument = null;

            using (MemoryStream stream = new MemoryStream())
            {
                partOfScreen.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Position = 0;
                imageDocument = manager.CreateDocument(stream);
            }

            manager.ProcessDocument(imageDocument, null, arguments.Language?.Value, arguments.LanguageWeight.Value, arguments.DictionaryWeight.Value, ListConverter.ExtractDictionary(arguments.Dictionary?.Value));

            SetVariableValue(arguments.Result.Value, new GStructures.Integer(imageDocument.ID));
        }
    }
}
