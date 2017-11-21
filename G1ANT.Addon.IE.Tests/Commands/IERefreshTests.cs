﻿
using G1ANT.Engine;
using NUnit.Framework;
using System;
using System.Threading;

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class IERefreshTests
    {
        private Scripter scripter;

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(IETests.TestTimeout)]
        public void IEOpenTestSuccessTest()
        {
            scripter = new Scripter();
            scripter.Text = $@"
                            ie.open
                            ie.seturl google.pl
                            ie.refresh
                            ";
            scripter.Run();
        }
     
        [TearDown]
        public void CleanUp()
        {
            IETests.KillAllIeProcesses();
        }
    }
}
