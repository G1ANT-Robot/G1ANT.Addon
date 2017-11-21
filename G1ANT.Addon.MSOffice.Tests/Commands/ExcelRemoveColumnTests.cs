
using G1ANT.Addon.MSOffice;

using System;
using NUnit.Framework;
using System.Reflection;

using System.Threading;
using System.Diagnostics;
using G1ANT.Engine;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelRemoveColumnTests
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
            scripter.RunLine("excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelRemoveColumnTest()
        {
            scripter.RunLine("excel.setvalue aaa row 1 col 1");
            scripter.RunLine("excel.setvalue bbb row 1 col 2");
            scripter.RunLine("excel.insertcolumn column 1 where after");
            scripter.RunLine("excel.removecolumn column 2");
            scripter.RunLine("excel.getvalue row 1 col 2");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));
            scripter.RunLine("excel.insertcolumn column 2 where before");
            scripter.RunLine("excel.removecolumn column b");
            scripter.RunLine("excel.getvalue row 1 col 2");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        public void ExcelRemoveColumnFailTest()
        {
                scripter.Text = "excel.removecolumn column hadhaad2radfa";
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
