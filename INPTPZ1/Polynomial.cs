using System.Collections.Generic;

namespace INPTPZ1
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public Polynomial Derivation()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealNumber = i }));
            }

            return polynomial;
        }

        public ComplexNumber Evaluate(ComplexNumber secondComplexNumber)
        {
            ComplexNumber zeroComplexNumber = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = this.Coefficients[i];
                ComplexNumber secondComplexNumberClone = secondComplexNumber;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        secondComplexNumberClone = secondComplexNumberClone.Multiply(secondComplexNumber);
                    }

                    coefficient = coefficient.Multiply(secondComplexNumberClone);
                }

                zeroComplexNumber = zeroComplexNumber.Add(coefficient);
            }

            return zeroComplexNumber;
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        result += "x";
                    }
                }

                result += " + ";
            }

            return result;
        }
    }
}
