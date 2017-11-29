using System;
using System.Diagnostics;
using System.Threading;

using G1ANT.Engine;
using G1ANT.Language.Ui.Commands;
using G1ANT.Language.Core.Tests;
using G1ANT.Interop;
using G1ANT.Language.Ui.Exceptions;
using NUnit.Framework;
using G1ANT.Language.Semantic;

namespace G1ANT.Language.Ui.Tests
{
    [TestFixture]
    [TestsClass(typeof(UiAttach))]
    public class UiAttachTests
    {
        static string testAppPath;
        static Process testerApp;
        Scripter scripter;
        const string title1 = "TestApp";

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
            
        testerApp = uiTests.StartFormTester($"Title {title1}");
        }

        [Test]
        [Timeout(20000)]
        public void UiAttachTest()
        {
            Thread.Sleep(500);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            Thread.Sleep(100);
            Assert.AreEqual(RobotWin32.GetWindowText(new IntPtr(G1ANT.Language.Ui.Api.UiManager.CurrentUi.Element.Current.NativeWindowHandle)), title1);
        }

        [Test]
        [Timeout(20000)]
        public void UiAttachNotFoundTest()
        {
            Thread.Sleep(500);
            scripter.Text = $"ui.attach windowname {SpecialChars.Text}ImnotAnExistingWindow{SpecialChars.Text}";
            Thread.Sleep(100);

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<WindowNotFoundException>(exception.GetBaseException());

          
        }

        [TearDown]
        [Timeout(20000)]
        public void TestCleanUp()
        {
            if (!testerApp.HasExited)
                testerApp.Kill();
        }
    }
}
