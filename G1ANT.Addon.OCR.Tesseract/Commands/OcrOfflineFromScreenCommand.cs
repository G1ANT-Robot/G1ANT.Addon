using G1ANT.Language;
using System;
using System.Drawing;
using System.IO;
using Tesseract;

namespace G1ANT.Language.Ocr.Tesseract
{
    [Command(Name = "ocroffline.fromscreen", Tooltip = "This command allows to capture part of the screen and recognize text from it. \nThis command may often be less accurate than 'ocr' command. \nPlease be aware that command will unpack some necessary data to folder My Documents/G1ANT.Robot.", IsUnderConstruction = true)]
    public class OcrOfflineFromScreenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public RectangleStructure Area { get; set; }

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");

            [Argument(DefaultVariable = "timeoutocr")]
            public override int Timeout { get; set; }

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public TextStructure Language { get; set; } = new TextStructure("eng");

        }
        public OcrOfflineFromScreenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : Helpers.ToAbsoluteCoordinates(arguments.Area.Value);
            Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            var imgToParse = OcrOfflineHelper.RescaleImage(partOfScreen, 4.0);
            int timeout = arguments.Timeout;
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
                            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(text));
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

