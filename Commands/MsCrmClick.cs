using System;
using G1ANT.Language;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.click",Tooltip = "This command allows to send click event to element of an active CRM instance.")]
    public class MsCrmClickCommand : Command
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
            public BooleanStructure Trigger { get; set; } = new BooleanStructure(false);

            [Argument]
            public BooleanStructure NoWait { get; set; } = new BooleanStructure(false);

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }

             
        }
        public MsCrmClickCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            if (MsCrmManager.CurrentCRM != null)
            {
                MsCrmManager.CurrentCRM.ClickByElement(arguments.Search.Value, arguments.By.Value, arguments.Iframe.Value, arguments.Trigger.Value, arguments.NoWait.Value);
                if (!arguments.NoWait.Value)
                {
                    MsCrmManager.CurrentCRM.Ie.WaitForComplete();
                }
            }
            else
            {
                throw new ApplicationException("Unable to find CRM. Use mscrm.attach first.");
            }
        }
    }
}
