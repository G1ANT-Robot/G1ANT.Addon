using System;
using G1ANT.Language;

namespace G1ANT.Addon.UI
{
    [Command(Name = "ui.setroot",
        Tooltip = "This command sets a root window, to which other UI elements will refer to by WPath")]
    public class UISetRootCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Desktop application window to be referred to")]
            public WPathStructure WPath { get; set; }

            //[Argument(Tooltip = "Name of the process to attach to (one of the WPath or ProcessName is required)")]
            //public TextStructure ProcessName { get; set; }
        }

        public UISetRootCommand(AbstractScripter scripter) : base(scripter)
        {
        }

        public void Execute(Arguments arguments)
        {
            if (string.IsNullOrEmpty(arguments.WPath?.Value))
            {
                UIElement.RootElement = null;
            }
            else
            {
                UIElement.RootElement = null;
                var root = UIElement.FromWPath(arguments.WPath);
                UIElement.RootElement = root;
            }
        }
    }
}
