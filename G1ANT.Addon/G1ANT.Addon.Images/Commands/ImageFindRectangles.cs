using G1ANT.Language.Arguments;
using G1ANT.Language.Attributes;
using G1ANT.Language.Commands;
using G1ANT.Language.Images.Api;
using G1ANT.Language.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace G1ANT.Language.Images.Commands
{
    [Command(Name = "image.findrectangles")]
    public class ImageFindRectangles : CommandBase<ImageFindRectangles.Arguments>
    {
        public new class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public Structures.String Path { get; set; }

            [Argument]
            public Structures.String Result { get; set; } = new Structures.String("result");

            [Argument]
            public Structures.Bool Invert { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.Integer MinWidth { get; set; }

            [Argument]
            public Structures.Integer MaxWidth { get; set; }

            [Argument]
            public Structures.Integer MinHeight { get; set; }

            [Argument]
            public Structures.Integer MaxHeight { get; set; }

            [Argument]
            public Structures.Bool If { get; set; } = new Structures.Bool(true);

            [Argument]
            public Structures.String ErrorJump { get; set; }

            [Argument]
            public Structures.String ErrorMessage { get; set; }
        }

        public override void Execute(Arguments arguments, IExecutionContext executionContext)
        {
            try
            {
                List<System.Drawing.Rectangle> foundRectangles = new List<System.Drawing.Rectangle>();
                using (Bitmap image = (Bitmap)Image.FromFile(arguments.Path.Value))
                {
                    foundRectangles = AForgeWrapper.FindRectangles(
                        image,
                        arguments.Invert.Value,
                        arguments.MinWidth?.Value,
                        arguments.MaxWidth?.Value,
                        arguments.MinHeight?.Value,
                        arguments.MaxHeight?.Value);
                }                   

                List<Structure> results = new List<Structure>();
                foreach (var foundRectangle in foundRectangles)
                {
                    results.Add(new Structures.Rectangle(foundRectangle));
                }
                SetVariableValue(arguments.Result.Value, new Structures.List(results));
            }
            catch (Exception ex)
            {               
                throw new ApplicationException($"Specified directory doesn't exist. Message: {ex.Message}", ex);
            }
        }
    }
}


