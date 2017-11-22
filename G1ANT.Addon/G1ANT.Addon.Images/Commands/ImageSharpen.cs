using System;
using System.Drawing;

using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.Images.Api;

namespace G1ANT.Language.Images.Commands
{
    [Command(Name = "image.sharpen", ToolTip = "This command sharpens image")]
    public class ImageSharpen : CommandBase<ImageSharpen.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Path of picture to get sharpened.")]
            public Structures.String Path { get; set; }

            [Argument(Tooltip = "Saving path. If not specified, input path will be used.")]
            public Structures.String OutputPath { get; set; }

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            string savingPath = arguments.OutputPath?.Value ?? arguments.Path.Value;
            string savingPathArgumentName = arguments.OutputPath != null ? nameof(arguments.OutputPath) : nameof(arguments.Path);
            Bitmap image = null;
            using (image = Imaging.OpenImageFile(arguments.Path.Value, nameof(arguments.Path)))
            {
                SharpenImage(image, arguments.Path.Value, nameof(arguments.Path));
                Imaging.SaveImageFile(image, savingPath, savingPathArgumentName);
            }
        }

        private void SharpenImage(Bitmap image, string path, string pathArgumentName)
        {
            try
            {
                Imaging.SharpenImage(image);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not sharpen image file '{path}' specified in {pathArgumentName} argument. Message: {ex.Message}", ex);
            }
        }
    }
}
