using System;
using System.Diagnostics;
using System.Threading;

using G1ANT.Engine;
using G1ANT.Language.Ui.Commands;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ui.Exceptions;
using G1ANT.Language.Semantic;
using NUnit.Framework;

namespace G1ANT.Language.Ui.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(UiWaitFor))]
    public class UiWaitForTests
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
        }
        [Test]
        [Timeout(20000)]
        public void UiWaitForTest()
        {
            int controlShownTick = -1;
            int processStartTick = -1;
            int tabSwitchDelay = 5000;
            int waitForFireWindow = 2000;

            testerApp = uiTests.StartFormTester($"Title {title1}");
            processStartTick = Environment.TickCount;
            while (testerApp.MainWindowHandle == null)
            {
                Thread.Sleep(20);
            }
            while (UiFramework.WindowsApi.WinApi.IsWindowVisible(testerApp.MainWindowHandle) == false)
            {
                Thread.Sleep(20);
            }

            Exception waitThreadException = null;
            Thread waitThread = new Thread(() =>
            {
                try
                {
                    scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
                    scripter.RunLine($"ui.waitfor wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\Edit[Id=\"textBox1\" Type=\"50004\"]{SpecialChars.Text}");
                    controlShownTick = Environment.TickCount;
                }
                catch (Exception ex)
                {
                    waitThreadException = new ThreadInterruptedException("Exception in wait thread", ex);
                }
            });
            waitThread.Start();

            Exception changeTabException = null;
            Thread changeTabThread = new Thread(() =>
            {
                try
                {
                    Scripter scr = new Scripter();
                    Thread.Sleep(tabSwitchDelay);
                    scr.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
                    scr.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]{SpecialChars.Text}");
                }
                catch (Exception ex)
                {
                    changeTabException = new ThreadInterruptedException("Exception in change tab thread", ex);
                }

            });
            changeTabThread.Start();

            changeTabThread.Join();
            waitThread.Join();

            if (changeTabException != null)
                throw changeTabException;
            if (waitThreadException != null)
                throw waitThreadException;

            Assert.IsTrue(controlShownTick > processStartTick + tabSwitchDelay, $"To fast, wait exited after {controlShownTick - processStartTick}");
            Assert.IsTrue(controlShownTick < processStartTick + tabSwitchDelay + waitForFireWindow, "To slow, wait exited after {controlShownTick-processStartTick}");
        }

        [Test]
        [Timeout(20000)]
        public void UiWaitForErrorTest()
        {
            testerApp = uiTests.StartFormTester($"Title {title1}");
            while (testerApp.MainWindowHandle == null)
            {
                Thread.Sleep(20);
            }
            while (UiFramework.WindowsApi.WinApi.IsWindowVisible(testerApp.MainWindowHandle) == false)
            {
                Thread.Sleep(20);
            }
            scripter.Text = ($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.Text = ($"ui.waitfor wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\Edit[Id=\"textBox1\" Type=\"50004\"]{SpecialChars.Text} timeout 3000 ");


            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<NullReferenceException>(exception.GetBaseException());
           
        }
        [TearDown]
        public void TestCleanUp()
        {
            if (!testerApp.HasExited)
                testerApp.Kill();
        }
    }
}
