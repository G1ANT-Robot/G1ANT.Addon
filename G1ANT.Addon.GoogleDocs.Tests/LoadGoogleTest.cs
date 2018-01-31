using NUnit.Framework;

namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    public class LoadGoogleTests
    {
        [Test]
        public void LoadGoogleAddon()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.GoogleDocs.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
