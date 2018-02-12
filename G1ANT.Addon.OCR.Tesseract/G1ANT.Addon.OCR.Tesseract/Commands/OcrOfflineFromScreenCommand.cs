/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.OCR.Tesseract
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using System.Drawing;
using System.IO;
using Tesseract;

namespace G1ANT.Addon.Ocr.Tesseract
{
    [Command(Name = "ocrtesseract.fromscreen", Tooltip = "This command allows to capture part of the screen and recognize text from it. \nThis command may often be less accurate than 'ocr' command. \nPlease be aware that command will unpack some necessary data to folder My Documents/G1ANT.Robot.", IsUnderConstruction = true)]
    public class OcrOfflineFromScreenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public RectangleStructure Area { get; set; }

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutocr")]
            public  override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "The language which should be considered trying to recognize text")]
            public TextStructure Language { get; set; } = new TextStructure("eng");

        }
        public OcrOfflineFromScreenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : arguments.Area.Value.ToAbsoluteCoordinates();
            Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            var imgToParse = OcrOfflineHelper.RescaleImage(partOfScreen, 4.0);
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
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
                            if (string.IsNullOrEmpty(text))
                                throw new NullReferenceException("Ocr was unable to find any text");
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
                throw e;
            }
            finally
            {
                File.Delete(imagePath);
            }
        }
    }
}

