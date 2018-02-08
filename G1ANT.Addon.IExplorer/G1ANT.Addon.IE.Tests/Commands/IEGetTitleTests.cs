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
    public class IEGetTitleTests
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
        public void IEGetTitleSuccessTest()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('#lst-ib').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.gettitle result {SpecialChars.Variable}title
                            ";
            scripter.Run();
            string title = scripter.Variables.GetVariableValue<string>("title", string.Empty, true)?.ToLower();
            Assert.IsTrue(title.Contains("google"));
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEGetTitleFailTest()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl {SpecialChars.Text}google.pl{SpecialChars.Text}
                            ie.close
                            ie.gettitle result {SpecialChars.Variable}title
                            ";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<InvalidOperationException>(exception.GetBaseException());
        }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
