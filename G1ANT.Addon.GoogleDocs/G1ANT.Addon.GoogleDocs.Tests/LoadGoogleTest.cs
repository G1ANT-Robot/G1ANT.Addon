/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.GoogleDocs
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
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
