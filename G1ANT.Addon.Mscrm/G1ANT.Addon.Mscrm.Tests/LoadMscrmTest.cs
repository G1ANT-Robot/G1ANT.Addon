﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Mscrm
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using NUnit.Framework;

namespace G1ANT.Addon.Mscrm.Tests
{
    [TestFixture]
    public class LoadMscrmTests
    {
        [Test]
        public void LoadMscrmAddon()
        {
            Language.Addon addon = Language.Addon.Load("G1ANT.Addon.Mscrm.dll");
            Assert.IsNotNull(addon);
            Assert.IsTrue(addon.Commands.Count > 0);
        }
    }
}
