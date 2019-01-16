/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;
using G1ANT.Language;
using G1ANT.Language.Services;
using System.Net;


namespace G1ANT.Addon.Net
{
    [Command(Name = "mail.clearattachments", Tooltip = "This command tries to delete all previouslz downloaded attachments.")]
    public class MailClearAttachmentsCommand : Command
    {
        public MailClearAttachmentsCommand(AbstractScripter scripter) : base(scripter)
        { }

        public new void Execute(ArgumentsBase arguments)
        {
            LongLivingTempFileService tempFileService = new LongLivingTempFileService();
            tempFileService.DeleteAllTempFilesWithPrefix("g1ant.attachment.");
        }

    }
}
