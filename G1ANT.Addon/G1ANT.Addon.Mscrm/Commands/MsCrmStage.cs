using G1ANT.Language;
using System;

namespace G1ANT.Language.Mscrm
{
    [Command(Name = "mscrm.stage",Tooltip = "This command clicks on 'Next Stage' link in CRM.")]
    public class MsCrmStage : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(DefaultVariable = "next")]
            public TextStructure Stage { get; set; }
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result"); // 

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }

             
        }
         public MsCrmStage(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            try
            {
                var frame = MsCrmManager.CurrentCRM.GetCurrentIframe();
                if (frame != null)
                {
                    string id = "stageAdvanceActionContainer";
                    if (arguments.Stage.Value.ToLower() == "next")
                    {

                    }
                    else if (arguments.Stage.Value.ToLower() == "back")
                    {
                        id = "stageBackActionContainer";
                    }
                    else if (arguments.Stage.Value.ToLower() == "finish")
                    {
                        id = "stageFinishActionContainer";
                    }
                    else
                    {
                        throw new ApplicationException("Allowed stage in mscrm.stage are: back, next, finish");
                    }

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
