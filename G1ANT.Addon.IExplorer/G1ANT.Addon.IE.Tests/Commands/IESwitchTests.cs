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
    public class IESwitchTests
    {
        private Scripter scripter;

        [OneTimeSetUp]
        public void Initialize()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.IExplorer.dll");
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        [SetUp]
        public void TestInitialize()
        {
            
            scripter = new Scripter();
            
        }
        [Test, Timeout(IETests.TestTimeout)]
        public void IESwitchSuccessTest()
        {
            scripter.Text = $@"
                            ie.open result {SpecialChars.Variable}result1
                            ie.open result {SpecialChars.Variable}result2
                            ie.open result {SpecialChars.Variable}result3
                            ie.open result {SpecialChars.Variable}result4
                            ie.switch {SpecialChars.Variable}result2
                            ie.switch {SpecialChars.Variable}result3
                            ie.switch {SpecialChars.Variable}result4
                            ie.switch {SpecialChars.Variable}result1
                            ie.close
                            ie.switch {SpecialChars.Variable}result2
                            ie.close
                            ie.switch {SpecialChars.Variable}result3
                            ie.close
                            ie.switch {SpecialChars.Variable}result4
                            ie.close";
            scripter.Run();
            Assert.IsTrue(IETests.GetIeInstancesCount() == 0);
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IESwitchFailureTest()
        {
            scripter.Text = $@"
                            ie.open result {SpecialChars.Variable}result1
                            ie.open result {SpecialChars.Variable}result2
                            ie.open result {SpecialChars.Variable}result3                            
                            ie.switch {SpecialChars.Variable}result1
                            ie.close
                            ie.switch {SpecialChars.Variable}result2
                            ie.close
                            ie.switch {SpecialChars.Variable}result3
                            ie.switch {SpecialChars.Variable}result1
                            ie.close";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsInstanceOf<InvalidOperationException>(exception.GetBaseException());
            Assert.IsTrue(IETests.GetIeInstancesCount() != 0);
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IECloseFailIeNotOpenedTest()
        {
                scripter.Text = $@"
                            ie.open result {SpecialChars.Variable}result1
                            ie.open result {SpecialChars.Variable}result2
                            ie.open result {SpecialChars.Variable}result3
                            ie.switch {SpecialChars.Variable}result1
                            ie.close
                            ie.switch {SpecialChars.Variable}result2
                            ie.close
                            ie.switch {SpecialChars.Variable}result3
                            ie.close
                            ie.switch {SpecialChars.Variable}result3
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
