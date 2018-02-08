/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.GoogleDocs
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;

namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetDownloadTests
    {
        Scripter scripter;
        static string FileID = "147EH2vEjGVtbzzkT6XaI0eNZlY5Ec91wlvxN3HC4GMc"; //google sheets example file

        [OneTimeSetUp]
        public static void ClassInit()
        {

            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void Init()
        {
            scripter = new Scripter();
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("fileId", new TextStructure(FileID));
            scripter.InitVariables.Add("temp",new TextStructure(Path.GetTempPath().ToString()));
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetDownloadFileXLSX()
        {
            scripter.Text = ($@"googlesheet.open {SpecialChars.Variable}fileid isshared false
                                {SpecialChars.Variable}savePath = {SpecialChars.Variable}temp{SpecialChars.Text}\\SheetsTest.xlsx{SpecialChars.Text}
                                googlesheet.download path {SpecialChars.Variable}savePath type {SpecialChars.Text}xls{SpecialChars.Text}
                                delay 3
googlesheet.close");
            scripter.Run();
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("Download complete.", result.GetValue().ToString());
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetDownloadFilePDF()
        {
            scripter.Text = ($@"googlesheet.open {SpecialChars.Variable}fileid isshared false
                                {SpecialChars.Variable}savePath = {SpecialChars.Variable}temp{SpecialChars.Text}\\SheetsTest.pdf{SpecialChars.Text}
                                 googlesheet.download path {SpecialChars.Variable}savePath type {SpecialChars.Text}pdf{SpecialChars.Text}
                                 delay 3
googlesheet.close");
            scripter.Run();
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("Download complete.", result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
          
        }
    }
}
