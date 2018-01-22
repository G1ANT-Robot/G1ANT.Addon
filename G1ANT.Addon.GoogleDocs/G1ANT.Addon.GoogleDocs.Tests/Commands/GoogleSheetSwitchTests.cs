using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetSwitchTests
    {
        Scripter scripter;
        static string FileID1 = "147EH2vEjGVtbzzkT6XaI0eNZlY5Ec91wlvxN3HC4GMc"; //google sheets example file
        static string FileID2 = "1d4InQksHBQyAqmogBc2xsP2eU7uBGD3iBtcmng3--Hk"; //google sheets example file

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        [Timeout(50000)]
        public void Init()
        {
            scripter = new Scripter();
            scripter.InitVariables.Clear();
           scripter.InitVariables.Add("fileId1", new TextStructure(FileID1));
            scripter.InitVariables.Add("fileId2", new TextStructure(FileID2));
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetSwitchSpreadsheets()
        {
            scripter.Text =($@"googlesheet.open {SpecialChars.Variable}fileid1 result {SpecialChars.Variable}excelID1
                                googlesheet.open {SpecialChars.Variable}fileid2 result {SpecialChars.Variable}excelID2
                                googlesheet.switch {SpecialChars.Variable}excelID1
                                googlesheet.gettitle result {SpecialChars.Variable}excelTitle1
                                googlesheet.switch {SpecialChars.Variable}excelID2
                                googlesheet.gettitle result {SpecialChars.Variable}excelTitle2");
            scripter.Run();
            var result1 = scripter.Variables.GetVariable("excelTitle1");
            var result2 = scripter.Variables.GetVariable("excelTitle2");
            Assert.AreEqual("Example Spreadsheet", result1.GetValue().ToString());
            Assert.AreEqual("Example Spreadsheet Edited", result2.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine($"googlesheet.close {SpecialChars.Variable}excelID1");
            scripter.RunLine($"googlesheet.close {SpecialChars.Variable}excelID2");
        }
    }
}
