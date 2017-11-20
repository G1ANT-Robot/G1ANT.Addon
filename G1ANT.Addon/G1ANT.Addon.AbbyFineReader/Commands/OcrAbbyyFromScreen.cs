using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1ANT.Interop;
using G1ANT.Language.Commands;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using GStructures = G1ANT.Language.Structures;
using G1ANT.Sdk.Helpers;
using FREngine;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Commands
{
    [Command(Name = "ocrabbyy.fromscreen", ToolTip = "captures part of the screen and recognize text from it")]
    public class OcrAbbyyFromScreen : CommandBase<OcrAbbyyFromScreen.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "specifies screen area to be captured in format x0,y0,x1,y1 (x0,y0 – coordinates of a top left corner; x1,y1 – coordinates of a right bottom corner of the area)")]
            public GStructures.Rectangle Area { get; set; }

            [Argument(Tooltip = "if true area coordinates are relative to active window")]
            public GStructures.Bool Relative { get; set; } = new GStructures.Bool(false);

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public GStructures.String Language { get; set; } = new GStructures.String("English");

            [Argument(DefaultVariable = "result", Tooltip = "name of variable where recognized text will be stored")]
            public GStructures.String Result { get; set; } = new GStructures.String("result");

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

            System.Drawing.Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : Helpers.ToAbsoluteCoordinates(arguments.Area.Value);
            System.Drawing.Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);

            IEngine engine = manager.Engine;
            DocumentProcessingParams processingParams = engine.CreateDocumentProcessingParams();
            RecognizerParams recognizingParams = processingParams.PageProcessingParams.RecognizerParams;
            recognizingParams.SetPredefinedTextLanguage(arguments.Language.Value);
            engine.LoadPredefinedProfile(AbbyyManager.TextAccuracyProfile);
            FRDocument imageDocument = engine.CreateFRDocument();

            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                partOfScreen.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Position = 0;
                IReadStream imageStream = new StreamNet2AbbyyAdapter(stream);
                imageDocument.AddImageFileFromStream(imageStream);
            }

            imageDocument.Process(processingParams);


            SetVariableValue(arguments.Result.Value, new GStructures.String(imageDocument.PlainText.Text));
        }
    }
}
