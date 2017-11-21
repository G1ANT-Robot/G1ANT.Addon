

using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelAddSheetTests
    {
        static Scripter scripter;
        static string sheetName = "TestSheet";
        static string otherSheet = "otherSheet";

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
            scripter.Variables.SetVariableValue("TestSheet", new TextStructure(sheetName));
            scripter.Variables.SetVariableValue("otherSheet", new TextStructure(otherSheet));
            scripter.RunLine("excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelAddSheetTest()
        {
            scripter.RunLine($"excel.addsheet {SpecialChars.Variable}TestSheet");
            scripter.RunLine($"excel.activatesheet {SpecialChars.Variable}TestSheet");
            scripter.RunLine("excel.setvalue 1 col 1 row 1");
            scripter.RunLine("excel.getvalue row 1 col 1 result valTest");
            Assert.AreEqual(1, int.Parse(scripter.Variables.GetVariableValue<string>("valTest")));

            scripter.RunLine($"excel.addsheet {SpecialChars.Variable}otherSheet");
            scripter.RunLine($"excel.activatesheet {SpecialChars.Variable}otherSheet");
            scripter.RunLine("excel.setvalue 5 col 1 row 1");
            scripter.RunLine("excel.getvalue row 1 col 1 result valOther");
            Assert.AreEqual(5, int.Parse(scripter.Variables.GetVariableValue<string>("valOther")));

            scripter.RunLine($"excel.activatesheet {SpecialChars.Variable}TestSheet");
            scripter.RunLine("excel.getvalue row 1 col 1 result val");
            Assert.AreEqual(1, int.Parse(scripter.Variables.GetVariableValue<string>("val")));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelAddSheetFailTest()
        {

            scripter.RunLine($"excel.addsheet {SpecialChars.Variable}TestSheet");
            scripter.Text = $"excel.addsheet {SpecialChars.Variable}TestSheet";
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
