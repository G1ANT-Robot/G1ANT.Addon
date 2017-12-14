
using G1ANT.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class WordCloseTests
	{

        static Scripter scripter;

        private void KillProcesses()
        {
            foreach (Process p in Process.GetProcessesByName("word"))
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
        }
        [SetUp]
        public void SetUp()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            scripter = new Scripter();
        }
        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
		public void WordCloseTest()
		{
            KillProcesses();

            Process[] userProcesses = Process.GetProcessesByName("winword");
            
			scripter.Text = @"word.open";
			scripter.Run();
            int tick = 0;
            int starttick = Environment.TickCount;
            int openingDelay = 20000;
			Process[] allProcesses = Process.GetProcessesByName("winword");

            while(allProcesses.Length <= userProcesses.Length && tick <= starttick + openingDelay)
            {
                allProcesses = Process.GetProcessesByName("winword");
                tick = Environment.TickCount;
                Thread.Sleep(10);
            }

			Assert.IsTrue(allProcesses.Length > userProcesses.Length);


            List<Process> diffProcesses = new List<Process>();

			foreach (var proc in allProcesses)
			{
				if (!userProcesses.Contains(proc))
					diffProcesses.Add(proc);
			}

			scripter.Text = @"word.close";
			scripter.Run();
            System.Threading.Thread.Sleep(2000);
            allProcesses = Process.GetProcessesByName("winword");
			Assert.AreEqual(userProcesses.Length, allProcesses.Length);
		}

        [TearDown]
        public void TestCleanUp()
        {
            Process[] proc = Process.GetProcessesByName("word");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
