using G1ANT.Addon.Ocr.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using G1ANT.Language.Ocr;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;

namespace G1ANT.Addon.Ocr.Tests
{
    [TestFixture]
    public class OcrFromScreenTests
    {
        private Scripter scripter;
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
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Ocr.dll");
            path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.testimage), "png");
            scripter = new Scripter();
            scripter.InitVariables.Clear();
            GoogleOcrTests.StartPaint(path);
        }

        [Test, Timeout(GoogleOcrTests.TestTimeout)]
        public void GoogleOcrTest()
        {
            string expectedString = "animal";
            string script = $@"window {SpecialChars.Text + SpecialChars.Search}Paint{SpecialChars.Text + SpecialChars.Search} style maximize
                            ocr.login {SpecialChars.Text}{Resources.JsonCredentials}{SpecialChars.Text}
                            ocr.fromscreen area (rectangle)68{SpecialChars.Point}162{SpecialChars.Point}767{SpecialChars.Point}528";
            scripter.Text = script;
            scripter.Run();
            var resultOutput = scripter.Variables.GetVariableValue<string>("result");
            Assert.AreEqual(expectedString, resultOutput);
        }

        [Test, Timeout(GoogleOcrTests.TestTimeout)]
        public void OcrTestGoogleApiTest()
        {
            GoogleCloudApi.JsonCredential = Resources.JsonCredentials;
            var bitmapWithTestText = Addon.Ocr.Tests.Properties.Resources.testimage;
            var expectedRectangle = new Rectangle(167, 142, 192, 51);
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