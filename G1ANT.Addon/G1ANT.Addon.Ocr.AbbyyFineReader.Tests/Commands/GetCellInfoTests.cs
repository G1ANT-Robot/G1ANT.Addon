
using G1ANT.Engine;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.AbbyyFineReader.Commands;
using G1ANT.Language.Ocr.AbbyyFineReader.Tests.Properties;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using System.Reflection;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyGetCellInfo))]
    public class GetCellInfoTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void GetCellInfoTest()
        {
            Scripter scripter = new Scripter();
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
