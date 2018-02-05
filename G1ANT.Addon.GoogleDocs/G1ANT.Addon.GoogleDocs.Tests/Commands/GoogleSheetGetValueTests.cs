using G1ANT.Engine;
using G1ANT.Language;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;


namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetGetValueTests
    {
        Scripter scripter;
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
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("fileId", new TextStructure(FileID));
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid");
        }

        [Test]
        [Timeout(40000)]
        public void GoogleSheetGetValue()
        {
            rangeToBeChecked = "A3";
            string expectedValue = "Andrew";
           scripter.InitVariables.Add("rangeToBeChecked", new TextStructure(rangeToBeChecked));
            scripter.RunLine($"googlesheet.getvalue range {SpecialChars.Variable}rangeToBeChecked");
          System.Collections.Generic.List<object> result = (System.Collections.Generic.List<object>)scripter.Variables.GetVariable("result").GetValue().Object;
            Assert.AreEqual(expectedValue, result[0].ToString()); 
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("googlesheet.close");
        }
    }
}
