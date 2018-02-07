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
    public class IEWaitForCompleteTests
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
        public void IEWaitForCompleteSuccessGoogleTests()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl google.pl nowait true
                            ie.waitforcomplete timeout 20000
                            ie.gettitle result {SpecialChars.Variable}title
                            ";
            scripter.Run();
            var title = scripter.Variables.GetVariableValue<string>("title").ToLower();
            Assert.IsTrue(title.Contains("google"));
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEWaitForCompleteSuccessNavigateTests()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl tibia.com nowait true
                            ie.waitforcomplete timeout 20000
                            ie.gettitle result {SpecialChars.Variable}title
                            ";
            scripter.Run();
            var title = scripter.Variables.GetVariableValue<string>("title").ToLower();
            Assert.IsTrue(title.Contains("tibia"));
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEWaitForCompleteSuccessYahooTests()
        {
            scripter.Text = $@"
                            ie.open
                            ie.seturl yahoo.com nowait true
                            ie.waitforcomplete timeout 20000
                            ie.gettitle result {SpecialChars.Variable}title
                            ";
            scripter.Run();
            var title = scripter.Variables.GetVariableValue<string>("title").ToLower();
            Assert.IsTrue(title.Contains("yahoo"));
        }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
