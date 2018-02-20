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
using System.Xml;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using G1ANT.Language;

namespace G1ANT.Addon.Net.Tests
{
    [TestFixture]
    public class MailSmtpTests
    {
        public const int TestTimeout = 20000;

     
        private const int OneSecond = 1000;
        private const string textChar = SpecialChars.Text;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        [SetUp]
        public void Init()
        {
            
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Net.dll");
        }
        [Test, Timeout(TestTimeout)]
        public void SendEmailTest()
        {
            Scripter scripter = new Scripter();
            int startingQuantity = GetUnreadEmails((string)scripter.Variables.GetVariable("credential").GetValue("Net:email").Object, (string)scripter.Variables.GetVariable("credential").GetValue("Net:pass").Object).Count;
            string subject = GetEmailName();

            StringBuilder pathBuilder = new StringBuilder("testfile");
            string fileExtension = "txt";

            while (File.Exists($"{pathBuilder}.{fileExtension}"))
            {
                pathBuilder.Append('a');
            }
            pathBuilder.Append($".{fileExtension}");
            string path = pathBuilder.ToString();

            File.Create(path).Close();

            ListStructure attachments = new ListStructure(new List<Structure>()
            {
                new TextStructure(pathBuilder.ToString())
            });

            
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("att", attachments);
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} subject {textChar}{subject}{textChar} " +
                            $"attachments {SpecialChars.Variable}att";
            scripter.Run();

            Thread.Sleep(OneSecond * 3);

            var emails = GetUnreadEmails((string)scripter.Variables.GetVariable("credential").GetValue("Net:email").Object, (string)scripter.Variables.GetVariable("credential").GetValue("Net:pass").Object);

            bool found = false;
            for (int i = 0; i < emails.Count && !found; i++)
            {
                if (emails[i] == subject)
                {
                    found = true;
                }
            }

            Assert.IsTrue(found);
           // Assert.AreEqual(startingQuantity + 1, emails.Count); // gets always first 20 unreaded, so quantity will be 21 but emails.count always max 20

            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(SmtpException))]
        public void IncorrectPasswordTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {textChar}{"sadasdas" + 2.ToString()}{textChar} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} ";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<SmtpException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(SmtpException))]
        public void IncorrectLoginTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $"mail.smtp login {textChar}{"email" + "cos"}{textChar} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} ";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<SmtpException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
       // [ExpectedException(typeof(ArgumentException))]
        public void EmptyAddresseeTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {textChar}{string.Empty}{textChar} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} ";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(ArgumentException))]
        public void EmptyReceiverTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {textChar}{string.Empty}{textChar} ";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BadPortTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} port -3";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(WebException))]
        public void BadHostTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} host =-sdf=-jdsf-jsd";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<WebException>(exception.GetBaseException());
        }

        [Test, Timeout(TestTimeout)]
        //[ExpectedException(typeof(DirectoryNotFoundException))]
        public void NotExistingAttachmentTest()
        {
            string notExistingPath = "C:/asfpfs/safsaklfa/";

            ListStructure attachments = new ListStructure(new List<Structure>()
            {
                new TextStructure(notExistingPath)
            });

            if (File.Exists(notExistingPath))
            {
                Assert.Inconclusive($"File on path [{notExistingPath}] exists, very unlikely");
            }

            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("att", attachments);
            scripter.Text = $"mail.smtp login {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} password {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:pass{SpecialChars.IndexEnd} " +
                            $"from {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} to {SpecialChars.Variable}credential{SpecialChars.IndexBegin}Net:email{SpecialChars.IndexEnd} attachments {SpecialChars.Variable}att";


            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception.GetBaseException());
        }

        private string GetEmailName()
        {
            return $"Test {DateTime.Now.ToString()}";
        }

        private List<string> GetUnreadEmails(string email, string password)
        {
            List<string> mails = new List<string>();

            WebClient objClient = new WebClient();
            string response;
            string title;

            //Creating a new xml document
            XmlDocument doc = new XmlDocument();

            //Logging in Gmail server to get data
            objClient.Credentials = new NetworkCredential(email, password);
            //reading data and converting to string
            response = Encoding.UTF8.GetString(objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

            response = response.Replace(@"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

            //loading into an XML so we can get information easily
            doc.LoadXml(response);

            //nr of emails
            var nr = doc.SelectSingleNode(@"/feed/fullcount").InnerText;

            //Reading the title and the summary for every email
            foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
            {
                title = node.SelectSingleNode("title").InnerText;
                mails.Add(title);
            }
            return mails;
        }

    }
}
