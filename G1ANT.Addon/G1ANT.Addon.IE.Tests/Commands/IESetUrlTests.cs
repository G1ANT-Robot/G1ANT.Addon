
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
    public class IESetUrlTests
    {
        private Scripter scripter;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IESetUrlSuccessTests()
        {
            scripter = new Scripter();
            scripter.Text = $@"
                            ie.open
                            ie.seturl google.pl
                            ie.waitforvalue script {SpecialChars.Text}document.querySelectorAll('input[name=""q""]').length > 0{SpecialChars.Text} timeout 15000 expectedvalue true
                            ie.gettitle result title
                            ";
            scripter.Run();
            var title = scripter.Variables.GetVariableValue<string>("title").ToLower();
            Assert.IsTrue(title.Contains("google"));
        }
     
        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
