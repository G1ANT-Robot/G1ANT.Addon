using G1ANT.Language;

namespace G1ANT.Addon.Ui
{
    [Command(Name = "ui.getposition", Tooltip = "This command allows you to attach ui field.")]
    public class UiGetPositionCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Wpath { get; set; }

            [Argument]
            public BooleanStructure Relative { get; set; } = new BooleanStructure(true);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");
        }
        public UiGetPositionCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            try
            {
                System.Drawing.Point point = System.Drawing.Point.Empty;
                if (arguments.Relative.Value)
                    point = UiManager.GetRelativePosition(arguments.Wpath.Value);
                else
                    point = UiManager.GetGlobalPosition(arguments.Wpath.Value);

                Scripter.Variables.SetVariableValue(arguments.Result.Value, new PointStructure(point));
            }
            catch
            {
                throw new ControlNotFoundException($"Cannot find control:{arguments.Wpath.Value}");
            }
        }
    }
}
