using G1ANT.Addon.Xls.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Reflection;

namespace G1ANT.Addon.Xls.Tests
{
    [TestFixture]
    public class XlsGetValueTests
    {
        static Scripter scripter;
        string file;

        [OneTimeSetUp]
        [Timeout(20000)]
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            scripter = new Scripter();
scripter.InitVariables.Clear();
           scripter.InitVariables.Add("xlsPath", new TextStructure(file));
        }
        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Xls.dll");
        }
        [Test]
        [Timeout(20000)]
        public void XlsGetValueIntTest()
        {
            scripter.RunLine($"xls.open  {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id");
            scripter.RunLine($"xls.getvalue {SpecialChars.Text}A1{SpecialChars.Text} result {SpecialChars.Variable}testint");
            Assert.AreEqual("1234", scripter.Variables.GetVariableValue<string>("testint"));
        }

        [Test]
        [Timeout(20000)]
        public void XlsGetValueStringTest()
        {
            scripter.RunLine($"xls.open  {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id");
            scripter.RunLine($"xls.getvalue {SpecialChars.Text}B1{SpecialChars.Text} result {SpecialChars.Variable}teststring");
            Assert.AreEqual("abcd", scripter.Variables.GetVariableValue<string>("teststring"));
        }

        [Test]
        [Timeout(20000)]
        public void GetFloatValue()
        {
            scripter.RunLine($"xls.open  {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id");
            scripter.RunLine($"xls.getvalue {SpecialChars.Text}G2{SpecialChars.Text} result {SpecialChars.Variable}testfloat");
            Assert.AreEqual(12.345f, float.Parse(scripter.Variables.GetVariableValue<string>("testfloat")));
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