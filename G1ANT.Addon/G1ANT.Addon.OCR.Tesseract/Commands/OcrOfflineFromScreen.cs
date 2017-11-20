using G1ANT.Interop;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Sdk.Helpers;
using System;
using System.Drawing;
using System.IO;
using Tesseract;

namespace G1ANT.Language.Ocr.Tesseract.Commands
{
    [Command(Name = "ocroffline.fromscreen", ToolTip = "This command allows to capture part of the screen and recognize text from it. \nThis command may often be less accurate than 'ocr' command. \nPlease be aware that command will unpack some necessary data to folder My Documents/G1ANT.Robot.", IsUnderConstruction = true)]
    public class OcrOfflineFromScreen : CommandBase<OcrOfflineFromScreen.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.Rectangle Area { get; set; }

            [Argument]
            public Structures.Bool Relative { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument(DefaultVariable = "timeoutocr")]
            public override Structures.Integer Timeout { get; set; }

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public Structures.String Language { get; set; } = new Structures.String("eng");
            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }

        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : Helpers.ToAbsoluteCoordinates(arguments.Area.Value);
            Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            var imgToParse = OcrOfflineHelper.RescaleImage(partOfScreen, 4.0);
            int timeout = arguments.Timeout.Value;
            string language = arguments.Language.Value;
            var imagePath = OcrOfflineHelper.SaveImageToTemporaryFolder(imgToParse);
            OcrOfflineHelper.UnpackNeededAssemblies();
            var dataPath = OcrOfflineHelper.GetResourcesFolder(language);
            try
            {
                using (var tEngine = new TesseractEngine(dataPath, language, EngineMode.TesseractAndCube))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = tEngine.Process(img))
                        {
                            var text = page.GetText();
                            SetVariableValue(arguments.Result.Value, new Language.Structures.String(text));
                        }
                    }
                }
            }
            catch (TesseractException e)
            {
                throw new ApplicationException("Ocr engine exception, possibly missing language data in folder : " + dataPath);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message + " " + e.InnerException.Message);
            }
            finally
            {
                File.Delete(imagePath);
            }
        }
    }
}

