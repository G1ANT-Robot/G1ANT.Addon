using System;
using NUnit.Framework;

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    public class LoadTest
    {
        [Test]
        public void LoadIExplorerAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.IExplorer.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
