

using G1ANT.Addon.MSOffice;


using NUnit.Framework;
using System;
using System.Reflection;
using System.Threading;

using System.Diagnostics;
using G1ANT.Engine;
using G1ANT.Language;
using G1ANT.Addon.MSOffice.Tests.Properties;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelRunMacroTests
    {

        static Scripter scripter;
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

        static string sheetName = "Macro";
        static string macroName = "Calculate";
        static int calculationRow = 6;
        static int calculationValueToBeCountedcolindexumn = 2;
        static int calculationValueExpectedcolindexumn = 1;

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            scripter = new Scripter();
        }

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            xlsPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestWorkbook), "xlsm");
            scripter.Variables.SetVariableValue("xlsPath", new TextStructure(xlsPath));
            scripter.Variables.SetVariableValue("sheet", new TextStructure(sheetName));
            scripter.Variables.SetVariableValue("macroName", new TextStructure(macroName));
            scripter.RunLine($"excel.open {SpecialChars.Variable}xlsPath sheet {SpecialChars.Variable}sheet");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelRunMacroCalculationTest()
        {
            scripter.RunLine($"excel.setvalue {SpecialChars.Text}{SpecialChars.Text} row {calculationRow} colindex {calculationValueToBeCountedcolindexumn}");
            scripter.RunLine($"excel.runmacro {SpecialChars.Variable}macroName");
            scripter.RunLine($"excel.getvalue row {calculationRow} colindex {calculationValueExpectedcolindexumn}");
            int expectedValue = 0;
            Assert.AreEqual(expectedValue, int.Parse(scripter.Variables.GetVariableValue<string>("result")));

            scripter.RunLine($"excel.setvalue {SpecialChars.Text}4{SpecialChars.Text} row {calculationRow} colindex {calculationValueToBeCountedcolindexumn}");
            scripter.RunLine($"excel.runmacro {SpecialChars.Variable}macroName");
            scripter.RunLine($"excel.getvalue row {calculationRow} colindex {calculationValueExpectedcolindexumn}");
            expectedValue = 40;
            Assert.AreEqual(expectedValue, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
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
