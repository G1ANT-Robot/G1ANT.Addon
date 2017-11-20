using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Threading;
using WatiN.Core.Exceptions;

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IEFireEventTests
    {
        private Scripter scripter;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEFireEventTestsSuccessTest()
        {
            scripter = new Scripter();
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.fireevent eventname {SpecialChars.Text}onclick{SpecialChars.Text} search {SpecialChars.Text}a.gb_P[href='https://mail.google.com/mail/?tab=wm']{SpecialChars.Text} by query timeout 15000
                            ie.gettitle result title
                            ie.close";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>("title", string.Empty, true)?.ToLower();
            Assert.IsTrue(title.Contains("gmail"));
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEFireEventTestsWrongSelectorTest()
        {
            scripter = new Scripter();
            scripter.Text = $@"
                            ie.open timeout 20000
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.fireevent eventname {SpecialChars.Text}onclick{SpecialChars.Text} search {SpecialChars.Text}abc cba dec{SpecialChars.Text} by jquery timeout 15000
                            ie.gettitle result title
                            ie.close";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<JavaScriptException>(exception.GetBaseException());
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEFireEventTestsWrongByArgumentTest()
        {
                scripter = new Scripter();
                scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.fireevent eventname {SpecialChars.Text}onclick{SpecialChars.Text} search {SpecialChars.Text}asd ga gas{SpecialChars.Text} by abc nowait true
                            ie.gettitle result title
                            ie.close";
                Exception exception = Assert.Throws<ApplicationException>(delegate
                {
                    scripter.Run();
                });
                Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
            }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
