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
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.IExplorer.dll");
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
        //    scripter.Text = $@"
        //                    {SpecialChars.Variable}result = -1
        //                    ie.attach by url phrase google result result
        //                    ie.close";
        //    IETests.WaitForIeOpen(10000, newIeInstanceTitleKeywords);
        //    scripter.Run();
        //    Assert.AreEqual(IETests.GetIeInstancesCount(), 0, "there were ie instances opened left");
        //    int result = scripter.Variables.GetVariableValue<int>("result", -1, true);
        //    Assert.AreNotEqual(result, -1, $"wrong result value: {result}");

            scripter.Text = $@"
                            {SpecialChars.Variable}result2 = -1
                            ie.attach by url phrase google result {SpecialChars.Variable}result2
                            ie.close";
            IETests.WaitForIeOpen(10000, newIeInstanceTitleKeywords);
            scripter.Run();
            Assert.AreEqual(IETests.GetIeInstancesCount(), 0, "there were ie instances opened left");
            int result2 = scripter.Variables.GetVariableValue<int>("result2", -1, true);
        Assert.AreNotEqual(result2, -1, $"wrong result value: {result2}");
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEAttachFailureTest()
        {
            //    scripter.Text = $@"
            //                    ie.attach by hashaas phrase google
            //                    ie.close";
            //    Exception exception = Assert.Throws<ApplicationException>(delegate
            //    {
            //        scripter.Run();
            //    });
            //    Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());

        //    scripter.Text = $@"
        //                    ie.attach by hashaas phrase google result result2
        //                    ie.close";
        //    Exception exception = Assert.Throws<ApplicationException>(delegate
        //    {
        //        scripter.Run();
        //    });
        //Assert.IsInstanceOf<ArgumentException>(exception.GetBaseException());
        }

        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
