
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
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
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid isshared false");
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetDownloadFileXLSX()
        {
            scripter.RunLine($"{SpecialChars.Variable}savePath = {SpecialChars.Variable}temp{SpecialChars.Text}\\SheetsTest.xlsx{SpecialChars.Text}");
            scripter.RunLine($"googlesheet.download path {SpecialChars.Variable}savePath type {SpecialChars.Text}xls{SpecialChars.Text} ");
            scripter.RunLine("delay 3");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("Download complete.", result.GetValue().ToString());
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetDownloadFilePDF()
        {
            scripter.RunLine($"{SpecialChars.Variable}savePath = {SpecialChars.Variable}temp{SpecialChars.Text}\\SheetsTest.pdf{SpecialChars.Text}");
            scripter.RunLine($"googlesheet.download path {SpecialChars.Variable}savePath type {SpecialChars.Text}pdf{SpecialChars.Text}");
            scripter.RunLine("delay 3");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("Download complete.", result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("googlesheet.close");
        }
    }
}
