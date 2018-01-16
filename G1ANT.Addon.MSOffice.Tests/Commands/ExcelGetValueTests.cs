﻿

using G1ANT.Addon.MSOffice.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelGetValueTests
    {
        Scripter scripter;
        static string xlsPath;

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
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
           
        }

        [SetUp]
        public void TestInit()
        {
            scripter = new Scripter();
            scripter.InitVariables.Clear();
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            xlsPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestWorkbook), "xlsm");
           scripter.InitVariables.Add("xlsPath", new TextStructure(xlsPath));
            scripter.RunLine($"excel.open {SpecialChars.Variable}xlsPath sheet Add");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelGetValueTest()
        {
            scripter.RunLine("excel.getvalue row 1 colindex 1");
            Assert.AreEqual(3, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
            scripter.RunLine("excel.getvalue row 1 colindex 2");
            Assert.AreEqual(4, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
            scripter.RunLine("excel.getvalue row 1 colindex 3");
            Assert.AreEqual(7, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelGetValueFailTest()
        {
            scripter.Text = "excel.getvalue row 0 colindex 1";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelGetValueFail2Test()
        {
            scripter.Text = "excel.getvalue row 1 colindex 0";
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
