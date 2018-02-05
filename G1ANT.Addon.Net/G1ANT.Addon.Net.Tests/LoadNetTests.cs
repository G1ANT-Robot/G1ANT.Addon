using NUnit.Framework;

namespace G1ANT.Addon.Net.Tests
{
    [TestFixture]
    public class LoadNetTests
    {
        [Test]
        public void LoadNetAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Net.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
