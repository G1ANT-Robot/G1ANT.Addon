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
    public class IEWaitForValueTests
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
        public void WaitForValueSuccessTest()
        {
            scripter.Text = $@"
                            ie.open url {SpecialChars.Text}https://secure.tibia.com/community/?subtopic=characters{SpecialChars.Text}
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""name""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.close";
            scripter.Run();
        }


        [Test, Timeout(IETests.TestTimeout)]
        public void WaitForValueBadScriptTest()
        {
            scripter.Text = $@"
                            ie.open url {SpecialChars.Text}google.pl{SpecialChars.Text} timeout 20000
                            ie.setattribute name {SpecialChars.Text}value{SpecialChars.Text} value {SpecialChars.Text}abc{SpecialChars.Text} search {SpecialChars.Text}input[name='q']{SpecialChars.Text} by {SpecialChars.Text}query{SpecialChars.Text}
                            keyboard {SpecialChars.KeyBegin}ENTER{SpecialChars.KeyEnd}
                            ie.waitforvalue script {SpecialChars.Text}gasg gsag gsagas{SpecialChars.Text} expectedvalue true
                            ie.close";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<TimeoutException>(exception.GetBaseException());
        }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
