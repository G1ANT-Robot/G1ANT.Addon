using G1ANT.Addon.Xlsx.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace G1ANT.Addon.Xlsx.Tests
{
    [TestFixture]
    public class CountRowsTests
    {
        Scripter scripter;
        string file;
        string file2;

        [OneTimeSetUp]
        [Timeout(20000)]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            file2 = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.EmptyWorkbook), "xlsx");
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Xlsx.dll");
            scripter = new Scripter();
        }
       
        [Test]
        [Timeout(20000)]
        public void CountRowsTest()
        {
            int rowCount;
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("xlsPath", new TextStructure(file));
            scripter.Text = $@"xlsx.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xlsx.countrows result {SpecialChars.Variable}rowCunt";
            scripter.Run();
            rowCount = scripter.Variables.GetVariableValue<int>("rowCunt", -1, true);
            Assert.AreEqual(5, rowCount);
        }

        [Test]
        [Timeout(20000)]
        public void CountRowsInEmptyWoorkbookTest()
        {
            int rowCount;
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("xlsPath", new TextStructure(file2));
            scripter.Text = $@"xlsx.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xlsx.countrows result {SpecialChars.Variable}rowCount";
            scripter.Run();
            rowCount = scripter.Variables.GetVariableValue<int>("rowCount", -1, true);
            Assert.AreEqual(0, rowCount);
        }

        [OneTimeTearDown]
        [Timeout(10000)]
        public void ClassCleanUp()
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            if (File.Exists(file2))
            {
                File.Delete(file2);
            }
        }
    }
}
