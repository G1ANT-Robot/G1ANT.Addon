/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using System;
using System.Collections.Generic;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "outlook.getfolder", Tooltip = "This command returns folder by path.")]
    public class OutlookGetFolderCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true)]
            public TextStructure Path { get; set; }

            [Argument(Required = true)]
            public VariableStructure Result { get; set; }
        }
        public OutlookGetFolderCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.CurrentOutlook;
            if (outlookManager != null)
            {
                var folder = outlookManager.GetFolderByPath(arguments.Path.Value);
                if (folder == null)
                    throw new KeyNotFoundException($"Folder {arguments.Path.Value} cannot be found.");
                Scripter.Variables.SetVariableValue(arguments.Result.Value, new OutlookFolderStructure(folder, null, Scripter));
            }
            else
                throw new NullReferenceException("Current Outlook is not set.");
        }
    }
}
