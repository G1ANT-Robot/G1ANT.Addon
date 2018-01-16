
using G1ANT.Engine;
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;
using G1ANT.Language;
using NUnit.Framework;
using System.Reflection;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests
{
    [TestFixture]
    public class GetCellInfoTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        [SetUp]
        public void Init()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.AbbyyFineReader.dll");
        }
        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void GetCellInfoTest()
        {
            Scripter scripter = new Scripter();
scripter.InitVariables.Clear();
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            scripter.RunLine($"ocrabbyy.getcellinfo position 8,3");
            System.Drawing.Point cellInfo = scripter.Variables.GetVariableValue<System.Drawing.Point>("result");
            Assert.AreEqual(1, cellInfo.X);
            Assert.AreEqual(1, cellInfo.Y);
            scripter.RunLine($"ocrabbyy.getcellinfo position 1,6");
            cellInfo = scripter.Variables.GetVariableValue<System.Drawing.Point>("result");
            Assert.AreEqual(1, cellInfo.X);
            Assert.AreEqual(5, cellInfo.Y);
        }
    }
}
