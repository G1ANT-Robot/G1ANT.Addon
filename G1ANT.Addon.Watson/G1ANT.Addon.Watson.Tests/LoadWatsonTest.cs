using System;
using NUnit.Framework;

namespace G1ANT.Addon.Watson.Tests
{
    [TestFixture]
    public class WatsonLoadTests
    {
        [Test]
        public void LoadWatsonAddon()
        {
            Language.Addon addon = Language.Addon.Load("G1ANT.Addon.Watson.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
