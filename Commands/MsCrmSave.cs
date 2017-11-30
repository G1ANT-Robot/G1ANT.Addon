using System;
using G1ANT.Language;
using WatiN.Core.Constraints;
using WatiN.Core;
using System.Threading;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.save",Tooltip = "This command saves changes in current form of CRM.")]
    public class MsCrmSaveCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

             
        }
         public MsCrmSaveCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
                Document frame = MsCrmManager.CurrentCRM.GetCurrentIframe();
                if (frame != null)
                {
                    Element divSave = frame.Element(Find.ById("savefooter_statuscontrol"));
                    Span saveMessage = frame.Span("titlefooter_statuscontrol");
                    if (divSave != null)
                    {                       
                        divSave.Click();
                        Thread.Sleep(300);
                        saveMessage.WaitUntil(x => x.InnerHtml == null || x.InnerHtml?.Trim() != "saving", GetTimeoutLeftSeconds());
                        Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
                    }
                }
            }
            catch
            {
                throw new ApplicationException("Unable to save");
            }           
        }
    }
}
