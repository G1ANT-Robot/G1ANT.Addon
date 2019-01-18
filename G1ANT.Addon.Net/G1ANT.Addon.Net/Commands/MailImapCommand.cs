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
    [Command(Name = "mail.imap", Tooltip = "This command tries to retrieve the mails from inbox.")]
    public class MailImapCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "Host name")]
            public TextStructure Host { get; set; }

            [Argument(Required = true, Tooltip = "Port")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(993);

            [Argument(Required = true, Tooltip = "Login of the inbox user")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "Password of the inbox user")]
            public TextStructure Password { get; set; }

            [Argument(Required = true, Tooltip = "Since what date should emails be retrieved")]
            public DateStructure SinceDate { get; set; }

            [Argument(Required = false, Tooltip = "To what date should emails be retrieved")]
            public DateStructure ToDate { get; set; } = new DateStructure(DateTime.Now);

            [Argument(Required = false, Tooltip = "Look only for already unread messages")]
            public BooleanStructure OnlyUnreadMessages { get; set; } = new BooleanStructure(false);

            [Argument(Required = false, Tooltip = "Mark analyzed messages as read")]
            public BooleanStructure MarkAllMessagesAsRead { get; set; } = new BooleanStructure(true);

            [Argument(Required = false, Tooltip = "Received messages")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Required = false, Tooltip = "Ignore certificate errors")]
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
            var markAllMessagesAsRead = arguments.MarkAllMessagesAsRead.Value;

            var client = CreateImapClient(timeout);
            ConnectClient(client, credentials, uri, !markAllMessagesAsRead);

            if (client.IsConnected && client.IsAuthenticated)
            {
                var messages = ReceiveMesssages(client, arguments);
                SendMessageListToScripter(client, client.Inbox, arguments, messages);

                if (markAllMessagesAsRead)
                {
                    MarkMessagesAsRead(client, messages);
                }
            }
        }

        private static void ConnectClient(ImapClient client, NetworkCredential credentials, Uri uri, bool readOnly)
        {
            client.Connect(uri);
            client.Authenticate(credentials);
            client.Inbox.Open(readOnly ? FolderAccess.ReadOnly : FolderAccess.ReadWrite);
            client.Inbox.Subscribe();
        }

        private ImapClient CreateImapClient(int timeout)
        {
            var client = new ImapClient
            {
                Timeout = timeout,
            };
            return client;
        }

        private void SendMessageListToScripter(ImapClient client, IMailFolder folder, Arguments arguments, List<IMessageSummary> messages)
        {
            var messageList = CreateMessageStructuresFromMessages(client,folder,messages);
            Scripter.Variables.SetVariableValue(arguments.Result.Value, messageList);
        }

        private ListStructure CreateMessageStructuresFromMessages(ImapClient client, IMailFolder folder, List<IMessageSummary> messages)
        {
            var messageList = new ListStructure();
            foreach (var message in messages)
            {
                var attachments = CreateAttachmentStructuresFromAttachments(message,folder,message.Attachments);
                var messageWithFolder = new SimplifiedMessageSummary(message as MessageSummary, client.Inbox, attachments);
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

        private List<IMessageSummary> ReceiveMesssages(ImapClient client, Arguments arguments)
        {
            var options = MessageSummaryItems.All |
                          MessageSummaryItems.Body |
                          MessageSummaryItems.BodyStructure |
                          MessageSummaryItems.UniqueId;
            var allMessages = client.Inbox.Fetch(0, -1, options).ToList();
            var onlyUnread = arguments.OnlyUnreadMessages.Value;
            var since = arguments.SinceDate.Value;
            var to = arguments.ToDate.Value;

            return SelectMessages(allMessages, onlyUnread, since, to);
        }

        private static void MarkMessagesAsRead(ImapClient client, List<IMessageSummary> messages)
        {
            foreach (var message in messages)
            {
                client.Inbox.SetFlags(message.UniqueId, MessageFlags.Seen, true);
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
