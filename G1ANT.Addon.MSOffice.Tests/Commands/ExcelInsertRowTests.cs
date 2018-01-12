﻿
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using G1ANT.Engine;
namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelInsertRowTests
    {

        Scripter scripter;

        private void KillProcesses()
        {
            foreach (Process p in Process.GetProcessesByName("excel"))
            {
                try
                {
                    p.Kill();
                }
                catch { }
            }
        }

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            scripter = new Scripter();
scripter.InitVariables.Clear();
        }

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            scripter.RunLine($"excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelInsertRowTest()
        {
            scripter.RunLine("excel.setvalue aaa row 1 colindex 1");
            scripter.RunLine("excel.setvalue bbb row 2 colindex 1");
            scripter.RunLine("excel.insertrow row 1 where below");
            scripter.RunLine("excel.getvalue row 3 colindex 1");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));

            scripter.RunLine("excel.removerow row 2");
            scripter.RunLine("excel.insertrow row 2 where above");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelInsertRowFailTest()
        {
            scripter.Text = "excel.insertrow row 1 where aaa";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelInsertRowFail2Test()
        {
                scripter.Text = "excel.insertrow row 0 where below";
                Exception exception = Assert.Throws<ApplicationException>(delegate
                {
                    scripter.Run();
                });
                Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
            }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("excel.close");
            Process[] proc = Process.GetProcessesByName("excel");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
