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
        Scripter scripter;
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
        public void XlsGetValueDifferentTypesTest()
        {
            scripter.Text = $@"
            xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.getvalue row 1 colindex 1 result {SpecialChars.Variable}result1
            xls.getvalue row 1 colindex 2 result {SpecialChars.Variable}result2
            xls.getvalue row 1 colindex 4 result {SpecialChars.Variable}result3
            xls.getvalue row 2 colindex 7 result {SpecialChars.Variable}result4
            xls.getvalue row 5 colindex 27 result {SpecialChars.Variable}result6
            xls.getvalue row 5 colindex 52 result {SpecialChars.Variable}result7
            xls.getvalue row 5 colindex 53 result {SpecialChars.Variable}result8
            xls.getvalue row 5 colindex 728 result {SpecialChars.Variable}result9
            xls.getvalue row 5 colindex 731 result {SpecialChars.Variable}result10
            xls.getvalue row 5 colindex 26 result {SpecialChars.Variable}result11
";
            scripter.Run();
            Assert.AreEqual("1234", scripter.Variables.GetVariable("result1").GetValue().Object);
            Assert.AreEqual("abcd", scripter.Variables.GetVariable("result2").GetValue().Object);
            Assert.AreEqual("150", scripter.Variables.GetVariable("result3").GetValue().Object);
            
            Assert.AreEqual("AA", scripter.Variables.GetVariable("result6").GetValue().Object);
            Assert.AreEqual("AZ", scripter.Variables.GetVariable("result7").GetValue().Object);
            Assert.AreEqual("BA", scripter.Variables.GetVariable("result8").GetValue().Object);
            Assert.AreEqual("AAZ", scripter.Variables.GetVariable("result9").GetValue().Object);
            Assert.AreEqual("ABC", scripter.Variables.GetVariable("result10").GetValue().Object);
            Assert.AreEqual("Z", scripter.Variables.GetVariable("result11").GetValue().Object);
        }

        [Test]
        [Timeout(20000)]
        public void XlsGetValuePercentTest()
        {
            scripter.Text = $@"
            xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.getvalue row 1 colindex 5 result {SpecialChars.Variable}result1
            xls.getvalue row 2 colindex 5 result {SpecialChars.Variable}result2";
            scripter.Run();

            Assert.AreEqual("160%", scripter.Variables.GetVariable("result1").GetValue().Object);
            Assert.AreEqual("100%", scripter.Variables.GetVariable("result2").GetValue().Object);
        }

        [Test]
        [Timeout(20000)]
        public void XlsGetValueFloatTest()
        {
            scripter.Text = $@"
            xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.getvalue row 2 colindex 7 result {SpecialChars.Variable}result1
            ";
            scripter.Run();
            Assert.AreEqual(12.345f, scripter.Variables.GetVariable("result4").GetValue().Object);
        }

    }
}