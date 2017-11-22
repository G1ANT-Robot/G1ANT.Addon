﻿using AForge.Imaging.Filters;
using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Images.Api
{
    public class AForgeWrapper
    {
        public static Bitmap InvertImage(Bitmap image)
        {
            Invert filter = new Invert();
            return filter.Apply(image);
        }

        public static List<Rectangle> FindRectangles(Bitmap image, bool invert, int? minWidth, int? maxWidth, int? minHeight, int? maxHeight)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            Bitmap workingImage = new Bitmap(image);
            if (invert)
            {
                workingImage = InvertImage(image);
            }
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage((Bitmap)workingImage);
            var foundRectangles = blobCounter.GetObjectsRectangles();

            foreach (var foundRectangle in foundRectangles)
            {
                if (RectangleFitInBounds(foundRectangle, workingImage, minWidth, maxWidth, minHeight, maxHeight))
                    rectangles.Add(foundRectangle);
            }
            return rectangles;
        }

        private static bool RectangleFitInBounds(Rectangle foundRectangle, Bitmap image, int? minWidth, int? maxWidth, int? minHeight, int? maxHeight)
        {
            return
                (minWidth == null | foundRectangle.Width > minWidth * image.Width / 100)
                & (maxWidth == null | foundRectangle.Width < maxWidth * image.Width / 100)
                & (minHeight == null | foundRectangle.Height > minHeight * image.Height / 100)
                & (maxHeight == null | foundRectangle.Height < maxHeight * image.Height / 100);
        }

        public static void ApplyMatrix(Bitmap image, int[,] matrix, int threshold = 1)
        {
            Sharpen sharpenFilter = new Sharpen()
            {
                Kernel = matrix,
                Threshold = threshold
            };
            sharpenFilter.ApplyInPlace(image);
        }
    }
}
