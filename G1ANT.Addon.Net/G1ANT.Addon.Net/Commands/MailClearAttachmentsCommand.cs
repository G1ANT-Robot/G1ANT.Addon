/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using G1ANT.Language;
using G1ANT.Language.Services;

namespace G1ANT.Addon.Net
{
    [Command(Name = "mail.clearattachments", Tooltip = "This command deletes all previously downloaded attachments.")]
    public class MailClearAttachmentsCommand : Command
    {
        ILongLivingTempFileService longLivingTempFileService;

        public MailClearAttachmentsCommand(AbstractScripter scripter, ILongLivingTempFileService IlongLivingTempFileService = null) : base(scripter)
        {
            longLivingTempFileService = IlongLivingTempFileService ?? new LongLivingTempFileService();
        }

        public new void Execute(ArgumentsBase arguments)
        {
            longLivingTempFileService.DeleteAllTempFilesWithPrefix("g1ant.attachment.");
        }

    }
}
