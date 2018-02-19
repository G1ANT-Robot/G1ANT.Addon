﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Mscrm
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using G1ANT.Language;
using System.Threading;

namespace G1ANT.Addon.Mscrm
{
    [Command(Name = "mscrm.setvalue", Tooltip = "This command allows to set value of a field in CRM form. This command allows to handle fields like: text, lookup, tree lookup and dropdown.")]
    public class MsCrmSetValueCommand : Command
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
            public TextStructure Value { get; set; }

            [Argument]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument]
            public TextStructure ValidationResult { get; set; } = new TextStructure("validationresult");

             

            [Argument(DefaultVariable = "timeoutcrm")]
            public override TimeSpanStructure Timeout { get; set; }
        }
         public MsCrmSetValueCommand(AbstractScripter scripter) : base(scripter)
        { }
        public void Execute(Arguments arguments)
        {
            var currentCrm = MsCrmManager.CurrentCRM;
            int result = currentCrm.SetVal(arguments.Search.Value, arguments.Value.Value, arguments.By.Value, arguments.Iframe.Value);
            bool isFieldValid = currentCrm.FieldIsValid(arguments.Search.Value, arguments.By.Value, arguments.Iframe.Value);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new IntegerStructure(result));
            isFieldValid = isFieldValid && result == 1;
            Scripter.Variables.SetVariableValue(arguments.ValidationResult.Value, new BooleanStructure(isFieldValid));
        }
    }
}
