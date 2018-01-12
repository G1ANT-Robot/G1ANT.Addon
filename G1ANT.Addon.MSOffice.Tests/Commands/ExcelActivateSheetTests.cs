
using System;
using System.IO;
using NUnit.Framework;
using System.Threading;

using System.Reflection;
using System.Diagnostics;
using G1ANT.Engine;
using G1ANT.Language;
using G1ANT.Addon.MSOffice.Tests.Properties;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelActivateSheetTests
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
            xlsPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestWorkbook), "xlsm");  
           scripter.InitVariables.Add("xlsPath", new TextStructure(xlsPath));
            scripter.RunLine($"excel.open {SpecialChars.Variable}xlsPath");
        }

        

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelActivateSheetTest()
        {
            scripter.RunLine($"excel.activatesheet name {SpecialChars.Text}Macro{SpecialChars.Text}");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelActivateSheetFailTest()
        {

            scripter.Text = $"excel.activatesheet name {SpecialChars.Text}aaaa{SpecialChars.Text}";
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
