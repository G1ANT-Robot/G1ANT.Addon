﻿using G1ANT.Engine;
using G1ANT.Language.Xls.Tests.Properties;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Threading;

namespace G1ANT.Language.Xls.Tests
{
    [TestFixture]
    public class CountRowsTests
    {
        Scripter scripter;
        string file;
        string file2;

        [OneTimeSetUp]
        [Timeout(20000)]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            file2 = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.EmptyWorkbook), "xlsx");
        }
        [SetUp]
        public void testinit()
        {
            scripter = new Scripter();
        }

        [Test]
        [Timeout(20000)]
        public void CountRowsTest()
        {            
            int rowCount;
            scripter.RunLine($"xls.open {SpecialChars.Text}{file}{SpecialChars.Text}");
            scripter.RunLine($"xls.countrows result {nameof(rowCount)}");
            rowCount = scripter.Variables.GetVariableValue<int>(nameof(rowCount), -1, true);
            Assert.AreEqual(4, rowCount);
        }

        [Test]
        [Timeout(20000)]
        public void CountRowsInEmptyWoorkbookTest()
        {
            int rowCount;
            scripter.RunLine($"xls.open {SpecialChars.Text}{file2}{SpecialChars.Text}");
            scripter.RunLine($"xls.countrows result {nameof(rowCount)}");
            rowCount = scripter.Variables.GetVariableValue<int>(nameof(rowCount), -1, true);
            NUnit.Framework.Assert.AreEqual(0, rowCount);
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