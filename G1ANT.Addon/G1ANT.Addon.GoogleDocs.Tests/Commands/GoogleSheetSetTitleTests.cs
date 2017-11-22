﻿using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Language.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetSetTitleTests
    {
        static Scripter scripter;
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
            scripter.Variables.SetVariableValue("fileId", new Language.Structures.String(FileID));
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid");
            scripter.RunLine("googlesheet.gettitle");
            titleBeforeChange = scripter.Variables.GetVariable("result").Value.GetValue().ToString();
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetSetTitle()
        {
            var valueToBePlaced = "G1ANT";
            scripter.Variables.SetVariableValue("valueToBePlaced", new Language.Structures.String(valueToBePlaced));
            scripter.RunLine($"googlesheet.settitle {SpecialChars.Variable}valueToBePlaced");
            scripter.RunLine("googlesheet.gettitle");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual(valueToBePlaced, result.Value.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.Variables.SetVariableValue("valueToBePlaced", new Language.Structures.String(titleBeforeChange));
            scripter.RunLine($"googlesheet.settitle {SpecialChars.Variable}valueToBePlaced");
            scripter.RunLine("googlesheet.gettitle");
            var returnedToPreviousState = scripter.Variables.GetVariable("result").Value.GetValue().ToString();
            Assert.AreEqual(titleBeforeChange, returnedToPreviousState);
            scripter.RunLine("googlesheet.close");
        }
    }
}
