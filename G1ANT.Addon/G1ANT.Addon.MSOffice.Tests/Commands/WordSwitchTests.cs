

using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class WordSwitchTests
    {
        static Scripter scripter;
        static String someText = "lololololo";
        static String someText2 = "trolololololo";

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
            scripter.Variables.SetVariableValue("text", new TextStructure(someText));
            scripter.Variables.SetVariableValue("text2", new TextStructure(someText2));
        }

        [SetUp]
        public void TestInit()
        {
            scripter.RunLine($"word.open result id");
            scripter.RunLine($"word.open result id2");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordSwitchTest()
        {
            scripter.RunLine($"word.switch {SpecialChars.Variable}id");
            scripter.RunLine($"word.inserttext {SpecialChars.Variable}text");
            scripter.RunLine($"word.switch {SpecialChars.Variable}id2");
            scripter.RunLine($"word.inserttext {SpecialChars.Variable}text2");
            scripter.RunLine($"word.switch {SpecialChars.Variable}id");
            scripter.RunLine($"word.gettext");
            Assert.AreEqual(scripter.Variables.GetVariableValue<string>("result").Trim(), someText);
            scripter.RunLine($"word.switch {SpecialChars.Variable}id2");
            scripter.RunLine($"word.gettext");
            Assert.AreEqual(scripter.Variables.GetVariableValue<string>("result").Trim(), someText2);
        }

        [TearDown]
        public void TestCleanUp()
        {
            scripter.RunLine($"word.switch {SpecialChars.Variable}id");
            scripter.RunLine("word.close");
            scripter.RunLine($"word.switch {SpecialChars.Variable}id2");
            scripter.RunLine("word.close");
            Process[] proc = Process.GetProcessesByName("word");
            if (proc.Length != 0)
            {
                KillProcesses();
            }
        }
    }
}
