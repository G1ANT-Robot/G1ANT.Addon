using NUnit.Framework;

namespace G1ANT.Addon.Ocr.Tesseract.Tests
{
    [TestFixture]
    public class LoadTesseractTests
    {
        [Test]
        public void LoadTesseractAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.Tesseract.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
