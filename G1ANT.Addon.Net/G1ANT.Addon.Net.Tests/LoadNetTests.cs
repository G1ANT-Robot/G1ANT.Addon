/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
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
