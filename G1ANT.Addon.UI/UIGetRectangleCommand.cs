using System;
using G1ANT.Language;
using System.Drawing;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.getrectangle",
        Tooltip = "Get bounding box of the UI control described by WPathStructure")]
    public class UIGetRectangleCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "WPath structure defining control of the desktop application")]
            public WPathStructure WPath { get; set; }

            [Argument(Required = true)]
            public VariableStructure Result { get; set; }
        }

        public UIGetRectangleCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            var element = UIElement.FromWPath(arguments.WPath);
            if (element != null)
            {
                var rect = element.GetRectangle();
                Scripter.Variables.SetVariableValue(arguments.Result.Value, 
                    new RectangleStructure(Rectangle.FromLTRB((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom), null, Scripter));
            }
        }
    }
}
