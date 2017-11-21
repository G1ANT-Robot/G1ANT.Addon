﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FREngine;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader
{
    [Command(Name = "ocrabbyy.fromscreen", Tooltip = "captures part of the screen and recognize text from it")]
    public class OcrAbbyyFromScreenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "specifies screen area to be captured in format x0,y0,x1,y1 (x0,y0 – coordinates of a top left corner; x1,y1 – coordinates of a right bottom corner of the area)")]
            public RectangleStructure Area { get; set; }

            [Argument(Tooltip = "if true area coordinates are relative to active window")]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(false);

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public TextStructure Language { get; set; } = new TextStructure("English");

            [Argument(DefaultVariable = "result", Tooltip = "name of variable where recognized text will be stored")]
            public TextStructure Result { get; set; } = new TextStructure("result");

 
        }
        public OcrAbbyyFromScreenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        private AbbyyManager manager = null;

        public void Execute(Arguments arguments)
        {
            manager = AbbyyManager.Instance;

            System.Drawing.Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : Helpers.ToAbsoluteCoordinates(arguments.Area.Value); //TODO CASE
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


            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(imageDocument.PlainText.Text));
        }
    }
}