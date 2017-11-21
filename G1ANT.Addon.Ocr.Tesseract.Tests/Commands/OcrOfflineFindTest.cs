using G1ANT.Addon.Ocr.Tesseract.Tests.Properties;
using G1ANT.Engine;
using G1ANT.Language;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;

namespace G1ANT.Addon.Ocr.Tesseract.Tests
{
    [TestFixture]
    public class OcrOfflineFindTest
    {
        private static Scripter scripter;
        private Process proces;
        private static string filePath = string.Empty;

        [OneTimeSetUp]
        public static void OcrOfflineTestsInitialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            filePath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.testimageDance), "png");
        }

        [SetUp]
        public void TestInit()
        {
            scripter = new Scripter();
            var processInfo = new ProcessStartInfo("mspaint.exe", $"\"{filePath}\"");
            processInfo.WindowStyle = ProcessWindowStyle.Maximized;
            proces = Process.Start(processInfo);
            proces.WaitForInputIdle();
            Thread.Sleep(3000);

        }

        [Test]
        [Timeout(30000)]
        public void OcrOfflineFindCommandTest()
        {
            Rectangle expectedRectangle = new Rectangle(51, 91, 161, 43);

            //            window {SpecialChars.Search}Paint{SpecialChars.Search} style maximize
            string script = $@"
            ocroffline.find search {SpecialChars.Text}Dance{SpecialChars.Text} area (rectangle)68,162,767,528
            ";
            scripter.Text = script;
            scripter.Run();
            var resultRectangle = scripter.Variables.GetVariableValue<Rectangle>("result");
            Assert.IsTrue(AreRectanglesSimilar(expectedRectangle, resultRectangle, 10));
        }

        [TearDown]
        public void TestCleanup()
        {
            if (proces != null && !proces.HasExited)
            {
                proces.Kill();
            }
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


