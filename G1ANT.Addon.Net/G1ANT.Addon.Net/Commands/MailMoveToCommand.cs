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
using MailKit;
using G1ANT.Language;
using System.Net;
using MailKit.Net.Imap;

namespace G1ANT.Addon.Net
{
    [Command(Name = "mail.moveto", Tooltip = "This command moves a selected message to another folder")]
    public class MailMoveToCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "IMAP server address")]
            public TextStructure Host { get; set; }

            [Argument(Required = true, Tooltip = "User email login")]
            public TextStructure Login { get; set; }

            [Argument(Required = true, Tooltip = "User email password")]
            public TextStructure Password { get; set; }

            [Argument(Required = true, Tooltip = "Mail message to be moved")]
            public MailStructure Mail { get; set; }

            [Argument(Required = false, Tooltip = "Name of the destination folder")]
            public TextStructure Folder { get; set; } = new TextStructure(String.Empty);

            [Argument(Required = false, Tooltip = "IMAP server port number")]
            public IntegerStructure Port { get; set; } = new IntegerStructure(993);

            [Argument(Required = false, Tooltip = "If set to `true`, the command will ignore any security certificate errors")]
            public BooleanStructure IgnoreCertificateErrors { get; set; } = new BooleanStructure(false);
        }

        public MailMoveToCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            SetCertificateValidationCallback(arguments.IgnoreCertificateErrors.Value);

            var client = CreateClient(arguments);

            ValidateArgumentsAndConnection(client, arguments);

            var originFolder = client.GetFolder(arguments.Mail.Value.Folder.FullName);
            var destinationFolder = client.GetFolder(arguments.Folder.Value);

            ValidateFolders(originFolder, destinationFolder);

            destinationFolder.Open(FolderAccess.ReadWrite);
            originFolder.Open(FolderAccess.ReadWrite);
            originFolder.MoveTo(arguments.Mail.Value.UniqueId, destinationFolder);
        }

        private void ValidateArgumentsAndConnection(ImapClient client, Arguments arguments)
        {
            if (!client.IsConnected)
            {
                throw new Exception("Cannot connect to the server. Please check your internet connection.");
            }
            if (!client.IsAuthenticated)
            {
                throw new Exception("Cannot authenticate on the server. Please check credentials.");
            }
            if (arguments.Mail.Value == null)
            {
                throw new NullReferenceException("Provided mail message does not exist. Please check if it has been removed.");
            }
        }

        private void ValidateFolders(IMailFolder origin, IMailFolder destination)
        {
            if (origin == null)
            {
                throw new NullReferenceException($"Source folder {origin.Name} does not exist.");
            }
            if (destination == null)
            {
                throw new NullReferenceException($"Destination folder {destination.Name} does not exist.");
            }
        }

        private ImapClient CreateClient(Arguments arguments)
        {
            var credentials = new NetworkCredential(arguments.Login.Value, arguments.Password.Value);
            var uri = new UriBuilder("imaps", arguments.Host.Value, arguments.Port.Value).Uri;
            var timeout = (int)arguments.Timeout.Value.TotalMilliseconds;
            return ImapHelper.CreateImapClient(credentials, uri, false, timeout);
        }

        private void SetCertificateValidationCallback(bool ignoreCertificateErrors)
        {
            if (ignoreCertificateErrors)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
        }
    }
}