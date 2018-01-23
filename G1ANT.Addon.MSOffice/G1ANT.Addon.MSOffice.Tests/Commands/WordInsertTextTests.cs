
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
        Scripter scripter;
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
        public void ClassInit()
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
            
        }

        [Test]
        [Timeout(MSOfficeTests.TestsTimeout)]
        public void WordInsertTextTest()
        {
            scripter.Text = ($@"word.open {SpecialChars.Variable}wordPath
                               word.inserttext {SpecialChars.Text}{valueTested}{SpecialChars.Text}
                               word.gettext result {SpecialChars.Variable}result1
                               word.inserttext {SpecialChars.Text}{expectedEmptyString}{SpecialChars.Text} replacealltext true 
                               word.gettext result {SpecialChars.Variable}result2
                               word.close");
            scripter.Run();
            string trimmedValue = scripter.Variables.GetVariableValue<string>("result1").Trim();
            Assert.AreEqual(expected, trimmedValue);
            trimmedValue = scripter.Variables.GetVariableValue<string>("result2").Trim();
            Assert.AreEqual(expectedEmptyString, trimmedValue);
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
