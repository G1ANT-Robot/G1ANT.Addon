
using G1ANT.Language;
using System.Collections.Generic;
using System.Linq;

namespace G1ANT.Language.Ocr
{
    [Command(Name = "ocr.fromscreen",Tooltip = "This command allows to capture part of the screen and recognize text from it. \nIt uses internet connection and external data processing.")]
    public class OcrFromScreenCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public RectangleStructure Area { get; set; }

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");

            [Argument(DefaultVariable = "timeoutOcr")]
            public override int Timeout { get; set; }

            [Argument(Tooltip = "Comma separated language hints for better text recognition")]
            public TextStructure Languages { get; set; } = new TextStructure("en");

        }
        public OcrFromScreenCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            System.Drawing.Rectangle rectangle = !arguments.Relative.Value ? arguments.Area.Value : Helpers.ToAbsoluteCoordinates(arguments.Area.Value);

            System.Drawing.Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            int timeout = arguments.Timeout;
            List<string> languages = arguments.Languages.Value.Split(',').ToList();
            GoogleCloudApi googleApi = new GoogleCloudApi();
            string output = googleApi.RecognizeText(partOfScreen, languages, timeout);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(output));
        }
    }
}
