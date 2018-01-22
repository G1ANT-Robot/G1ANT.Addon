﻿using System;
using System.Collections.Generic;
using System.IO;

using G1ANT.Engine;

using NUnit.Framework;
using System.Reflection;
using G1ANT.Language;
using G1ANT.Addon.Xls.Tests.Properties;

namespace G1ANT.Addon.Xls.Tests
{
    [TestFixture]
    public class XlsFindTest
    {
        string file;
        string file2;
        Scripter scripter;
        [OneTimeSetUp]
        public void ClassInit()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            file = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.XlsTestWorkbook), "xlsx");
            file2 = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.EmptyWorkbook), "xlsx");

        }
        [SetUp]
        public void testinit()
        {
            Language.Addon addon = Language.Addon.Load(@"G1ANT.Addon.Xls.dll");
            scripter = new Scripter();
            scripter.InitVariables.Clear();
            scripter.InitVariables.Add("xlsPath", new TextStructure(file));
            scripter.Text = $"xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id";
            scripter.Run();
        }
        [Test]
        [Timeout(40000)]
        public void XlsFindDifferentTypesTest()
        {

            scripter.Text = $@"xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.find 1234 resultrow {SpecialChars.Variable}resrow resultcolumn {SpecialChars.Variable}resCol
            xls.find {SpecialChars.Text}abcd{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow2 resultcolumn {SpecialChars.Variable}resCol2
            xls.find 150 resultrow {SpecialChars.Variable}resrow3 resultcolumn {SpecialChars.Variable}resCol3
            -xls.find {SpecialChars.Text}160%{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow4 resultcolumn {SpecialChars.Variable}resCol4
            -xls.find {SpecialChars.Text}100%{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow5 resultcolumn {SpecialChars.Variable}resCol5
            xls.find {SpecialChars.Text}AA{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow6 resultcolumn {SpecialChars.Variable}resCol6
            xls.find {SpecialChars.Text}AZ{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow7 resultcolumn {SpecialChars.Variable}resCol7
            xls.find {SpecialChars.Text}BA{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow8 resultcolumn {SpecialChars.Variable}resCol8
            xls.find {SpecialChars.Text}AAZ{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow9 resultcolumn {SpecialChars.Variable}resCol9
            xls.find {SpecialChars.Text}ABC{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow10 resultcolumn {SpecialChars.Variable}resCol10
            xls.find {SpecialChars.Text}Z{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow11 resultcolumn {SpecialChars.Variable}resCol11

";
            scripter.Run();
            Assert.AreEqual(1, scripter.Variables.GetVariable("resrow").GetValue().Object);
            Assert.AreEqual(1, scripter.Variables.GetVariable("resCol").GetValue().Object);

            Assert.AreEqual(1, scripter.Variables.GetVariable("resrow2").GetValue().Object);
            Assert.AreEqual(2, scripter.Variables.GetVariable("resCol2").GetValue().Object);

            Assert.AreEqual(1, scripter.Variables.GetVariable("resrow3").GetValue().Object);
            Assert.AreEqual(4, scripter.Variables.GetVariable("resCol3").GetValue().Object);

            //Assert.AreEqual(1, scripter.Variables.GetVariable("resrow4").GetValue().Object);
            //Assert.AreEqual(5, scripter.Variables.GetVariable("resCol4").GetValue().Object);

            //Assert.AreEqual(2, scripter.Variables.GetVariable("resrow5").GetValue().Object);
            //Assert.AreEqual(5, scripter.Variables.GetVariable("resCol5").GetValue().Object);

            Assert.AreEqual(5, scripter.Variables.GetVariable("resrow6").GetValue().Object);
            Assert.AreEqual(27, scripter.Variables.GetVariable("resCol6").GetValue().Object);

            Assert.AreEqual(5, scripter.Variables.GetVariable("resrow7").GetValue().Object);
            Assert.AreEqual(52, scripter.Variables.GetVariable("resCol7").GetValue().Object);

            Assert.AreEqual(5, scripter.Variables.GetVariable("resrow8").GetValue().Object);
            Assert.AreEqual(53, scripter.Variables.GetVariable("resCol8").GetValue().Object);

            Assert.AreEqual(5, scripter.Variables.GetVariable("resrow9").GetValue().Object);
            Assert.AreEqual(728, scripter.Variables.GetVariable("resCol9").GetValue().Object);

            Assert.AreEqual(5, scripter.Variables.GetVariable("resrow10").GetValue().Object);
            Assert.AreEqual(731, scripter.Variables.GetVariable("resCol10").GetValue().Object);

            Assert.AreEqual(5, scripter.Variables.GetVariable("resrow11").GetValue().Object);
            Assert.AreEqual(26, scripter.Variables.GetVariable("resCol11").GetValue().Object);
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindPercentTest()
        {
            scripter.Text = $@"xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.find {SpecialChars.Text}160%{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow4 resultcolumn {SpecialChars.Variable}resCol4
            xls.find {SpecialChars.Text}100%{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow5 resultcolumn {SpecialChars.Variable}resCol5
";
            scripter.Run();
            Assert.AreEqual(1, scripter.Variables.GetVariable("resrow4").GetValue().Object);
            Assert.AreEqual(5, scripter.Variables.GetVariable("resCol4").GetValue().Object);

            Assert.AreEqual(2, scripter.Variables.GetVariable("resrow5").GetValue().Object);
            Assert.AreEqual(5, scripter.Variables.GetVariable("resCol5").GetValue().Object);
        }

        [Test]
        [Timeout(20000)]
        public void XlsFindDateTest()
        {
            scripter.Text = $@"xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.find {SpecialChars.Text}21.07.2017{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow1 resultcolumn {SpecialChars.Variable}resCol1
            xls.find {SpecialChars.Text}22.07.2017{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow2 resultcolumn {SpecialChars.Variable}resCol2
";
            scripter.Run();
            Assert.AreEqual(1, scripter.Variables.GetVariable("resrow1").GetValue().Object);
            Assert.AreEqual(3, scripter.Variables.GetVariable("resCol2").GetValue().Object);

            Assert.AreEqual(2, scripter.Variables.GetVariable("resrow2").GetValue().Object);
            Assert.AreEqual(3, scripter.Variables.GetVariable("resCol2").GetValue().Object);
        }

        [Test]
        [Timeout(20000)]
        public void XlsFailToFind()
        {
            scripter.Text = $@"xls.open {SpecialChars.Variable}xlsPath result {SpecialChars.Variable}id
            xls.find {SpecialChars.Text}01.01.1001{SpecialChars.Text} resultrow {SpecialChars.Variable}resrow1 resultcolumn {SpecialChars.Variable}resCol1
            ";
            scripter.Run();
            Assert.AreEqual(-1, int.Parse(scripter.Variables.GetVariable("resrow1").GetValue().ToString()));
            Assert.AreEqual(-1, int.Parse(scripter.Variables.GetVariable("resCol1").GetValue().ToString()));
        }
    }
}
