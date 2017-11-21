using NUnit.Framework;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests
{
    [TestFixture]
    public class LoadAbbyTests
    {
        [Test]
        public void LoadAbbyAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.AbbyyFineReader.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Structures.Count > 0);
            Assert.IsTrue(addon.Commands.Count > 0);
            Assert.IsTrue(addon.Triggers.Count > 0);
        }
    }
}
