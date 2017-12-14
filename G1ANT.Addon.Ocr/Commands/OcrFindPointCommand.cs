using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G1ANT.Language.Ocr
{
    [Command(Name = "ocr.findpoint", Tooltip = "This command allows to find the text on the active screen and return it's position in a 'point' format. ", IsUnderConstruction = true)]
    public class OcrFindPointCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true,Tooltip = "Text that you want to find in the screen. Provide only single words")]
            public TextStructure Search { get; set; }

            [Argument]
            public RectangleStructure Area { get; set; } = new RectangleStructure(System.Windows.Forms.Screen.PrimaryScreen.Bounds);

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(DefaultVariable = "timeoutOcr")]
            public  override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "Comma separated list of languages from which you want to recognize text on the screen")]
            public TextStructure Languages { get; set; } = new TextStructure("en");
        }
        public OcrFindPointCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            System.Drawing.Rectangle rectangle = arguments.Relative.Value ? arguments.Area.Value.ToAbsoluteCoordinates() : arguments.Area.Value;
            System.Drawing.Bitmap partOfScreen = RobotWin32.GetPartOfScreen(rectangle);
            int timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            List<string> languages = arguments.Languages.Value.Split(',').ToList();
            string search = arguments.Search.Value;
            GoogleCloudApi googleApi = new GoogleCloudApi();

            System.Drawing.Rectangle output = googleApi.RecognizeText(partOfScreen, search, languages, timeout);
            System.Drawing.Point pointOutput = new System.Drawing.Point(output.X + arguments.Area.Value.X, output.Y + arguments.Area.Value.Y);
            if (output == null)
                throw new ApplicationException("Ocr was unable to find text");
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new PointStructure(pointOutput));
        }
    }
}
