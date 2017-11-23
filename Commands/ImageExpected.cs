﻿using System;
using System.Drawing;
using System.Windows.Forms;

using G1ANT.Language.Images;

namespace G1ANT.Language.Images
{
    [Command(Name = "image.expected", Tooltip = "This command allows to confirm if image1 is exactly the same as image2")]
    public class ImageExpected : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path of the picture to be found.")]
            public TextStructure Image1 { get; set; }

            [Argument(Tooltip = "Path of the picture where image1 will be searched. If not specified, image1 will be searched on the screen.")]
            public TextStructure Image2 { get; set; }

            [Argument(Tooltip = "Argument narrowing search area. Specifying can speed up the search.")]
            public RectangleStructure ScreenSearchArea { get; set; } = new RectangleStructure(SystemInformation.VirtualScreen);

            [Argument(Tooltip = "Argument specifying, whether the search is to be done relatively to the foreground window")]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument(Tooltip = "Tolerance threshold. By default 0, which means that the image has to match in 100%.")]
            public FloatStructure Threshold { get; set; } = new FloatStructure(0);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

             
        }
        public ImageExpected(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
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
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(found));
            }

        }
    }
}
