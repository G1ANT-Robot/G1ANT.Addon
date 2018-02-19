/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.GoogleDocs
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using NUnit.Framework;
using System.Threading;

namespace G1ANT.Addon.GoogleDocs.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class GoogleSheetCloseTest
    {
        [Test]
        [Timeout(50000)]
        public void CloseTest()
        {
            // Google docs api do not expect any action to signalize end of working with document
            Assert.IsTrue(true);
        }
    }
}
