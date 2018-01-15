﻿
using G1ANT.Addon.MSOffice;
using G1ANT.Engine;
using System;
using System.IO;
using NUnit.Framework;
using System.Reflection;


using System.Threading;
using System.Diagnostics;
using G1ANT.Language;
using G1ANT.Addon.MSOffice.Tests.Properties;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelGetFormulaTests
    {
        Scripter scripter;
        static string xlsPath;
        static string formula = "=A1+B1";

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
        public void ExcelgetFormulaTest()
        {
            scripter.RunLine("excel.getformula row 1 colindex 3");
            Assert.AreEqual(formula, scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelgetFormula2Test()
        {
            scripter.RunLine("excel.getformula row 10 colindex 10");
            Assert.AreEqual(string.Empty, scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelgetFormula3Test()
        {
            scripter.Text = "excel.getformula row -1 colindex 10";
            Exception exception = Assert.Throws<ApplicationException>(delegate
                {
                    scripter.Run();
                });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelgetFormula5Test()
        {
            scripter.Text = $"excel.getformula row -1 colname żd2";
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
