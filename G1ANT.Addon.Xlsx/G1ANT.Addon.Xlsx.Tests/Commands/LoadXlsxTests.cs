using NUnit.Framework;

namespace G1ANT.Addon.Xlsx.Tests
{
    [TestFixture]
    public class LoadXlsTests
    {
        [Test]
        public void LoadXlsxAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Xlsx.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
