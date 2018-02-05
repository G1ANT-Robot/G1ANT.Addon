using System;
using G1ANT.Language;
using System.Threading;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.waitforload")]
    public class MsCrmWaitForLoadCommand : Command
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
            public override TimeSpanStructure Timeout { get; set; } = new TimeSpanStructure(10000);
        }
         public MsCrmWaitForLoadCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var currentCrm = MsCrmManager.CurrentCRM;
            currentCrm.WaitForLoad(arguments.Search.Value, arguments.By.Value, arguments.Iframe.Value, (int)arguments.Timeout.Value.TotalMilliseconds);            
        }
    }
}
