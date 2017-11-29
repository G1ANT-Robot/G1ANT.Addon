using System;
using System.Diagnostics;
using System.Threading;

using G1ANT.Engine;
using G1ANT.Language.Ui.Commands;
using G1ANT.Language.Core.Tests;
using NUnit.Framework;
using G1ANT.Language.Semantic;

namespace G1ANT.Language.Ui.Tests
{
    [TestFixture]
    [TestsClass(typeof(UiGetPosition))]
    public class UiGetPositionTests
    {
        string testAppPath;
        Process testerApp;
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
            testerApp = uiTests.StartFormTester($"Title {title1}");
            scripter = new Scripter();
        }

        [Test]
        [Timeout(20000)]
        public void UiGetPositionTest()
        {
            Thread.Sleep(500);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.RunLine($"ui.getposition  wpath {SpecialChars.Text}MenuBar[Name=\"menuStrip1\" Id=\"menuStrip\" Type=\"50010\"]\\MenuItem[Name=\"File\" Type=\"50011\"]{SpecialChars.Text} result position");
            Thread.Sleep(100);
            Structures.Point pos = new Structures.Point(new System.Drawing.Point(32,43));
            Structures.Point posi = (Structures.Point)scripter.Variables.GetVariable("position").Value.GetValue();
            Assert.AreEqual(pos.Value.X,posi.Value.X);
            Assert.AreEqual(pos.Value.Y, posi.Value.Y);
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
