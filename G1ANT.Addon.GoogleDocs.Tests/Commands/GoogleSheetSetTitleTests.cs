using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetSetTitleTests
    {
        Scripter scripter;
        static string FileID = "147EH2vEjGVtbzzkT6XaI0eNZlY5Ec91wlvxN3HC4GMc"; //google sheets example file
        static string titleBeforeChange;

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
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid");
            scripter.RunLine("googlesheet.gettitle");
            titleBeforeChange = scripter.Variables.GetVariable("result").GetValue().ToString();
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetSetTitle()
        {
            var valueToBePlaced = "G1ANT";
           scripter.InitVariables.Add("valueToBePlaced", new TextStructure(valueToBePlaced));
            scripter.RunLine($"googlesheet.settitle {SpecialChars.Variable}valueToBePlaced");
            scripter.RunLine("googlesheet.gettitle");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual(valueToBePlaced, result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
           scripter.InitVariables.Add("valueToBePlaced", new TextStructure(titleBeforeChange));
            scripter.RunLine($"googlesheet.settitle {SpecialChars.Variable}valueToBePlaced");
            scripter.RunLine("googlesheet.gettitle");
            var returnedToPreviousState = scripter.Variables.GetVariable("result").GetValue().ToString();
            Assert.AreEqual(titleBeforeChange, returnedToPreviousState);
            scripter.RunLine("googlesheet.close");
        }
    }
}
