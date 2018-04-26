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

namespace G1ANT.Addon.MSOffice.Commands
{
    [Command(Name = "outlook.moveto", Tooltip = "This command move mail or folder to the new destination.")]
    public class OutlookMoveToCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "mail or folder structure.")]
            public Structure Item { get; set; }

            [Argument(Required = true, Tooltip = "Destination folder where item will be moved.")]
            public OutlookFolderStructure DestinationFolder { get; set; }
        }
        public OutlookMoveToCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            var outlookManager = OutlookManager.CurrentOutlook;
            if (outlookManager != null)
            {
                if (arguments.Item is OutlookMailStructure mail)
                    mail.Value.Move(arguments.DestinationFolder.Value);
                else if (arguments.Item is OutlookFolderStructure folder)
                    folder.Value.MoveTo(arguments.DestinationFolder.Value);
                else
                    throw new NotSupportedException($"{arguments.Item.GetType()} is not supported.");
            }
            else
                throw new NullReferenceException("Current Outlook is not set.");
        }
    }
}
