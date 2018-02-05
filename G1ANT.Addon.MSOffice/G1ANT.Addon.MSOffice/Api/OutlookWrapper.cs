using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Data.Linq.SqlClient;

namespace G1ANT.Addon.MSOffice
{
    public class OutlookWrapper
    {
        private OutlookWrapper() { }
        public OutlookWrapper(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
        public bool IsMailFound { get; set; } = false;

        private Application application;

        public Application Application
        {
            get
            {
                try
                {
                    string version = application.Version;
                }
                catch (System.Exception ex)
                {
                    throw new InvalidOperationException($"Outlook instance could not be found. Most likely, it has been closed. Message: '{ex.Message}'.");
                }
                return application;
            }
            set { application = value; }
        }

        private MailItem mailItem = null;
        private NameSpace nameSpace = null;

        public void Open()
        {
            Application = new Application();
            nameSpace = Application.GetNamespace("MAPI");
            nameSpace.GetDefaultFolder(OlDefaultFolders.olFolderInbox).Display();            
        }

        public void NewMessage(string to, string subject, string body)
        {
            mailItem = Application.CreateItem(OlItemType.olMailItem);
            mailItem.To = to;
            mailItem.Subject = subject;
            mailItem.Body = body;
            mailItem.Display();
            mailItem.Save();
        }

        public void DiscardMail()
        {
            mailItem.Close(OlInspectorClose.olDiscard);
        }
        public void NewMessageWithAttachements(string to, string subject, string body, List<string> paths)
        {
            mailItem = Application.CreateItem(OlItemType.olMailItem);
            mailItem.To = to;
            mailItem.Subject = subject;
            mailItem.Body = body;
            List<FileInfo> files = new List<FileInfo>();
            foreach (var filePath in paths)
            {
                if (File.Exists(filePath))
                {
                    FileInfo file = new FileInfo(filePath);
                    files.Add(file);
                }
                else if (string.IsNullOrWhiteSpace(filePath) == false)
                {
                    throw new FileNotFoundException("Attachement not found: " + filePath);
                }
            }
            for (int i = 0; i < files.Count; i++)
            {
                mailItem.Attachments.Add(files[i].FullName, OlAttachmentType.olByValue, i + 1, files[i].Name);
            }
            mailItem.Display();

        }
        private class MailDetails
        {
            public string EntryID { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
            public string FolderID { get; set; } = string.Empty;
        }

        private List<MailDetails> ReturnAllMailsDetails()
        {

            List<MailDetails> mailDetailsList = new List<MailDetails>();
            foreach (MAPIFolder f in nameSpace.Folders)
            {
                MAPIFolder inboxFolder = f.Store.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                Items items = inboxFolder.Items;
                foreach (var item in inboxFolder.Items)
                {
                    MailItem tmp = item as MailItem;
                    if (tmp != null)
                    {
                        MailDetails mailDetails = new MailDetails()
                        {
                            EntryID = tmp.EntryID,
                            Subject = tmp?.Subject ?? string.Empty,
                            FolderID = f.StoreID
                        };
                        mailDetailsList.Add(mailDetails);
                    }
                }
            }
            return mailDetailsList;
        }

        private List<MailDetails> ReturnAllFoundMailsDetails(List<MailDetails> mailDetailList, string search)
        {
            int howManySubjectsFound = mailDetailList.Where(x => x.Subject.Contains(search)).Count();
            List<MailDetails> foundMailDetailsList = new List<MailDetails>();
            if (howManySubjectsFound > 0)
            {
                foreach (var item in mailDetailList)
                {
                    if (item.Subject.Contains(search))
                    {
                        MailDetails foundMailDetails = new MailDetails()
                        {
                            EntryID = item.EntryID,
                            Subject = item.Subject,
                            FolderID = item.FolderID
                    };                     
                        foundMailDetailsList.Add(foundMailDetails);
                    }
                }
            }
            return foundMailDetailsList;
        }


        public IList<MailItem> FindMails(string search, bool showMail = false)
        {

            List<MailDetails> allMailsDetails = ReturnAllMailsDetails();
            List<MailDetails> allFoundMailsDetails = ReturnAllFoundMailsDetails(allMailsDetails, search);
            IsMailFound = allFoundMailsDetails?.Count() > 0 ? true : false;
            MailItem tmpMailItem;
            List<MailItem> foundMails = new List<MailItem>();
            foreach (var item in allFoundMailsDetails)
            {
                tmpMailItem = nameSpace.GetItemFromID(item.EntryID, item.FolderID) as MailItem;
                if (tmpMailItem != null) foundMails.Add(tmpMailItem);

            }
            if (showMail)
            {
                foreach (MailItem item in foundMails)
                {
                    item.Display();
                }
               
            }
            return foundMails;
        }

        public void Send()
        {
            mailItem.Send();

        }
        public void Close()
        {
            try
            {
                Application.Quit();

                    foreach (Process p in Process.GetProcessesByName("outlook"))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { }
                    }
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException($"Error occured while closing current Outlook Instance. Message: {ex.Message}");
            }
        }
    }
}
