using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using G1ANT.Language.Core.Tests;
using G1ANT.UiFramework;

namespace G1ANT.UiFrameworkTests
{
    [TestClass]
    [TestsClass(typeof(G1ANT.UiFramework.AutomatedItem))]
    public class AutomatedItemTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadConnentionStringFailTest()
        {
            AutomatedItem TestItem = new AutomatedItem();
            Dynamic.CallPrivate(TestItem,"ReadConnentionString");
        }
        [TestMethod]
        public void ReadParentFailTest()
        {
            AutomatedItem TestItem = new AutomatedItem();
            var a=  Dynamic.CallPrivate(TestItem, "ReadParent");
            Assert.IsTrue(a == null);
        }
    }
}
