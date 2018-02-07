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

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IEClickTests
    {
        private Scripter scripter;
        private string pageAddress = @"www.google.pl";

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
        public void IEClickTestsSuccessTest()
        {
            string titleVariable = "title";
            string titleShouldContain = "gmail";
            scripter.Text = $@"
                            ie.open {pageAddress}
                            ie.click search {SpecialChars.Text}a.gb_P[href='https://mail.google.com/mail/?tab=wm']{SpecialChars.Text} by query timeout 30000
                            ie.gettitle result {SpecialChars.Variable}{titleVariable}";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>(titleVariable, string.Empty, true)?.ToLower();
            Assert.IsTrue(title.Contains(titleShouldContain));
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEClickTestsWrongSelectorTest()
        {
            scripter.Text = $@"
                            ie.open {pageAddress} timeout 20000
                            ie.click search {SpecialChars.Text}asd ga gas{SpecialChars.Text} by query nowait true
                            ie.gettitle result {SpecialChars.Variable}title";

            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<TimeoutException>(exception.GetBaseException());
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEClickTestsWrongByArgumentTest()
        {
            scripter.Text = $@"
                            ie.open url {pageAddress} timeout 20000
                            ie.click search {SpecialChars.Text}asd ga gas{SpecialChars.Text} by abc nowait true";
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
