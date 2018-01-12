
using G1ANT.Addon.MSOffice;
using System.Diagnostics;
using System;
using NUnit.Framework;
using System.Threading;
using G1ANT.Engine;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExcelCloseTests
    {
        static Scripter scripter;
        int userProcessCount;

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
            userProcessCount = Process.GetProcessesByName("excel").Length;
            scripter.RunLine("excel.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelCloseTest()
        {
            scripter.RunLine("excel.close");
            scripter.RunLine("delay 1");
            Assert.AreEqual(userProcessCount, Process.GetProcessesByName("excel").Length);
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void ExcelCloseFailTest()
        {
            scripter.RunLine("excel.close");
            scripter.RunLine("delay 1");
            scripter.Text = "excel.close";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
            Assert.AreEqual(userProcessCount, Process.GetProcessesByName("excel").Length);
        }

        [TearDown]
        public void TestCleanUp()
        {
            Process[] proc = Process.GetProcessesByName("excel");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
