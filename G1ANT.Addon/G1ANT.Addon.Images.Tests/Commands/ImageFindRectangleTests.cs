using System.Collections.Generic;

using G1ANT.Engine;
using G1ANT.Language.Semantic;
using NUnit.Framework;
using G1ANT.Language.Core.Tests;
using G1ANT.Language.Images.Commands;
using System.Reflection;
using G1ANT.Language.Images.Tests.Properties;
using System;

namespace G1ANT.Language.Images.Tests.Commands
{
    [TestFixture]
    [TestsClass(typeof(ImageFindRectangles))]
    public class ImageFindRectangleTests
    {
        [OneTimeSetUp]
        public void Initialize()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            scripter = new Scripter();
        }

        private Scripter scripter;

        [Test, Timeout(ImagesTests.TestsTimeout)]
        public void FindAllTest()
        {
            int expectedRectanglesCount = 8;
            string imageWithRectanglesPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.Rectangles8), "png");
            scripter.RunLine($"image.findrectangles {SpecialChars.Text}{imageWithRectanglesPath}{SpecialChars.Text}");
            Assert.AreEqual(expectedRectanglesCount, scripter.Variables.GetVariableValue<List<Structures.Structure>>("result").Count);
        }

        [Test, Timeout(ImagesTests.TestsTimeout)]
        public void WidthFilterTest()
        {
            int widthTreshold = 180*100/1900;
            int expectedRectanglesCount = 2;
            string imageWithRectanglesPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.Rectangles8), "png");

            scripter.RunLine($"image.findrectangles {SpecialChars.Text}{imageWithRectanglesPath}{SpecialChars.Text} maxwidth {widthTreshold}");
            Assert.AreEqual(expectedRectanglesCount, scripter.Variables.GetVariableValue<List<Structures.Structure>>("result").Count);

            imageWithRectanglesPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.Rectangles8), "png");

            expectedRectanglesCount = 6;
            scripter.RunLine($"image.findrectangles {SpecialChars.Text}{imageWithRectanglesPath}{SpecialChars.Text} minwidth {widthTreshold}");
            Assert.AreEqual(expectedRectanglesCount, scripter.Variables.GetVariableValue<List<Structures.Structure>>("result").Count);
        }

        [Test, Timeout(ImagesTests.TestsTimeout)]
        public void HeigthFilterTest()
        {
            int maxHeight = 250*100/850;
            int minHeight = 80*100/850;
            int expectedRectanglesCount = 5;
            string imageWithRectanglesPath = Assembly.GetExecutingAssembly().UnpackResourceToFile(nameof(Resources.Rectangles8), "png");

            scripter.RunLine($"image.findrectangles {SpecialChars.Text}{imageWithRectanglesPath}{SpecialChars.Text} maxheight {maxHeight} minheight {minHeight}");
            Assert.AreEqual(expectedRectanglesCount, scripter.Variables.GetVariableValue<List<Structures.Structure>>("result").Count);
        }
    }
}
