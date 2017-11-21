using NUnit.Framework;

namespace G1ANT.Addon.Xls.Tests
{
    [TestFixture]
    public class LoadXlsTests
    {
        [Test]
        public void LoadXlsAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Xls.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
