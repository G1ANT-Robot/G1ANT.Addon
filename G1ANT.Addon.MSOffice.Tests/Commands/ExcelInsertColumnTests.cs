
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using G1ANT.Engine;
namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelInsertColumnTests
    {

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
            scripter.RunLine($"excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelInsertColumnTest()
        {
            scripter.RunLine("excel.setvalue aaa row 1 col 1");
            scripter.RunLine("excel.setvalue bbb row 1 col 2");
            scripter.RunLine("excel.insertcolumn column 1 where after");
            scripter.RunLine("excel.getvalue row 1 col 3");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));

            scripter.RunLine("excel.removecolumn column 2");
            scripter.RunLine("excel.insertcolumn column b where before");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelInsertRowFailTest()
        {

            scripter.Text = "excel.insertcolumn column 1 where haha";
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

            scripter.Text = "excel.insertcolumn column 0 where after";
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
