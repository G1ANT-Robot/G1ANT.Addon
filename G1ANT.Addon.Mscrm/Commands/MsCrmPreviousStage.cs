using System;
using G1ANT.Language;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.previousstage",Tooltip = "This command clicks on 'Previous Stage(Back)' link in CRM. If operation is not possible ♥result  returns false, otherwise returns true.")]
    public class MsCrmPreviousStageCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result"); // 
        }
         public MsCrmPreviousStageCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                var frame = MsCrmManager.CurrentCRM.GetCurrentIframe();
                if (frame != null)
                {
                    string id = "stageBackActionContainer";
                    var stage = frame.Element(id);
                    if (stage != null)
                    {
                        if (stage.ClassName != null && stage.ClassName.Contains("disabled"))
                        {
                            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(false));
                        }
                        else
                        {
                            stage.Click();
                            Scripter.Variables.SetVariableValue(arguments.Result.Value, new BooleanStructure(true));
                            MsCrmManager.CurrentCRM.Ie.WaitForComplete();
                        }
                    }
                }
            }
            catch
            {
                throw new ApplicationException("Unable to nextstage in CRM");
            }
        }
    }
}
