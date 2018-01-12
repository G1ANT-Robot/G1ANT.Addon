using System;

using G1ANT.Engine;
using G1ANT.Language.Semantic;

using NUnit.Framework;
using System.Threading;
using G1ANT.Language;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests
{
    [TestFixture]
    public class ProcessScreenTests
    {
        private static Scripter scripter;
        private static System.Diagnostics.Process testerApp;
        private static string appTitle = "TestApp";
        private const int TesterAppTimeout = 1000;

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.AbbyyFineReader.dll");
            scripter = new Scripter();
scripter.InitVariables.Clear();
            testerApp = AbbyTests.StartFormTester($"Title {appTitle}");
        }
        
        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void ProcessScreenTest()
        {
            IntPtr hTesterAppWindow = testerApp.MainWindowHandle;
            RobotWin32.Rect windowRect = new RobotWin32.Rect();
            RobotWin32.GetWindowRectangle(hTesterAppWindow, ref windowRect);
            int titleBarHeight = 24;
            scripter.RunLine($"ocrabbyy.processscreen area {SpecialChars.Text}{windowRect.Left},{windowRect.Top},{windowRect.Right},{windowRect.Top + titleBarHeight}{SpecialChars.Text}");
            int documentId = scripter.Variables.GetVariableValue<int>("result");

            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            Assert.IsNotNull(document);
            string plainText = document.GetAllText();

            Assert.IsTrue(plainText.Contains(appTitle));
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void RelativeTest()
        {
            IntPtr hTesterAppWindow = testerApp.MainWindowHandle;
            RobotWin32.Rect windowRect = new RobotWin32.Rect();
            RobotWin32.GetWindowRectangle(hTesterAppWindow, ref windowRect);
            int titleBarHeight = 24;
            scripter.RunLine($"ocrabbyy.processscreen area {SpecialChars.Text}0,0,{windowRect.Right - windowRect.Left},{titleBarHeight}{SpecialChars.Text} relative true");
            int documentId = scripter.Variables.GetVariableValue<int>("result");

            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            Assert.IsNotNull(document);
            string plainText = document.GetAllText();

            Assert.IsTrue(plainText.Contains(appTitle));
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void LanguageTest()
        {
            string appTitle2 = "шестьсот";  // in case someone worry what this mean, it's six hundred
            scripter.RunLine($"keyboard {SpecialChars.Text}title {appTitle2}{SpecialChars.Text}{SpecialChars.KeyBegin}enter{SpecialChars.KeyEnd}");
            IntPtr hTesterAppWindow = testerApp.MainWindowHandle;
            RobotWin32.Rect windowRect = new RobotWin32.Rect();
            RobotWin32.GetWindowRectangle(hTesterAppWindow, ref windowRect);
            int titleBarHeight = 24;

            scripter.RunLine($"ocrabbyy.processscreen area {SpecialChars.Text}{windowRect.Left},{windowRect.Top},{windowRect.Right},{windowRect.Top + titleBarHeight}{SpecialChars.Text} language russian");
            int documentId = scripter.Variables.GetVariableValue<int>("result");

            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            Assert.IsNotNull(document);
            string plainText = document.GetAllText();

            Assert.IsTrue(plainText.Contains(appTitle2));
        }


        [TearDown]
        public void ClassCleanup()
        {
            if ((testerApp?.HasExited ?? true) == false)
            {
                testerApp.Kill();
            }
        }
    }
}