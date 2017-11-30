using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace G1ANT.Addon.MSOffice.Tests
{
    [TestFixture]
    class LoadMSOfficeTest
    {
        [Test]
        public void LoadMSOfficeAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Mscrm.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
