using System;
using G1ANT.Engine;


using NUnit.Framework;
using System.Reflection;
using G1ANT.Language;
using G1ANT.Addon.Xls.Tests.Properties;

namespace G1ANT.Addon.Xls.Tests
{
    [TestFixture]
    public class XlsSetValueTests
    {
        string file;
        static Scripter scripter;


        [SetUp]
        public void testinit()
        {
            scripter = new Scripter();
        }
        [Test]
        [Timeout(20000)]
        public void XlsSetValueInd()
        {
            scripter.RunLine($"xls.setvalue value {SpecialChars.Text}123{SpecialChars.Text} position {SpecialChars.Text}F3{SpecialChars.Text} result res");
            Assert.AreNotEqual(false, scripter.Variables.GetVariableValue<bool>("res"));
        }


        [Test]
        [Timeout(20000)]
        public void XlsSetValueString()
        {
            scripter.RunLine($"xls.setvalue value {SpecialChars.Text}test{SpecialChars.Text} position {SpecialChars.Text}F4{SpecialChars.Text} result res");
            Assert.AreNotEqual(false, scripter.Variables.GetVariableValue<bool>("res"));
        }

        [OneTimeSetUp]
        [Timeout(20000)]
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("xlsPath", new TextStructure(file));
            scripter.RunLine("xls.open ♥xlsPath result id");
        }

        [OneTimeTearDown]
        [Timeout(20000)]
        public static void ClassCleanUp()
        {
            scripter.RunLine($"xls.setvalue value {SpecialChars.Text}{SpecialChars.Text} position {SpecialChars.Text}F3{SpecialChars.Text}");
            scripter.RunLine($"xls.setvalue value {SpecialChars.Text}{SpecialChars.Text} position {SpecialChars.Text}F4{SpecialChars.Text}");
            scripter.RunLine($"xls.close");
        }
    }
}
