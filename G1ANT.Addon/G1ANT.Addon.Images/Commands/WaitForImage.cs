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

    [Command(Name = "waitfor.image", ToolTip = "This command allows to wait for specified image in current screen view.", IsUnderConstruction = true)]
    public class WaitForImage : CommandBase<WaitForImage.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Specifies path to an image that we are waiting for.")]
            public Structures.String Image { get; set; }

            [Argument(Tooltip = "Argument specifying, whether the search is to be done relatively to the foreground window")]
            public Structures.Bool Relative { get; set; } = new Structures.Bool(true);

            [Argument(Tooltip = "Tolerance threshold. By default 0, which means that the image has to match in 100%.")]
            public Structures.Decimal Threshold { get; set; } = new Structures.Decimal(0);

            [Argument(Tooltip = "Argument narrowing search area. Specifying can speed up the search.")]
            public Structures.Rectangle ScreenSearchArea { get; set; } = new Structures.Rectangle(SystemInformation.VirtualScreen);

            [Argument(Tooltip = "If specified, result point will be pointing at the middle of the found area.")]
            public Structures.Bool CenterResult { get; set; } = new Structures.Bool(true);

            [Argument(Tooltip = "Value that will be added to the result's X coordinate.")]
            public Structures.Integer OffsetX { get; set; } = new Structures.Integer(0);

            [Argument(Tooltip = "Value that will be added to the result's Y coordinate.")]
            public Structures.Integer OffsetY { get; set; } = new Structures.Integer(0);

            [Argument(Required = true, DefaultVariable = "timeoutimagefind")]
            public override Structures.Integer Timeout { get; set; }

            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; } = new Structures.String("Error waitfor.image: problem while finding image");
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            if (arguments.Threshold.Value < 0 || arguments.Threshold.Value > 1)
            {
                throw new ArgumentOutOfRangeException("Threshold must be a value from 0 to 1.");
            }

            using (Bitmap bitmap1 = Imaging.OpenImageFile(arguments.Image.Value, nameof(arguments.Image)))
            {
                int timeout = arguments.Timeout.Value;
                long start = Environment.TickCount;
                Rectangle foundRectangle = Rectangle.Empty;
                while (Math.Abs(Environment.TickCount - start) < timeout && ShouldStopScript() == false && foundRectangle == Rectangle.Empty)
                {
                    using (
                    Bitmap bitmap2 = RobotWin32.GetPartOfScreen(
                        Imaging.ParseRectanglePositionFromArguments(arguments.ScreenSearchArea.Value, arguments.Relative.Value),
                        bitmap1.PixelFormat))
                    {
                        foundRectangle = Imaging.IsImageInImage(bitmap1, bitmap2, (double)arguments.Threshold.Value);
                        Application.DoEvents();
                    }
                }

                if (foundRectangle == Rectangle.Empty)
                {
                    throw new TimeoutException("Image was not found in specified search area.");
                }
                else
                {
                    Point foundPoint = (!arguments.CenterResult.Value) ?
                        new Point(foundRectangle.X, foundRectangle.Y) :
                        new Point(foundRectangle.X + foundRectangle.Width / 2, foundRectangle.Y + foundRectangle.Height / 2);
                    foundPoint = new Point(foundPoint.X + arguments.OffsetX.Value, foundPoint.Y + arguments.OffsetY.Value);
                    SetVariableValue(arguments.Result.Value, new Structures.Point(foundPoint));
                }
            }
        }
    }
}
