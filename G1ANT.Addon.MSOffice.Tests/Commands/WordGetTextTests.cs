


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
    public class WordGetTextTests
    {
        static Scripter scripter;
        static string wordPath;
        static string valueTested = "Test, test, test....test";
        static string expected = "Test, test, test....testTest, test, test....test";
        static string expectedEmptyString = string.Empty;

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
            wordPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestDocument), "docx");
            scripter.Variables.SetVariableValue("wordPath", new TextStructure(wordPath));
        }

        [SetUp]
        public void TestInit()
        {
            scripter.RunLine($"word.open {SpecialChars.Variable}wordPath");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordGetTextTest()
        {
            scripter.RunLine($"word.inserttext {SpecialChars.Text}{valueTested}{SpecialChars.Text}");
            scripter.RunLine($"word.inserttext {SpecialChars.Text}{valueTested}{SpecialChars.Text}");
            scripter.RunLine($"word.gettext");
            string trimmedValue = scripter.Variables.GetVariableValue<string>("result").Trim();
            Assert.AreEqual(expected, trimmedValue);

            scripter.RunLine($"word.inserttext {SpecialChars.Text}{valueTested}{SpecialChars.Text}");
            scripter.RunLine($"word.inserttext {SpecialChars.Text}{expectedEmptyString}{SpecialChars.Text} replacealltext true ");
            scripter.RunLine($"word.gettext");
            trimmedValue = scripter.Variables.GetVariableValue<string>("result").Trim();
            Assert.AreEqual(expectedEmptyString, trimmedValue);
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
