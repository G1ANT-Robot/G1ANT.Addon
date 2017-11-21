
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IEGetAttributeTests
    {
        private Scripter scripter;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEGetAttributeSuccessTest()
        {

            scripter = new Scripter();
            scripter.Text = $@"
                            ie.open timeout 20000
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.getattribute name {SpecialChars.Text}title{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by id result title
                            ";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>("title", string.Empty, true)?.ToLower();
            Assert.IsTrue(title == "search" || title == "szukaj");
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEGetAttributeWrongNameFailTest()
        {
            scripter = new Scripter();
            scripter.Text = $@"
                            ie.open timeout 20000
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.getattribute name {SpecialChars.Text}aabbcc{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by id result title
                            ";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>("title", string.Empty, true)?.ToLower();
            Assert.AreEqual(title, string.Empty);
        }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
