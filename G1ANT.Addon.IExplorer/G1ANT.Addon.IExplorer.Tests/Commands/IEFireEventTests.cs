/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.IExplorer
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
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
        [SetUp]
        public void TestInitialize()
        {
            scripter = new Scripter();
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.IExplorer.dll");
        }
        [Test, Timeout(IETests.TestTimeout)]
        public void IEFireEventTestsSuccessTest()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.fireevent eventname {SpecialChars.Text}onclick{SpecialChars.Text} search {SpecialChars.Text}a.gb_P[href='https://mail.google.com/mail/?tab=wm']{SpecialChars.Text} by query timeout 15000
                            ie.gettitle result {SpecialChars.Variable}title
                            ie.close";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>("title", string.Empty, true)?.ToLower();
            Assert.IsTrue(title.Contains("gmail"));
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEFireEventTestsWrongSelectorTest()
        {
            scripter.Text = $@"
                            ie.open timeout 20000
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.fireevent eventname {SpecialChars.Text}onclick{SpecialChars.Text} search {SpecialChars.Text}abc cba dec{SpecialChars.Text} by jquery timeout 15000
                            ie.gettitle result {SpecialChars.Variable}title
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
                scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.fireevent eventname {SpecialChars.Text}onclick{SpecialChars.Text} search {SpecialChars.Text}asd ga gas{SpecialChars.Text} by abc nowait true
                            ie.gettitle result {SpecialChars.Variable}title
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
