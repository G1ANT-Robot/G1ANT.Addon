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
using System.Linq;
using System;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.getstate",Tooltip = "This command allows capturing current state displayed in the status bar (left side) at bottom of the form.")]
    public class MsCrmGetStateCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

             
        }
        public MsCrmGetStateCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(string.Empty));
            try
            {
                var currentCrm = MsCrmManager.CurrentCRM;
                string state = currentCrm.GetState();
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new Language.TextStructure(state));
            }
            catch
            {
                throw new ApplicationException("Unable to getstate from CRM");
            }
        }
    }
}
