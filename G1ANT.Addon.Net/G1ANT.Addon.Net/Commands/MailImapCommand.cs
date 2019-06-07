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
using G1ANT.Language.Models;
using System.Net;


namespace G1ANT.Addon.Net
{
    [Command(Name = "mail.imap", Tooltip = "This command uses the IMAP protocol to check an email inbox and allows the user to analyze their messages received within a specified time span, with the option to consider only unread messages and/or mark all of the checked ones as read")]
    public class MailImapCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "IMAP server address")]
            public TextStructure Host { get; set; }

            [Argument(Required = true, Tooltip = "IMAP server port number")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(993);

            [Argument(Required = true, Tooltip = "User email login")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "User email password")]
            public TextStructure Password { get; set; }

            [Argument(Required = true, Tooltip = "Folder to fetch emails from")]
            public TextStructure Folder { get; set; } = new TextStructure("INBOX");

            [Argument(Required = true, Tooltip = "Starting date for messages to be checked")]
            public DateStructure SinceDate { get; set; }

            [Argument(Required = false, Tooltip = "Ending date for messages to be checked")]
            public DateStructure ToDate { get; set; } = new DateStructure(DateTime.Now);

            [Argument(Required = false, Tooltip = "If set to `true`, only unread messages will be checked")]
            public BooleanStructure OnlyUnreadMessages { get; set; } = new BooleanStructure(false);

            [Argument(Required = false, Tooltip = "Mark analyzed messages as read")]
            public BooleanStructure MarkAsRead { get; set; } = new BooleanStructure(true);

            [Argument(Required = false, Tooltip = "Name of a list variable where the returned mail variables will be stored")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Required = false, Tooltip = "If set to `true`, the command will ignore any security certificate errors")]
            public BooleanStructure IgnoreCertificateErrors { get; set; } = new BooleanStructure(false);
        }


        public MailImapCommand(AbstractScripter scripter) : base(scripter)
        { }


        public void Execute(Arguments arguments)
        {
            if (arguments.IgnoreCertificateErrors.Value)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            var credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var uri = new UriBuilder("imaps", arguments.Host.Value, arguments.Port.Value).Uri;
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            var markAllMessagesAsRead = arguments.MarkAsRead.Value;

            var client = ImapHelper.CreateImapClient(credentials, uri, !markAllMessagesAsRead, timeout);

            if (client.IsConnected && client.IsAuthenticated)
            {
                var folder = client.GetFolder(arguments.Folder.Value);
                folder.Open(FolderAccess.ReadOnly);
                var messages = ReceiveMesssages(folder, arguments);
                SendMessageListToScripter(folder, arguments, messages);

                if (markAllMessagesAsRead)
                {
                    MarkMessagesAsRead(folder, messages);
                }
            }
        }

        private void SendMessageListToScripter(IMailFolder folder, Arguments arguments, List<IMessageSummary> messages)
        {
            var messageList = CreateMessageStructuresFromMessages(folder, messages);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, messageList);
        }

        private ListStructure CreateMessageStructuresFromMessages(IMailFolder folder, List<IMessageSummary> messages)
        {
            var messageList = new ListStructure();
            foreach (var message in messages)
            {
                var attachments = CreateAttachmentStructuresFromAttachments(message,folder,message.Attachments);
                var messageWithFolder = new SimplifiedMessageSummary(message as MessageSummary, folder, attachments);
                var structure = new MailStructure(messageWithFolder, null, null);
                messageList.AddItem(structure);
            }
            return messageList;
        }

        private ListStructure CreateAttachmentStructuresFromAttachments (IMessageSummary message,IMailFolder folder, 
            IEnumerable<BodyPartBasic> attachments)
        {
            ListStructure attachmentsList = new ListStructure();
            foreach (var attachment in attachments)
            {
                AttachmentModel attachmentModel = new AttachmentModel(attachment, folder, message);
                AttachmentStructure temp = new AttachmentStructure(attachmentModel);
                attachmentsList.AddItem(temp);
            }
            return attachmentsList;
        }

        private List<IMessageSummary> ReceiveMesssages(IMailFolder folder, Arguments arguments)
        {
            var options = MessageSummaryItems.All |
                          MessageSummaryItems.Body |
                          MessageSummaryItems.BodyStructure |
                          MessageSummaryItems.UniqueId;
            var allMessages = folder.Fetch(0, -1, options).ToList();
            var onlyUnread = arguments.OnlyUnreadMessages.Value;
            var since = arguments.SinceDate.Value;
            var to = arguments.ToDate.Value;

            return SelectMessages(allMessages, onlyUnread, since, to);
        }

        private static void MarkMessagesAsRead(IMailFolder folder, List<IMessageSummary> messages)
        {
            foreach (var message in messages)
            {
                folder.SetFlags(message.UniqueId, MessageFlags.Seen, true);
            }
        }

        private static List<IMessageSummary> SelectMessages(
        IList<IMessageSummary> messages, bool onlyUnRead, DateTime sinceDate, DateTime toDate)
        {
            Func<IMessageSummary, bool> isUnread = m => m.Flags != null && m.Flags.Value.HasFlag(MessageFlags.Seen) == false;
            var relevantMessages = messages.Where(m => m.Date >= sinceDate && m.Date <= toDate).ToList();
            relevantMessages = onlyUnRead ? relevantMessages.Where(isUnread).ToList() : relevantMessages;
            return relevantMessages;
        }
    }
}
