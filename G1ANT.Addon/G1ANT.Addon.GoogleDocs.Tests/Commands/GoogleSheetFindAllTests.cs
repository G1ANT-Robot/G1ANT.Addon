﻿using G1ANT.Engine;
using G1ANT.Language.Core.Commands;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Language.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetFindAllTests
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
            scripter.Variables.SetVariableValue("fileId", new Language.Structures.String(FileID));
            scripter.RunLine($"googlesheet.open {SpecialChars.Variable}fileid isshared false");
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetFindValueWhichExistsManyTimes()
        {
            var value1 = "3. Junior";
            scripter.Variables.SetVariableValue("valueToBeFound", new Language.Structures.String(value1));
            scripter.RunLine($"googlesheet.findall value {SpecialChars.Variable}valueToBeFound");
            var result1 = scripter.Variables.GetVariable("result");
            Assert.AreEqual("C7&C8&C11&C14&C20&C25", result1.Value.GetValue().ToString());

            var value2 = "Lacrosse";
            scripter.Variables.SetVariableValue("valueToBeFound", new Language.Structures.String(value2));
            scripter.RunLine($"googlesheet.findall value {SpecialChars.Variable}valueToBeFound");
            var result2 = scripter.Variables.GetVariable("result");
            Assert.AreEqual("F3&F9&F20&F26&F30", result2.Value.GetValue().ToString());
        }

        [Test]
        [Timeout(40000)]
        public void GoogleSheetFindValueWhichExistsOnce()
        {
            var value = "Anna";
            scripter.Variables.SetVariableValue("valueToBeFound", new Language.Structures.String(value));
            scripter.RunLine($"googlesheet.findall value {SpecialChars.Variable}valueToBeFound");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("A4", result.Value.GetValue().ToString());
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetFindValueWhichDoesntExist()
        {
            var value = "notexists";
            scripter.Variables.SetVariableValue("valueToBeFound", new Language.Structures.String(value));
            scripter.RunLine($"googlesheet.findall value {SpecialChars.Variable}valueToBeFound");
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("", result.Value.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("googlesheet.close");
        }
    }
}
