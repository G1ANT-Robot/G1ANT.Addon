using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Language.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetGetTitleTests
    {
        static Scripter scripter;
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
            scripter.Variables.SetVariableValue("fileId", new TextStructure(FileID));
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid");
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetGetTitle()
        {
            var expectedTitle = "Example Spreadsheet";
            scripter.RunLine("googlesheet.gettitle");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual(expectedTitle, result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("googlesheet.close");
        }
    }
}
