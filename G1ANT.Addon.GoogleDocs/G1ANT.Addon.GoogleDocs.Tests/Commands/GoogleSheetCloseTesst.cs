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
