using G1ANT.Language;
using System;

namespace G1ANT.Language.Mscrm
{
    [Command(Name = "mscrm.nextstage",Tooltip = "This command clicks on 'Next Stage' link in CRM.")]
    public class MsCrmNextStage: Command
    {
        public class Arguments : CommandArguments
        {  
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result"); // 

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }
        }
         public MsCrmNextStage(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                var frame = MsCrmManager.CurrentCRM.GetCurrentIframe();
                if (frame != null)
                {
                    string id = "stageAdvanceActionContainer";
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
