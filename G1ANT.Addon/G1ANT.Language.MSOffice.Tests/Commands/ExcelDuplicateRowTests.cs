
using G1ANT.Addon.MSOffice;

using System;
using System.IO;
using NUnit.Framework;
using System.Threading;

using System.Reflection;
using System.Diagnostics;
using G1ANT.Engine;
using G1ANT.Addon.MSOffice.Tests.Properties;
using G1ANT.Language;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelDuplicateRowTests
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

        [OneTimeSetUp]
        public static void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            scripter = new Scripter();
        }

        [SetUp]
        public void TestInit()
        {
            xlsPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestWorkbook), "xlsm");
            scripter.Variables.SetVariableValue("xlsPath", new TextStructure(xlsPath));
            scripter.RunLine("excel.open ♥xlsPath sheet Add");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelDuplicateRowTest()
        {
            scripter.RunLine("excel.duplicaterow source 1 destination 2");
            scripter.RunLine("excel.getvalue row 2 col 1");
            Assert.AreEqual(3, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelDuplicateRowFailTest()
        {

            scripter.RunLine("excel.duplicaterow source 1 destination 2");
            scripter.Text = "excel.getvalue row 0 col 1";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());

        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelDuplicateRowFail2Test()
        {

            scripter.RunLine("excel.duplicaterow source 1 destination 2");
            scripter.Text = "excel.getvalue row -1 col -1";
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
