using System;
using System.IO;

using G1ANT.Engine;

using NUnit.Framework;
using G1ANT.Language.Xls.Tests.Properties;
using System.Reflection;

namespace G1ANT.Language.Xls.Tests
{
    [TestFixture]
    public class SetValueTests
    {
        
        string file;
        string file2;
        private static Scripter scripter;

        [OneTimeSetUp]
        [Timeout(20000)]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file2 = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.EmptyWorkbook), "xlsx");
        }

        [SetUp]
        public void testinit()
        {
            scripter = new Scripter();
            scripter.RunLine($"xls.open {SpecialChars.Text}{file2}{SpecialChars.Text} accessmode read");
        }
        [TearDown]
        [Timeout(20000)]
        public static void ClassCleanUp()
        {
            try
            {
                scripter.RunLine("xls.close");
            }
            catch { }
        }

        [Test]
        [Timeout(20000)]
        public void SetString()
        {
            string textValue = "inserted text";
            string position = "A1";
            scripter.RunLine($"xls.setvalue value {SpecialChars.Text}{textValue}{SpecialChars.Text} position {position}");
            scripter.RunLine($"xls.getvalue {position}");
            Assert.AreEqual(textValue, scripter.Variables.GetVariableValue<string>("result"));
        }

        [Test]
        [Timeout(20000)]
        public void SetInt()
        {
            int numericalValue = 12345;
            string position = "B1";
            scripter.RunLine($"xls.setvalue {SpecialChars.Text}{numericalValue}{SpecialChars.Text} position {position}");
            scripter.RunLine($"xls.getvalue {position}");
            Assert.AreEqual(numericalValue, int.Parse(scripter.Variables.GetVariableValue<string>("result")));
        }
    }
}
