using G1ANT.Engine;
using G1ANT.Language.Ocr.Tests.Properties;
using G1ANT.Language;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace G1ANT.Language.Ocr.Tests.Commands
{
    [TestFixture]
    public class OcrFromScreenTests
    {
        private static Scripter scripter;
        private Process proces;
        private string path;

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            //Window window = new Window(); //TODO  was G1ANT.Language.Core.Commands.Window i think do wyjebania
        }

        [SetUp]
        public void OcrFromScreenInitialize()
        {
            path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.testimage), "png");
            scripter = new Scripter();
            GoogleOcrTests.StartPaint(path);
            scripter.RunLine($"window {SpecialChars.Text + SpecialChars.Search}Paint{SpecialChars.Text + SpecialChars.Search} style maximize");
        }

        [Test, Timeout(GoogleOcrTests.TestTimeout)]
        public void GoogleOcrTest()
        {
            string expectedString = "animal";
            string script = "ocr.fromscreen area 68,162,767,528";
            scripter.Text = script;
            scripter.Run();
            var resultOutput = scripter.Variables.GetVariableValue<string>("result");
            Assert.AreEqual(expectedString, resultOutput);
        }

        [Test, Timeout(GoogleOcrTests.TestTimeout)]
        public void OcrTestGoogleApiTest()
        {
            var bitmapWithTestText = Language.Ocr.Tests.Properties.Resources.testimage;
            var expectedRectangle = new Rectangle(167, 142, 191, 51);
            var languages = new List<string>() { "en" };
            var timeout = 10000;
            GoogleCloudApi googleApi = new GoogleCloudApi();
            Rectangle foundRectangle = googleApi.RecognizeText(bitmapWithTestText, "animal", languages, timeout);
            Assert.AreEqual(expectedRectangle, foundRectangle);
        }

        [TearDown]
        public void OcrFromScreenCleanup()
        {
            GoogleOcrTests.KillAllPaints();
        }
    }
}