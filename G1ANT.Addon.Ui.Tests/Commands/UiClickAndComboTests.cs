using System;
using System.Diagnostics;
using System.Threading;

using G1ANT.Engine;
using G1ANT.Language.Ui.Commands;
using G1ANT.Language.Ui.Exceptions;
using G1ANT.Language.Core.Tests;
using NUnit.Framework;
using G1ANT.Language.Semantic;

namespace G1ANT.Language.Ui.Tests
{
    [TestFixture]
    [TestsClass(typeof(UiClick))]
    public class UiClickTests
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
        public void UiClickTest()
        {
            Thread.Sleep(200);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}TitleBar[Type=\"50037\" Name=\"{ title1}\" Id=\"TitleBar\"]\\Button[Type=\"50000\" Name=\"Zamknij\" Id=\"Close\"]{SpecialChars.Text}");

            Thread.Sleep(500);
            Assert.AreEqual(testerApp.HasExited, true);
        }
        [Test]
        [Timeout(20000)]
        public void UiClickFailTest()
        {
            Thread.Sleep(200);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.Text = ($"ui.click wpath {SpecialChars.Text}TitleBar[Type=\"50037\" Name=\"{title1}\" Id=\"TitleBar\"]\\Button[Type=\"50000\" Name=\"Zamknij\" Id=\"Closet\"]{SpecialChars.Text}");
            Thread.Sleep(500);
            Assert.AreEqual(testerApp.HasExited, false);

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<NullReferenceException>(exception.GetBaseException());
        }

        [Test]
        [Timeout(20000)]
        public void UiClickTabCheckBoxAndStateTest()
        {
            Thread.Sleep(200);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\CheckBox[Name=\"checkBox1\" Id=\"checkBox1\" Type=\"50002\"]{SpecialChars.Text}");
            scripter.RunLine($"ui.state wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\CheckBox[Name=\"checkBox1\" Id=\"checkBox1\" Type=\"50002\"]{SpecialChars.Text} result state");
            Thread.Sleep(500);
            Assert.AreEqual(true, ((Structures.Bool)scripter.Variables.GetVariable("state").Value).Value);
        }
        [Test]
        [Timeout(20000)]
        public void UiClickTabListGetEditValTest()
        {
            Thread.Sleep(200);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\ComboBox[Id=\"comboBox1\" Type=\"50003\"]\\List[Id=\"ListBox\" Type=\"50008\"]\\ListItem[Name=\"Lol2\" Type=\"50007\"]{SpecialChars.Text}");
            Thread.Sleep(200);
            scripter.RunLine($"ui.getvalue wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\ComboBox[Name=\"Lol2\" Id=\"comboBox1\" Type=\"50003\"]\\Edit[Name=\"Lol2\" Id=\"1001\" Type=\"50004\"]{SpecialChars.Text} result combo");
            Assert.AreEqual("Lol2", ((Structures.String)scripter.Variables.GetVariable("combo").Value).Value);
        }
        [Test]
        [Timeout(20000)]
        public void UiClickTabRadioTest()
        {
            Thread.Sleep(200);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\RadioButton[Name=\"radioButton2\" Id=\"radioButton2\" Type=\"50013\"]{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\RadioButton[Name=\"radioButton1\" Id=\"radioButton1\" Type=\"50013\"]{SpecialChars.Text}");
            Assert.AreEqual(true, ((Structures.Bool)scripter.Variables.GetVariable("result").Value).Value); //cannot check state for radio button
        }
        [Test]
        [Timeout(20000)]
        public void UiSetGetSetValueTest()
        {
            Thread.Sleep(200);
            scripter.RunLine($"ui.attach windowname {SpecialChars.Text}{title1}{SpecialChars.Text}");
            scripter.RunLine($"ui.click wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]{SpecialChars.Text}");
            scripter.RunLine($"ui.setvalue wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\Edit[Id=\"textBox1\" Type=\"50004\"]{SpecialChars.Text} value {SpecialChars.Text}G1ANT{SpecialChars.Text}");
            scripter.RunLine($"ui.getvalue wpath {SpecialChars.Text}Tab[Id=\"tab111\" Type=\"50018\"]\\TabItem[Name=\"DropDownTab\" Type=\"50019\"]\\Edit[Name=\"G1ANT\" Id=\"textBox1\" Type=\"50004\"]{SpecialChars.Text} result lolo");

            Assert.AreEqual("G1ANT", ((Structures.String)scripter.Variables.GetVariable("lolo").Value).Value);
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