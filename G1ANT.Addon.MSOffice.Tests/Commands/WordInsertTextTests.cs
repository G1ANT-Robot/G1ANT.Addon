
using G1ANT.Addon.MSOffice;


using NUnit.Framework;
using System;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using G1ANT.Engine;
using G1ANT.Addon.MSOffice.Tests.Properties;
using G1ANT.Language;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class WordInsertTextTests
    {
        static Scripter scripter;
        static string wordPath;
        static string valueTested = "Test";
        static string expected = "Test";
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
scripter.InitVariables.Clear();
            wordPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.TestDocument), "docx");
           scripter.InitVariables.Add("wordPath", new TextStructure(wordPath));
        }

        [SetUp]
        public void TestInit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.MSOffice.dll");
            scripter.RunLine($"word.open {SpecialChars.Variable}wordPath");
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordInsertTextTest()
        {
            scripter.RunLine($"word.inserttext {SpecialChars.Text}{valueTested}{SpecialChars.Text}");
            scripter.RunLine($"word.gettext");
            string trimmedValue = scripter.Variables.GetVariableValue<string>("result").Trim();
            Assert.AreEqual(expected, trimmedValue);

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
