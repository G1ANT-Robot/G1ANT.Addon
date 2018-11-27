﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.IExplorer
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using NUnit.Framework;

namespace G1ANT.Addon.IExplorer.Tests
{

    [TestFixture]
    public class LoadTest
    {
        [Test]
        public void LoadIExplorerAddon()
        {
            Language.Addon addon = Language.AddonLoader.Load(@"G1ANT.Addon.IExplorer.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
