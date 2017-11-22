using System;
using System.Drawing;
using System.Windows.Forms;

using G1ANT.Interop;
using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.Images.Api;

namespace G1ANT.Language.Images.Commands
{
    [Command(Name = "image.expected", ToolTip = "This command allows to confirm if image1 is exactly the same as image2")]
    public class ImageExpected : CommandBase<ImageExpected.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path of the picture to be found.")]
            public Structures.String Image1 { get; set; }

            [Argument(Tooltip = "Path of the picture where image1 will be searched. If not specified, image1 will be searched on the screen.")]
            public Structures.String Image2 { get; set; }

            [Argument(Tooltip = "Argument narrowing search area. Specifying can speed up the search.")]
            public Structures.Rectangle ScreenSearchArea { get; set; } = new Structures.Rectangle(SystemInformation.VirtualScreen);

            [Argument(Tooltip = "Argument specifying, whether the search is to be done relatively to the foreground window")]
            public Structures.Bool Relative { get; set; } = new Structures.Bool(true);

            [Argument(Tooltip = "Tolerance threshold. By default 0, which means that the image has to match in 100%.")]
            public Structures.Decimal Threshold { get; set; } = new Structures.Decimal(0);

            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            if (arguments.Threshold.Value < 0 || arguments.Threshold.Value > 1)
            {
                throw new ArgumentOutOfRangeException("Threshold must be a value from 0 to 1.");
            }

            using (Bitmap bitmap1 = Imaging.OpenImageFile(arguments.Image1.Value, nameof(arguments.Image1)))
            using (Bitmap bitmap2 = (string.IsNullOrEmpty(arguments.Image2?.Value)) ?
                                RobotWin32.GetPartOfScreen(
                                    Imaging.ParseRectanglePositionFromArguments(arguments.ScreenSearchArea.Value, arguments.Relative.Value),
                                    bitmap1.PixelFormat) :
                                Imaging.OpenImageFile(arguments.Image2.Value, nameof(arguments.Image2)))
            {


                bool found = Rectangle.Empty != Imaging.IsImageInImage(bitmap1, bitmap2, (double)arguments.Threshold.Value);
                SetVariableValue(arguments.Result.Value, new Structures.Bool(found));
            }

        }
    }
}
