


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
    public class ExcelSwitchTests
	{
		static Engine.Scripter scripter;
		static string xlsPath;
		static int someVal = 10;

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
        }

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            xlsPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestWorkbook), "xlsm");
            scripter.Variables.SetVariableValue("xlsPath", new TextStructure(xlsPath));
            scripter.Variables.SetVariableValue("val", new IntegerStructure(someVal));
            scripter.RunLine($"excel.open {SpecialChars.Text}{SpecialChars.Text} result {SpecialChars.Variable}id");
            scripter.RunLine($"excel.open {SpecialChars.Variable}xlsPath sheet Add result {SpecialChars.Variable}id2");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
		public void ExcelSwitchTest()
		{
			scripter.RunLine($"excel.switch {SpecialChars.Variable}id");
			scripter.RunLine($"excel.setvalue {SpecialChars.Variable}val row 1 col 1");
			scripter.RunLine($"excel.switch {SpecialChars.Variable}id2");
			scripter.RunLine("excel.getvalue row 1 col 1");
			Assert.AreNotEqual(someVal, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
			scripter.RunLine("excel.activatesheet Macro");
			scripter.RunLine($"excel.getvalue row 6 col 2 result {SpecialChars.Variable}val2");
			scripter.RunLine($"excel.switch {SpecialChars.Variable}id");
			scripter.RunLine("excel.getvalue row 1 col 1");
			Assert.AreEqual(someVal, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
			scripter.RunLine($"excel.switch {SpecialChars.Variable}id2");
			scripter.RunLine("excel.getvalue row 6 col 2");
            Assert.AreEqual(int.Parse(scripter.Variables.GetVariableValue<string>("val2")), int.Parse(scripter.Variables.GetVariableValue<string>("result")));
		}

		[TearDown]
		public void TestCleanUp()
		{
			scripter.RunLine($"excel.switch {SpecialChars.Variable}id");
			scripter.RunLine("excel.close");
			scripter.RunLine($"excel.switch {SpecialChars.Variable}id2");
			scripter.RunLine("excel.close");
            Process[] proc = Process.GetProcessesByName("excel");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
	}
}
