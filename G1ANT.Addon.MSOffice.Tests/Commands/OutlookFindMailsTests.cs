

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
    public class OutlookFindMailsTests
    {
        static Scripter scripter;

        private void KillProcesses()
        {
            foreach (Process p in Process.GetProcessesByName("outlook"))
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
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            scripter = new Scripter();
            string email = "g1ant.robot.tester@gmail.com";
            string subject = "test" + DateTime.Now;
            string text = "example text";
            scripter.Text =
                $@"outlook.open
			    delay 1
			    outlook.newmessage {SpecialChars.Variable}email subject {SpecialChars.Variable}sbj body {SpecialChars.Variable}txt
			    delay 1
			    outlook.send
			    delay 20";
            scripter.Variables.SetVariableValue("email", new TextStructure(email));
            scripter.Variables.SetVariableValue("sbj", new TextStructure(subject));
            scripter.Variables.SetVariableValue("txt", new TextStructure(text));
            scripter.Run();
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout + 20000)]
        public void OutlookFindMailsTest()
        {
            scripter.Text =
                $@"outlook.findmails search {SpecialChars.Variable}sbj";
            scripter.Run();
            string res = scripter.Variables.GetVariableValue<string>("result");
            Assert.AreEqual(true, Boolean.Parse(res));
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("outlook.close");
            Process[] proc = Process.GetProcessesByName("outlook");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
