using G1ANT.Addon.MSOffice.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class WordRunMacroTests
    {
        Scripter scripter;
        static string wordPath;
        static string macroName = "SortText";
        static string testedValue = $"Pawel\rPatryk\rMarcin\rZuza\rChris\rMichal\rDiana\rPrzemek\rJano\r";
        static string expectedValue = $"\rChris\rDiana\rJano\rMarcin\rMichal\rPatryk\rPawel\rPrzemek\rZuza\r";
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
scripter.InitVariables.Clear();
        }

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            wordPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestDocumentMacro), "docm");
           scripter.InitVariables.Add("wordPath", new TextStructure(wordPath));
           scripter.InitVariables.Add("macroName", new TextStructure(macroName));
            scripter.RunLine($"word.open {SpecialChars.Variable}wordPath");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordRunMacroTest()
        {
            scripter.RunLine($"word.inserttext {SpecialChars.Text}{testedValue}{SpecialChars.Text}");
            scripter.RunLine($"word.runmacro {SpecialChars.Variable}macroName");
            scripter.RunLine($"word.gettext");
            string trimmedValue = scripter.Variables.GetVariableValue<string>("result");
            Assert.AreEqual(expectedValue, trimmedValue);
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
