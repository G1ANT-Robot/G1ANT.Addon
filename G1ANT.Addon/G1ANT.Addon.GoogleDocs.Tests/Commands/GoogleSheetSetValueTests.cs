using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Language.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetSetValueTests
    {
        static Scripter scripter;
        static string FileID = "147EH2vEjGVtbzzkT6XaI0eNZlY5Ec91wlvxN3HC4GMc"; //google sheets example file
        string resultBeforeChange;
        string rangeToBePlaced;

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
        [Timeout(50000)]
        public void GoogleSheetSetValue()
        {
            rangeToBePlaced = "A2";
            scripter.Variables.SetVariableValue("rangeToBePlaced", new Language.Structures.String(rangeToBePlaced));
            var valueToBePlaced = "G1ANT";
            scripter.Variables.SetVariableValue("valueToBePlaced", new Language.Structures.String(valueToBePlaced));
            scripter.RunLine($"googlesheet.getvalue range {SpecialChars.Variable}rangeToBePlaced");

            resultBeforeChange = scripter.Variables.GetVariable("result").Value.GetValue().ToString();
            scripter.RunLine($"googlesheet.setvalue range {SpecialChars.Variable}rangeToBePlaced value {SpecialChars.Variable}valueToBePlaced numeric false");
            scripter.RunLine($"googlesheet.getvalue range {SpecialChars.Variable}rangeToBePlaced");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual(valueToBePlaced, result.Value.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.Variables.SetVariableValue("valueToBePlaced", new Language.Structures.String(resultBeforeChange));
            scripter.RunLine($"googlesheet.setvalue range {SpecialChars.Variable}rangeToBePlaced value {SpecialChars.Variable}valueToBePlaced numeric false");
            scripter.RunLine($"googlesheet.getvalue range {SpecialChars.Variable}rangeToBePlaced");
            var returnedToPreviousState = scripter.Variables.GetVariable("result").Value.GetValue().ToString();
            Assert.AreEqual(resultBeforeChange, returnedToPreviousState);
            scripter.RunLine("googlesheet.close");
        }
    }
}
