using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const double xMin = -1.5;
        private const double xMax = 1.5;
        private const double yMin = -1.5;
        private const double yMax = 1.5;

        private const double xStep = (xMax - xMin) / 300;
        private const double yStep = (yMax - yMin) / 300;

        private static Bitmap bitMapImage = new Bitmap(300, 300);

        private static Polynomial polynomial, polynomialDerivation;

        private static List<ComplexNumber> roots;

        private static Color[] colors = new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Yellow,
                Color.Orange,
                Color.Fuchsia,
                Color.Gold,
                Color.Cyan,
                Color.Magenta
            };

        static void Main(string[] args)
        {
            roots = new List<ComplexNumber>();
            polynomial = PolynomialInicialization();
            polynomialDerivation = polynomial.Derivation();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivation);

            ProcessImage();
            SaveImage();
        }

        static Polynomial PolynomialInicialization()
        {
            return new Polynomial()
            {
                Coefficients =
                {
                    new ComplexNumber() {RealNumber = 1},
                    ComplexNumber.Zero,
                    ComplexNumber.Zero,
                    new ComplexNumber() {RealNumber = 1}
                }
            };
        }

        static void ProcessImage()
        {
            for (int i = 0; i < bitMapImage.Height; i++)
            {
                for (int j = 0; j < bitMapImage.Width; j++)
                {
                    ComplexNumber point = FindWorldCoordinates(i, j);
                    int iterations = CalculateNewtonsIteration(ref point);
                    int rootIndex = FindRootIndex(point);
                    ColorizePixel(j, i, iterations, rootIndex);
                }
            }
        }

        static void SaveImage()
        {
            bitMapImage.Save("../../../out.png");
        }

        static ComplexNumber FindWorldCoordinates(int i, int j)
        {
            double x = xMin + j * xStep;
            double y = yMin + i * yStep;

            ComplexNumber pixel = new ComplexNumber()
            {
                RealNumber = x,
                Imaginary = y
            };

            if (pixel.RealNumber == 0)
            {
                pixel.RealNumber = 0.0001;
            }
            
            if (pixel.Imaginary == 0)
            {
                pixel.Imaginary = 0.0001;
            }

            return pixel;
        }

        static int CalculateNewtonsIteration(ref ComplexNumber point)
        {
            int iterations = 0;

            for (int i = 0; i < 30; i++)
            {
                ComplexNumber diff = polynomial.Evaluate(point).Divide(polynomialDerivation.Evaluate(point));
                point = point.Subtract(diff);

                if (Math.Pow(diff.RealNumber, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                {
                    i--;
                }

                iterations++;
            }

            return iterations;
        }

        static int FindRootIndex(ComplexNumber point)
        {
            bool known = false;
            int rootIndex = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(point.RealNumber - roots[i].RealNumber, 2) + Math.Pow(point.Imaginary - roots[i].Imaginary, 2) <= 0.01)
                {
                    known = true;
                    rootIndex = i;
                }
            }

            if (!known)
            {
                roots.Add(point);
                rootIndex = roots.Count;
            }

            return rootIndex;
        }

        static void ColorizePixel(int x, int y, int iterations, int rootIndex)
        {
            Color color = colors[rootIndex % colors.Length];

            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - iterations * 2), 255),//RED
                Math.Min(Math.Max(0, color.G - iterations * 2), 255),//GREEN
                Math.Min(Math.Max(0, color.B - iterations * 2), 255)//BLUE
                );
            
            bitMapImage.SetPixel(x, y, color);
        }

    }
}
