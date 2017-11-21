using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.IO;

namespace G1ANT.Language.Selenium.Tests
{
    [TestFixture]

    public class SeleniumTypeTests
    {
        private Scripter scripter;
        private string pageAddress = "google.pl";

        [SetUp]
        public void TestInitialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(SeleniumTests.TestTimeout)]
        public void BrowsersGetAttributesSuccessTest()
        {
            scripter = new Scripter();
            scripter.Text = $@"
                    selenium.open type {SpecialChars.Text}firefox{SpecialChars.Text} url {SpecialChars.Text}{pageAddress}{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('#lst-ib').length > 0{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text} timeout 20000
                    selenium.type text {SpecialChars.Text}abc{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.presskey key {SpecialChars.Text}enter{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('.rc').length > 0;{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text}
                    selenium.gettitle
                    selenium.close
                ";
            scripter.Run();
            Assert.IsTrue(scripter.Variables.GetVariableValue<string>("result")?.Contains("abc") ?? false);

            scripter = new Scripter();
            scripter.Text = $@"
                    selenium.open type {SpecialChars.Text}edge{SpecialChars.Text} url {SpecialChars.Text}{pageAddress}{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('#lst-ib').length > 0{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text} timeout 20000
                    selenium.type text {SpecialChars.Text}abc{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.presskey key {SpecialChars.Text}enter{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('.rc').length > 0;{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text}
                    selenium.gettitle
                    selenium.close
                ";
            scripter.Run();
            Assert.IsTrue(scripter.Variables.GetVariableValue<string>("result")?.Contains("abc") ?? false);

            scripter = new Scripter();
            scripter.Text = $@"
                    selenium.open type {SpecialChars.Text}chrome{SpecialChars.Text} url {SpecialChars.Text}{pageAddress}{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('#lst-ib').length > 0{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text} timeout 20000
                    selenium.type text {SpecialChars.Text}abc{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.presskey key {SpecialChars.Text}enter{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('.rc').length > 0;{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text}
                    selenium.gettitle
                    selenium.close
                ";
            scripter.Run();
            Assert.IsTrue(scripter.Variables.GetVariableValue<string>("result")?.Contains("abc") ?? false);

            scripter = new Scripter();
            scripter.Text = $@"
                    selenium.open type {SpecialChars.Text}ie{SpecialChars.Text} url {SpecialChars.Text}{pageAddress}{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('#lst-ib').length > 0{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text} timeout 20000
                    selenium.type text {SpecialChars.Text}abc{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.presskey key {SpecialChars.Text}enter{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by {SpecialChars.Text}id{SpecialChars.Text}
                    selenium.waitforvalue script {SpecialChars.Text}return document.querySelectorAll('.rc').length > 0;{SpecialChars.Text} expectedvalue {SpecialChars.Text}true{SpecialChars.Text}
                    selenium.gettitle
                    selenium.close
                ";
            scripter.Run();
            Assert.IsTrue(scripter.Variables.GetVariableValue<string>("result")?.Contains("abc") ?? false);
        }

        [TearDown]
        public void CleanUp()
        {
            SeleniumTests.KillAllBrowserProcesses();
        }
    }
}
