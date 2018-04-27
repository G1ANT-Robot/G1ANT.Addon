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
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;

namespace G1ANT.Addon.MSOffice
{
    [Structure(Name = "OutlookMail", AutoCreate = false)]
    public class OutlookMailStructure : StructureTyped<MailItem>
    {
        const string IdIndex = "id";
        const string FromIndex = "from";
        const string AccountIndex = "account";
        const string SubjectIndex = "subject";
        const string BodyIndex = "body";
        const string HtmlBodyIndex = "htmlbody";
        const string AttachmentsIndex = "attachments";

        public OutlookMailStructure(string value, string format = "", AbstractScripter scripter = null) :
            base(value, format, scripter)
        {
            Init();
        }

        public OutlookMailStructure(object value, string format = null, AbstractScripter scripter = null)
            : base(value, format, scripter)
        {
            Init();
        }

        protected void Init()
        {
            Indexes.Add(IdIndex);
            Indexes.Add(SubjectIndex);
            Indexes.Add(AttachmentsIndex);
            Indexes.Add(BodyIndex);
            Indexes.Add(HtmlBodyIndex);
            Indexes.Add(FromIndex);
            Indexes.Add(AccountIndex);
        }

        public override Structure Get(string index = "")
        {
            if (string.IsNullOrWhiteSpace(index))
                return new OutlookMailStructure(Value, Format);
            switch (index.ToLower())
            {
                case IdIndex:
                    return new TextStructure(Value.EntryID, null, Scripter);
                case SubjectIndex:
                    return new TextStructure(Value.Subject, null, Scripter);
                case BodyIndex:
                    return new TextStructure(Value.Body, null, Scripter);
                case HtmlBodyIndex:
                    return new TextStructure(Value.HTMLBody, null, Scripter);
                case FromIndex:
                    return new TextStructure(Value.SenderEmailAddress, null, Scripter);
                case AccountIndex:
                    return new TextStructure(Value.SendUsingAccount.SmtpAddress, null, Scripter);
                case AttachmentsIndex:
                    {
                        var outlookManager = OutlookManager.CurrentOutlook;
                        if (outlookManager != null)
                        {
                            var attachements = outlookManager.GetAttachments(Value);
                            var list = new List<object>();
                            foreach (var a in attachements)
                                list.Add(new OutlookAttachmentStructure(a, null, Scripter));
                            return new ListStructure(list, null, Scripter);
                        }
                        else
                            throw new NullReferenceException("Current Outlook is not set.");
                    }
            }
            throw new ArgumentException($"Unknown index '{index}'");
        }

        public override void Set(Structure structure, string index = null)
        {
            if (structure == null || structure.Object == null)
                throw new ArgumentNullException(nameof(structure));
            else
            {
                switch (index.ToLower())
                {
                    case SubjectIndex:
                        Value.Subject = structure.ToString();
                        break;
                    case BodyIndex:
                        Value.Body = structure.ToString();
                        break;
                    case HtmlBodyIndex:
                        Value.HTMLBody = structure.ToString();
                        break;
                    case AccountIndex:
                        {
                            Accounts accounts = Value.Session.Accounts;
                            foreach (Account account in accounts)
                            {
                                // When the e-mail address matches, return the account. 
                                if (account.SmtpAddress == structure.ToString())
                                {
                                    Value.SendUsingAccount = account;
                                    Value.Sender = account.CurrentUser.AddressEntry;
                                    return;
                                }
                            }
                            throw new ArgumentException($"Cannot find outlook account '{structure.ToString()}'");
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unknown index '{index}'");
                }
            }
        }

        public override string ToString(string format)
        {
            return Value?.ToString();
        }

        protected override MailItem Parse(string value, string format = null)
        {
            return null;
        }
    }
}
