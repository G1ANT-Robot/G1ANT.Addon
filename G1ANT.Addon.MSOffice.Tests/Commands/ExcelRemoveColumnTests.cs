
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
    public class ExcelRemovecolindexumnTests
    {
        Scripter scripter;

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
            scripter.RunLine("excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelRemovecolindexumnTest()
        {
            scripter.RunLine("excel.setvalue aaa row 1 colindex 1");
            scripter.RunLine("excel.setvalue bbb row 1 colindex 2");
            scripter.RunLine("excel.insertcolumn colindex 1 where after");
            scripter.RunLine("excel.removecolumn colindex 2");
            scripter.RunLine("excel.getvalue row 1 colindex 2");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));
            scripter.RunLine("excel.insertcolumn colindex 2 where before");
            scripter.RunLine("excel.removecolumn colname b");
            scripter.RunLine("excel.getvalue row 1 colindex 2");
            Assert.AreEqual("bbb", scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        public void ExcelRemovecolindexumnFailTest()
        {
                scripter.Text = "excel.removecolumn colindex hadhaad2radfa";
                Exception exception = Assert.Throws<ApplicationException>(delegate
                {
                    scripter.Run();
                });
                Assert.IsInstanceOf<ApplicationException>(exception.GetBaseException());
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
