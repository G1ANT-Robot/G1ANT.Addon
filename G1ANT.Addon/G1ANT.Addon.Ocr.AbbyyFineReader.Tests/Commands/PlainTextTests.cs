using System;
using System.Collections.Generic;
using System.IO;

using G1ANT.Engine;
using G1ANT.Interop;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.AbbyyFineReader.Commands;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;
using G1ANT.Language.Semantic;
using GStruct = G1ANT.Language.Structures;

using NUnit.Framework;
using G1ANT.Language.Ocr.AbbyyFineReader.Tests.Properties;
using System.Reflection;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyPlainText))]
    public class PlainTextTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document2), "jpg");

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void PlainTextTest()
        {
            Scripter scripter = new Scripter();
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            scripter.RunLine($"ocrabbyy.plaintext");
            string plainText = scripter.Variables.GetVariableValue<string>("result");
            Assert.IsTrue(plainText.Contains("In 1929 Gustav Tauschek obtained a patent on OCR in Germany, followed"));
            Assert.IsTrue(plainText.Contains("In about 1965, Reader's Digest and RCA collaborated to build an OCR Document"));
        }
    }
}
