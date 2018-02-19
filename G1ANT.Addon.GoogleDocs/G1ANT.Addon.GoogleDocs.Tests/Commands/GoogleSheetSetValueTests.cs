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
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetSetValueTests
    {
        Scripter scripter;
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
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("fileid", new TextStructure(FileID));
            
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetSetValue()
        {
            var valueToBePlaced = "G1ANT";
            rangeToBePlaced = "A2";
            scripter.InitVariables.Add("valueToBePlaced", new TextStructure(valueToBePlaced));
            scripter.InitVariables.Add("rangeToBePlaced", new TextStructure(rangeToBePlaced));
            scripter.Text =($@"googlesheet.open {SpecialChars.Variable}fileid
                               googlesheet.getvalue range {SpecialChars.Variable}rangeToBePlaced result {SpecialChars.Variable}result1
                               googlesheet.setvalue range {SpecialChars.Variable}rangeToBePlaced value {SpecialChars.Variable}valueToBePlaced numeric false
                               googlesheet.getvalue range {SpecialChars.Variable}rangeToBePlaced result {SpecialChars.Variable}result2");
            scripter.Run();
            resultBeforeChange = scripter.Variables.GetVariable("result1").GetValue().ToString();
            var result = scripter.Variables.GetVariable("result2");
            Assert.AreEqual(valueToBePlaced, result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("valueToBePlaced", new TextStructure(resultBeforeChange));
            scripter.InitVariables.Add("rangeToBePlaced", new TextStructure(rangeToBePlaced));
            scripter.Text =($@"googlesheet.setvalue range {SpecialChars.Variable}rangeToBePlaced value {SpecialChars.Variable}valueToBePlaced numeric false
                               googlesheet.getvalue range {SpecialChars.Variable}rangeToBePlaced result {SpecialChars.Variable}returned
                               googlesheet.close");
            scripter.Run();
            var returnedToPreviousState = scripter.Variables.GetVariable("returned").GetValue().ToString();
            Assert.AreEqual(resultBeforeChange, returnedToPreviousState);
            
        }
    }
}
