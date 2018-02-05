using System;
using NUnit.Framework;

namespace G1ANT.Addon.Mscrm.Tests
{
    [TestFixture]
    public class LoadMscrmTests
    {
        [Test]
        public void LoadMscrmAddon()
        {
            Language.Addon addon = Language.Addon.Load("G1ANT.Addon.Mscrm.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
