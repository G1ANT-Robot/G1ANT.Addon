using G1ANT.Engine;
using G1ANT.Language.Ocr;
using G1ANT.Language;
using System.Diagnostics;
using System.Drawing;
using NUnit.Framework;
using System.Reflection;
using System;
using G1ANT.Addon.Ocr.Tests.Properties;

namespace G1ANT.Addon.Ocr.Tests
{
    [TestFixture]
    public class OcrFindTests
    {
        private static Scripter scripter;
        private string path;
        private Process proces;

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void TestInit()
        {
            path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.testimage), "png");
            scripter = new Scripter();
            GoogleOcrTests.StartPaint(path);
        }

        [Test, Timeout(GoogleOcrTests.TestTimeout)]
        public void OcrFindCommandTest()
        {
            System.Drawing.Rectangle expectedRectangle = new Rectangle(64, 102, 191, 51);
            string script = $@"
            window {SpecialChars.Text + SpecialChars.Search}Paint{SpecialChars.Text + SpecialChars.Search} style maximize
            ocr.find search {SpecialChars.Text}animal{SpecialChars.Text} area (rectangle)110,184,564,488
            ";
            scripter.Text = script;
            scripter.Run();
            var resultRectangle = scripter.Variables.GetVariableValue<Rectangle>("result");
            Assert.IsTrue(AreRectanglesSimilar(expectedRectangle, resultRectangle, 10));
        }

        [TearDown]
        public void TestCleanup()
        {
            GoogleOcrTests.KillAllPaints();
        }

        private bool AreRectanglesSimilar(Rectangle r1, Rectangle r2, int tolerance)
        {
            if (Math.Abs(r1.X - r2.X) > tolerance)
            {
                return false;
            }
            if (Math.Abs(r1.Y - r2.Y) > tolerance)
            {
                return false;
            }
            if (Math.Abs(r1.Width - r2.Width) > tolerance)
            {
                return false;
            }
            if (Math.Abs(r1.Height - r2.Height) > tolerance)
            {
                return false;
            }
            return true;
        }
    }
}
