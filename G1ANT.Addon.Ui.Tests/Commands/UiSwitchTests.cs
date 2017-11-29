using System;
using System.Diagnostics;
using System.Threading;

using G1ANT.Engine;
using G1ANT.Language.Ui.Commands;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ui.Api;
using NUnit.Framework;
using G1ANT.Language.Semantic;

namespace G1ANT.Language.Ui.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(UiSwitchUi))]
    public class UiSwitchTests
    {
        static string testAppPath;
        static Process testerApp;
        static Process testerApp2;
        Scripter scripter;

        [OneTimeSetUp]
        [Timeout(20000)]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        [Timeout(20000)]
        public void TestInit()
        {
            scripter = new Scripter();
        }
        [Test]
        [Timeout(20000)]
        public void UiSwitchTest()
        {
            const string title1 = "TestApp";
            const string title2 = "TestApp2";
            testerApp = uiTests.StartFormTester($"Title {title1}");
            testerApp2 = uiTests.StartFormTester($"Title {title2}");
            Thread.Sleep(400);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text} result jeden");
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title2}{SpecialChars.Text} result dwa");
            scripter.RunLine($"ui.switchui id ♥jeden");
            Assert.AreEqual(title1, UiManager.CurrentUi.Element.Current.Name);
            scripter.RunLine($"ui.switchui id ♥dwa");
            Assert.AreEqual(title2, UiManager.CurrentUi.Element.Current.Name);
        }
        [TearDown]
        [Timeout(20000)]
        public void TestCleanUp()
        {
            if (!testerApp.HasExited)
                testerApp.Kill();

            if (!testerApp2.HasExited)
                testerApp2.Kill();
        }
    }
}
