

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
    public class WordReplaceTests
    {
        static Scripter scripter;
        static String replaceFrom = "tro";
        static String replaceTo = "lo";
        static String restOfText = "lololololo";
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
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("text", new TextStructure(replaceFrom + restOfText));
        }

        [SetUp]
        public void TestInit()
        {
            scripter.RunLine($"word.open");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordReplaceTest()
        {
            scripter.RunLine($"word.inserttext {SpecialChars.Variable}text");
            scripter.RunLine($"word.replace from {replaceFrom} to {replaceTo}");
            scripter.RunLine($"word.gettext");
            Assert.AreEqual(replaceTo + restOfText, scripter.Variables.GetVariableValue<string>("result").Trim());
        }


        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine("word.close");
            Process[] proc = Process.GetProcessesByName("word");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
