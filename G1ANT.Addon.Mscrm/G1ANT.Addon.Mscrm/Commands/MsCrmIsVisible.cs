using System;
using G1ANT.Language;
using System.Threading;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.isvisible", Tooltip = "This command allows to search for specified text in active CRM instance.")]
    public class MsCrmIsVisibleCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument]
            public TextStructure Search { get; set; }

            [Argument]
            public TextStructure By { get; set; } = new TextStructure("id");

            [Argument]
            public TextStructure Iframe { get; set; } = new TextStructure(string.Empty);

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");



            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }
        }
        public MsCrmIsVisibleCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var currentCrm = MsCrmManager.CurrentCRM;
            bool isVisible = currentCrm.IsElementVisible(arguments.Search.Value, arguments.By.Value, arguments.Iframe.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(isVisible));
        }
    }
}
