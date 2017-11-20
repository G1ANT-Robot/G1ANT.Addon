﻿using System;
using System.Collections.Generic;
using System.IO;

using G1ANT.Engine;

using NUnit.Framework;
using G1ANT.Language.Xls.Tests.Properties;
using System.Reflection;

namespace G1ANT.Language.Xls.Tests
{
    [TestFixture]
    public class XlsFindTest
    {
         string file;
         string file2;
        static Scripter scripter;
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
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("xlsPath", new TextStructure(file));
        }
        [Test]
        [Timeout(20000)]
        public void XlsFindIntTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}1234{SpecialChars.Text} result testint");
            Assert.AreEqual("A1", scripter.Variables.GetVariableValue<string>("testint"));
        }
        [Test]
        [Timeout(20000)]
        public void XlsFindStringTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}abcd{SpecialChars.Text} result teststring");
            Assert.AreEqual("B1", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindNumberTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}150{SpecialChars.Text} result teststring");
            Assert.AreEqual("D1", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindPercentTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}160%{SpecialChars.Text} result teststring");
            Assert.AreEqual("E1", scripter.Variables.GetVariableValue<string>("teststring"));
            scripter.RunLine($"xls.find {SpecialChars.Text}100%{SpecialChars.Text} result teststring");
            Assert.AreEqual("E2", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindDateTest()
        {
            scripter.RunLine($"xls.find {SpecialChars.Text}21.07.2017{SpecialChars.Text} result teststring");
            Assert.AreEqual("C1", scripter.Variables.GetVariableValue<string>("teststring"));
            scripter.RunLine($"xls.find {SpecialChars.Text}22.07.2017{SpecialChars.Text} result teststring");
            Assert.AreEqual("C2", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsFailToFind()
        {
                scripter.RunLine($"xls.find {SpecialChars.Text}01.01.1001{SpecialChars.Text} result teststring");
            Assert.AreEqual(-1,int.Parse(scripter.Variables.GetVariable("teststring").GetValue().ToString()));
        }

        

        [SetUp]
        [Timeout(20000)]
        public void TestInit()
        {
            scripter.RunLine($"xls.open  {SpecialChars.Variable}xlsPath result id");
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
