﻿using G1ANT.Engine;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Ocr.AbbyyFineReader.Commands;
using G1ANT.Language.Ocr.AbbyyFineReader.Structures;
using G1ANT.Language.Semantic;
using GStructures = G1ANT.Language.Structures;

using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Properties;

namespace G1ANT.Addon.Ocr.AbbyyFineReader.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(OcrAbbyyProcessFile))]
    public class OcrAbbyyProcessFileTest
    {
        private static Scripter scripter;
        private static string path;

        [OneTimeSetUp]
        public void Initialize()
        {
            System.Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [SetUp]
        public void SetUp()
        {
            path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document3), "tif");
            scripter = new Scripter();
            scripter.Variables.SetVariableValue("file", new GStructures.String(path));
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void ProcessFileTest()
        {
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Variable}file language English");
            int documentId = scripter.Variables.GetVariableValue<int>("result");

            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            Assert.IsNotNull(document);
            string plainText = document.GetAllText();
            Assert.AreEqual(Properties.Resources.documentText, plainText);
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void PagesTest()
        {
            string doc4Path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.dokument4), "pdf");
            string endOfFirstPage = @"Nowa sekcja 3 Strona 1";

            List<GStructures.Structure> list = new List<GStructures.Structure>() { new GStructures.Integer(1) };
            scripter.Variables.SetVariableValue(nameof(list), new GStructures.List(list));
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{doc4Path}{SpecialChars.Text} pages {SpecialChars.Variable}{nameof(list)}");
            FineReaderDocument document = AbbyyManager.Instance.GetDocument(scripter.Variables.GetVariableValue<int>("result"));
            Assert.IsTrue(document.GetAllText().Trim().EndsWith(endOfFirstPage));
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void LanguageTest()
        {
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Variable}file language Polish");
            int documentId = scripter.Variables.GetVariableValue<int>("result");

            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
            Assert.IsNotNull(document);
            string plainText = document.GetAllText();
            Assert.AreNotEqual(Properties.Resources.documentText, plainText);
        }

        [Test, Timeout(AbbyTests.TestsTimeout)]
        public void ProcessWithCustomDirectoryTest()
        {
            string path = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.document1), "tif");
            List<string> words = new List<string>
            {
                "inwestycje",
                "długoterminowe",
                "aktywa",
                "trwałe",
                "prac",
                "prawne",
                "pozostałych",
                "akcje",
                "papiery",
                "pożyczki",
                "podatku"
            };

            List<GStructures.Structure> wordsList = new List<GStructures.Structure>(words.Capacity);
            foreach (string word in words)
            {
                wordsList.Add(new GStructures.String(word));
            }

            scripter.Variables.SetVariableValue(nameof(wordsList), new GStructures.List(wordsList));
            scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{path}{SpecialChars.Text} language Polish languageweight 0 dictionary {SpecialChars.Variable}{nameof(wordsList)}");
            //scripter.RunLine($"ocrabbyy.processfile {SpecialChars.Text}{Initializer.BgzBilans6Path}{SpecialChars.Text} dictionary {SpecialChars.Variable}{nameof(wordsList)}");
            int documentId = scripter.Variables.GetVariableValue<int>("result");

            FineReaderDocument document = AbbyyManager.Instance.GetDocument(documentId);
        }
    }
}