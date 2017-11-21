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
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;
using System.Reflection;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyGetTextInParagraphs))]
    public class GetTextInParagraphsTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document2), "jpg");
        Scripter scripter = new Scripter();

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void GetParagraphsTest()
        {
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            List<GStruct.Structure> paragraphs;
            scripter.RunLine($"ocrabbyy.gettextparagraphs result {nameof(paragraphs)}");
            paragraphs = scripter.Variables.GetVariableValue<List<GStruct.Structure>>(nameof(paragraphs));
            Assert.IsNotNull(paragraphs);
            Assert.AreNotEqual(0, paragraphs.Count);
            Assert.AreEqual(8, paragraphs.Count);
            Assert.IsTrue(((GStruct.String)paragraphs[0]).Value.StartsWith("In 1929 Gustav Tauschek obtained a patent on OCR in Germany, followed"));
            Assert.IsTrue(((GStruct.String)paragraphs[5]).Value.StartsWith("In about 1965, Reader's Digest and RCA collaborated to build an OCR Document"));
        }
    }
}
