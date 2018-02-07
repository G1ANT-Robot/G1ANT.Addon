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
    public class GoogleSheetFindAllTests
    {
        Scripter scripter;
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
           scripter.InitVariables.Clear();
           scripter.InitVariables.Add("fileId", new TextStructure(FileID));
           
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetFindValueWhichExistsManyTimes()
        {
            var value1 = "3. Junior";
            var value2 = "Lacrosse";
            scripter.InitVariables.Add("valueToBeFound", new TextStructure(value1));
            scripter.InitVariables.Add("valueToBeFound2", new TextStructure(value2));
            scripter.Text =($@"googlesheet.open {SpecialChars.Variable}fileid isshared false
                           googlesheet.findall value {SpecialChars.Variable}valueToBeFound result {SpecialChars.Variable}result1
                           googlesheet.findall value {SpecialChars.Variable}valueToBeFound2 result {SpecialChars.Variable}result2
googlesheet.close");
            scripter.Run();
            var result1 = scripter.Variables.GetVariable("result1").GetValue().Object;
            Assert.AreEqual("C7&C8&C11&C14&C20&C25", result1);
            
            
            var result2 = scripter.Variables.GetVariable("result2").GetValue().Object;
            Assert.AreEqual("F3&F9&F20&F26&F30", result2);
        }

        [Test]
        [Timeout(40000)]
        public void GoogleSheetFindValueWhichExistsOnce()
        {
            var value = "Anna";
            scripter.InitVariables.Add("valueToBeFound", new TextStructure(value));
            scripter.Text = ($@"googlesheet.open {SpecialChars.Variable}fileid isshared false
                                googlesheet.findall value {SpecialChars.Variable}valueToBeFound
googlesheet.close");
            scripter.Run();
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("A4", result.GetValue().ToString());
        }

        [Test]
        [Timeout(50000)]
        public void GoogleSheetFindValueWhichDoesntExist()
        {
            var value = "notexists";
            scripter.InitVariables.Add("valueToBeFound", new TextStructure(value));
            scripter.Text =($@"googlesheet.open {SpecialChars.Variable}fileid isshared false
                                googlesheet.findall value {SpecialChars.Variable}valueToBeFound
googlesheet.close");
            scripter.Run();
            var result = scripter.Variables.GetVariable("result");
            Assert.AreEqual("", result.GetValue().ToString());
        }

        [TearDown]
        public void TestCleanUp()
        {
        }
    }
}
