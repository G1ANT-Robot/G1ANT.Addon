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
            
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetSetTitle()
        {
           
            var valueToBePlaced = "G1ANT";
            scripter.InitVariables.Add("valueToBePlaced", new TextStructure(valueToBePlaced));

            scripter.Text = ($@"googlesheet.open {SpecialChars.Variable}fileid
                                googlesheet.gettitle result {SpecialChars.Variable}before
                                googlesheet.settitle {SpecialChars.Variable}valueToBePlaced
                                googlesheet.gettitle
                                googlesheet.close");
            
            scripter.Run();
            var result = scripter.Variables.GetVariable("result");
            titleBeforeChange = scripter.Variables.GetVariable("before").GetValue().ToString();
            Assert.AreEqual(valueToBePlaced, result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.InitVariables.Add("valueToBePlacedback", new TextStructure(titleBeforeChange));
            scripter.Text = ($@"googlesheet.open {SpecialChars.Variable}fileid
                                googlesheet.settitle {SpecialChars.Variable}valueToBePlacedback
                                googlesheet.gettitle
googlesheet.close");
            var returnedToPreviousState = scripter.Variables.GetVariable("result").GetValue().ToString();
            Assert.AreEqual(titleBeforeChange, returnedToPreviousState);
        }
    }
}
