using System;
using System.IO;

using G1ANT.Engine;
using G1ANT.Interop;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.AbbyyFineReader.Commands;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;
using G1ANT.Language.Semantic;
using GStruct = G1ANT.Language.Structures;

using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using G1ANT.Language.Ocr.AbbyyFineReader.Tests.Properties;

namespace G1ANT.Language.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyFilter))]
    public class OcrAbbyyFilterTests
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void FilterTest()
        {
            List<string> boldedTexts = new List<string>() { "In 1929", "In 1949", "In 1950,", "In 1955," };
            string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document2), "jpg");

            Scripter scripter = new Scripter();
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            scripter.RunLine($"ocrabbyy.filter filter bold");
            List<GStruct.Structure> res = scripter.Variables.GetVariableValue<List<GStruct.Structure>>("result");

            foreach (GStruct.Structure value in res)
            {
                string text = ((GStruct.String)value).Value.Trim();
                if (boldedTexts.Contains(text))
                {
                    boldedTexts.Remove(text);
                }
            }

            System.Text.StringBuilder notRecognizedBold = new System.Text.StringBuilder();
            foreach (string s in boldedTexts)
            {
                notRecognizedBold.Append($" '{s}'");
            }

            Assert.AreEqual(0, boldedTexts.Count, $"Text not recognized as boold {notRecognizedBold}");
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void InvalidFilterTest()
        {
            Scripter scripter = new Scripter();
            string invalidFilter = "filterThatDoNotExists";
            Exception exception = Assert.Throws<ApplicationException>(delegate
            {
                scripter.RunLine($"ocrabbyy.filter filter {invalidFilter}");
            });
            Assert.IsInstanceOf<ArgumentOutOfRangeException>(exception.GetBaseException());
        }
    }
}
