/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Mscrm
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.nextstage",Tooltip = "This command clicks on 'Next Stage' link in CRM.")]
    public class MsCrmNextStageCommand : Command
    {
        public class Arguments : CommandArguments
        {  
            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result"); // 

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }
        }
         public MsCrmNextStageCommand(AbstractScripter scripter) : base(scripter)
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
