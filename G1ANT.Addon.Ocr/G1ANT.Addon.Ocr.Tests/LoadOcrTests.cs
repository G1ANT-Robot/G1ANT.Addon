/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Ocr
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using NUnit.Framework;

namespace G1ANT.Addon.Ocr.Tests
{
    [TestFixture]
    public class LoadOcrTests
    {
        [Test]
        public void LoadAddon()
        {
            Language.Addon addon = Language.AddonLoader.Load(@"G1ANT.Addon.Ocr.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
