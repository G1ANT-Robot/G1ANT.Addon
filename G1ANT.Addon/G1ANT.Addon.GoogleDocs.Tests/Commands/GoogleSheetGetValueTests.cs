using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;


namespace G1ANT.Language.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetGetValueTests
    {
        static Scripter scripter;
        static string FileID = "147EH2vEjGVtbzzkT6XaI0eNZlY5Ec91wlvxN3HC4GMc"; //google sheets example file
        string rangeToBeChecked;

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void Init()
        {
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("fileId", new Language.Structures.String(FileID));
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid");
        }

        [Test]
        [Timeout(40000)]
        public void GoogleSheetGetValue()
        {
            rangeToBeChecked = "A2";
            string expectedValue = "Alexandra";
            scripter.Variables.SetVariableValue("rangeToBeChecked", new Language.Structures.String(rangeToBeChecked));
            scripter.RunLine($"googlesheet.getvalue range {SpecialChars.Variable}rangeToBeChecked");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual(expectedValue, result.Value.GetValue().ToString()); 
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("googlesheet.close");
        }
    }
}
