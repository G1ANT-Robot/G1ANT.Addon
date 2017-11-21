

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
    public class ExcelImportTextTests
	{
		static string csvPath;
		static Scripter scripter;

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
            csvPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestData), "csv");
            scripter.Variables.SetVariableValue("csvPath", new TextStructure(csvPath));
            scripter.RunLine($"excel.open {SpecialChars.Variable}csvPath");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout), Ignore("Passing point as argument to commands doesnt work")]
        public void ExcelImportTextTest()
		{
            string twentyOne = 21.ToString();
            string thirtyTwo = 32.ToString();

			scripter.RunLine($"excel.importtext path {SpecialChars.Variable}csvPath delimiter ,");
			scripter.RunLine("excel.getvalue row 2 col 1");
			Assert.AreEqual(twentyOne, scripter.Variables.GetVariableValue<string>("result"));
			scripter.RunLine("excel.getvalue row 3 col 2");
			Assert.AreEqual(thirtyTwo, scripter.Variables.GetVariableValue<string>("result"));

            scripter.RunLine($"excel.importtext path {SpecialChars.Variable}csvPath destination D1 delimiter ,");
            scripter.RunLine("excel.getvalue row 2 col 4");
            Assert.AreEqual(twentyOne, scripter.Variables.GetVariableValue<string>("result"));
            scripter.RunLine("excel.getvalue row 3 col 5");
            Assert.AreEqual(thirtyTwo, scripter.Variables.GetVariableValue<string>("result"));
            
            // passing point by command parameter has to be fixed, i'm aware of the fact that it's not working for now, fogbugz case created
            scripter.RunLine($"excel.importtext path {SpecialChars.Variable}csvPath destination (point)4,1 delimiter ,");
            scripter.RunLine("excel.getvalue row 5 col 1");
            Assert.AreEqual(twentyOne, scripter.Variables.GetVariableValue<string>("result"));
            scripter.RunLine("excel.getvalue row 6 col 2");
            Assert.AreEqual(thirtyTwo, scripter.Variables.GetVariableValue<string>("result"));
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
