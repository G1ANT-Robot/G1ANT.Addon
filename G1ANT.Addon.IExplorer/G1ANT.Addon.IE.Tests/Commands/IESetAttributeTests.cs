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

namespace G1ANT.Addon.IExplorer.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IESetAttributeTests
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
        public void IESetAttributeSuccessTest()
        {
            scripter.Text = $@"
                            ie.open timeout 20000
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.setattribute name {SpecialChars.Text}attributeNameHeh{SpecialChars.Text} value {SpecialChars.Text}lol{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by id
                            ie.getattribute name {SpecialChars.Text}attributeNameHeh{SpecialChars.Text} search {SpecialChars.Text}lst-ib{SpecialChars.Text} by id result {SpecialChars.Variable}title
                            ";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>("title", string.Empty, true)?.ToLower();
            Assert.AreEqual(title, "lol");
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IESetAttributeWrongElementFailTest()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.setattribute name {SpecialChars.Text}attributeNameHeh{SpecialChars.Text} value {SpecialChars.Text}lol{SpecialChars.Text} search {SpecialChars.Text}lst-ib2{SpecialChars.Text} by id timeout 2000
                            ";
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
