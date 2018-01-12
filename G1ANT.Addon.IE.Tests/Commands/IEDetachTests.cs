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
    public class IEDetachTests
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
scripter.InitVariables.Clear();
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.IExplorer.dll");
        }
        [Test, Timeout(IETests.TestTimeout)]
        public void IEDetachSuccessTest()
        {
            scripter.Text = $@"
                            ie.open result {SpecialChars.Variable}result1
                            ie.open result {SpecialChars.Variable}result2
                            ie.switch {SpecialChars.Variable}result1
                            ie.detach
                            ie.switch {SpecialChars.Variable}result2
                            ie.detach";

            scripter.Run();
            Assert.IsTrue(IETests.GetIeInstancesCount() != 0);
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEDetachFailureTest()
        {
            scripter.Text = $@"
                            ie.open result {SpecialChars.Variable}result1
                            ie.open result {SpecialChars.Variable}result2
                            ie.switch {SpecialChars.Variable}result1
                            ie.detach
                            ie.close
                            ie.switch {SpecialChars.Variable}result2
                            ie.detach
                            ie.close
                            ie.detach";
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
