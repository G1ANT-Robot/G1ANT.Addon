using System;
using System.IO;

using G1ANT.Engine;
using NUnit.Framework;
using G1ANT.Language.Xls.Tests.Properties;
using System.Reflection;

namespace G1ANT.Language.Xls.Tests
{
    [TestFixture]
    public class XlsSetSheetTests
    {
        string file;
        static Scripter scripter;

        
        [Test]
        [Timeout(20000)]
        public void XlsSetSheetDefault()
        {
            scripter.RunLine($"xls.setsheet result res");
            Assert.IsTrue(scripter.Variables.GetVariableValue<bool>("res"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsSetSheetCustom()
        {
            scripter.RunLine($"xls.setsheet {SpecialChars.Text}Arkusz2{SpecialChars.Text} result res");
            Assert.IsTrue(scripter.Variables.GetVariableValue<bool>("res"));
        }

        [Test]
        [Timeout(20000)]
        public void SetNotExistingSheet()
        {
            scripter.Text = "xls.setsheet a!@#$poq098239 result res";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.Run();
            });
            Assert.IsFalse(scripter.Variables.GetVariableValue<bool>("res"));
            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception.GetBaseException());
        }

        [OneTimeSetUp]
        [Timeout(20000)]
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("xlsPath", new TextStructure(file));
        }

        [SetUp]
        [Timeout(20000)]
        public void TestInit()
        {
            scripter.RunLine($"xls.open  {SpecialChars.Variable}xlsPath result id");
        }

        [TearDown]
        [Timeout(20000)]
        public void TestCleanUp()
        {
            try
            {
                scripter.RunLine("xls.close");
            }
            catch { }
        }
    }
}
