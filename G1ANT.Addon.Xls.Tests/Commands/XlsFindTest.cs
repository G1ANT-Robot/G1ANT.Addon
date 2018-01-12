using System;
using System.Collections.Generic;
using System.IO;

using G1ANT.Engine;

using NUnit.Framework;
using System.Reflection;
using G1ANT.Language;
using G1ANT.Addon.Xls.Tests.Properties;

namespace G1ANT.Addon.Xls.Tests
{
    [TestFixture]
    public class XlsFindTest
    {
         string file;
         string file2;
        Scripter scripter;
        [OneTimeSetUp]
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            file2 = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.EmptyWorkbook), "xlsx");

        }
        [SetUp]
        public void testinit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Xls.dll");
            scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("xlsPath", new TextStructure(file));
            scripter.RunLine($"xls.open  {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id");
        }
        [Test]
        [Timeout(20000)]
        public void XlsFindIntTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}1234{SpecialChars.Text} result {SpecialChars.Variable}testint");
            Assert.AreEqual("A1", scripter.Variables.GetVariable("testint").GetValue().Object);
        }
        [Test]
        [Timeout(20000)]
        public void XlsFindStringTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}abcd{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("B1", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindNumberTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}150{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("D1", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindPercentTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}160%{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("E1", scripter.Variables.GetVariableValue<string>("teststring"));
            scripter.RunLine($"xls.find {SpecialChars.Text}100%{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("E2", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindDateTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}21.07.2017{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("C1", scripter.Variables.GetVariableValue<string>("teststring"));
            scripter.RunLine($"xls.find {SpecialChars.Text}22.07.2017{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("C2", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFailToFind()
        {
                scripter.RunLine($"xls.find {SpecialChars.Text}01.01.1001{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual(-1,int.Parse(scripter.Variables.GetVariable("teststring").GetValue().ToString()));
        }

        [TearDown]
        [Timeout(20000)]
        public void TestCleanUp()
        {
            try
            {
                scripter.RunLine("xls.close");
            }
            catch { }
        }
    }
}
