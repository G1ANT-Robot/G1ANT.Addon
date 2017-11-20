using G1ANT.Engine;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Linq;
using G1ANT.Language;

namespace G1ANT.Addon.IExplorer.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IEAttachTests
    {
        private Scripter scripter;
        private string internetExplorerPath = @"c:\Program Files\Internet Explorer\iexplore.exe";
        private string pageAddress = "www.google.pl";
        private string[] newIeInstanceTitleKeywords = new string[] { "internet", "explorer", "google" };

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void TestInitialize()
        {
            scripter = new Scripter();
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = internetExplorerPath,
                Arguments = pageAddress
            };
            var process = Process.Start(psi);
            process.WaitForInputIdle();
            WaitForIeGoogleProcess("google");
            scripter = new Scripter();
        }

        private void WaitForIeGoogleProcess(string titlePart)
        {
            while (!Process.GetProcessesByName("iexplore").ToList()
                .Where(x => x.MainWindowTitle?.ToLower()?.Contains(titlePart) ?? false).Any())
            {
                Thread.Sleep(100);
            }
            Thread.Sleep(2000);
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEAttachSuccessTest()
        {
            scripter.Text = $@"
                            {SpecialChars.Variable}result = -1
                            ie.attach by url phrase google
                            ie.close";
            IETests.WaitForIeOpen(10000, newIeInstanceTitleKeywords);
            scripter.Run();
            Assert.AreEqual(IETests.GetIeInstancesCount(), 0, "there were ie instances opened left");
            int result = scripter.Variables.GetVariableValue<int>("result", -1, true);
            Assert.AreNotEqual(result, -1, $"wrong result value: {result}");
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEAttachFailureTest()
        {
            scripter.Text = $@"
                            ie.attach by hashaas phrase google
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
