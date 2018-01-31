using NUnit.Framework;

namespace G1ANT.Addon.Ocr.Tests
{
    [TestFixture]
    public class LoadOcrTests
    {
        [Test]
        public void LoadAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
