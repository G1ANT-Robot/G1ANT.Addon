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
using System.Reflection;
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyReadTables))]
    public class ReadTablesTests
    {
        string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");

        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void ReadTabletest()
        {
            Scripter scripter = new Scripter();

            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text}");
            scripter.RunLine($"ocrabbyy.readtables");
            List<GStruct.Structure> cells = scripter.Variables.GetVariableValue<List<GStruct.Structure>>("result");

            List<string> values = new List<string>() { "Egypt", "Nigeria" };

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < values.Count; j++)
                {
                    if (values[j] == ((GStruct.String)cells[i]).Value.Trim())
                    {
                        values.Remove(values[j]);
                    }
                }
            }

            Assert.AreEqual(0, values.Count, $"Havent found values {values}");
        }
    }
}
