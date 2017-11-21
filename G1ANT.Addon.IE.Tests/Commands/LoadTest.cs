using System;
using NUnit.Framework;

namespace G1ANT.Language.IE.Tests.Commands
{

    [TestFixture]
    public class LoadTest
    {
        [Test]
        public void LoadAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.IExplorer.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
