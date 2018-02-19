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
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System;
using System.Threading;
using WatiN.Core.Exceptions;

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IERunScriptTests
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
        public void IERunScriptSuccessTest()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.runscript script {SpecialChars.Text}2 + 5{SpecialChars.Text}
                            ";
            scripter.Run();
            string result = scripter.Variables.GetVariableValue<string>("result", string.Empty, true)?.ToLower();
            Assert.AreEqual(result, 7.ToString());
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IERunScriptCreateDomElementSuccessTest()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.runscript script {SpecialChars.Text}el = document.createElement('div');el.id = 'hehe';document.body.appendChild(el);{SpecialChars.Text}
                            ie.getattribute name {SpecialChars.Text}id{SpecialChars.Text} search {SpecialChars.Text}hehe{SpecialChars.Text} by id timeout 2000
                            ";
            scripter.Run();
            string result = scripter.Variables.GetVariableValue<string>("result", string.Empty, true)?.ToLower();
            Assert.AreEqual(result, "hehe");
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IERunScriptWrongScriptFailTest()
        {
            scripter.Text = $@"
                            ie.open timeout 20000
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.runscript script {SpecialChars.Text}ajsdhka ajsd a02-{SpecialChars.Text}
                            ie.getattribute name {SpecialChars.Text}id{SpecialChars.Text} search {SpecialChars.Text}hehe{SpecialChars.Text} by id timeout 2000
                            ";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<JavaScriptException>(exception.GetBaseException());
        }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
