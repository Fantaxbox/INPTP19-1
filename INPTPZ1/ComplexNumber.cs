namespace INPTPZ1
{
    public class ComplexNumber
    {
        public double RealNumber { get; set; }
        public double Imaginary { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealNumber = 0,
            Imaginary = 0
        };

        public ComplexNumber Multiply(ComplexNumber second)
        {
            return new ComplexNumber()
            {
                RealNumber = this.RealNumber * second.RealNumber - this.Imaginary * second.Imaginary,
                Imaginary = this.RealNumber * second.Imaginary + this.Imaginary * second.RealNumber
            };
        }

        public ComplexNumber Add(ComplexNumber second)
        {
            return new ComplexNumber()
            {
                RealNumber = this.RealNumber + second.RealNumber,
                Imaginary = this.Imaginary + second.Imaginary
            };
        }

        public ComplexNumber Subtract(ComplexNumber second)
        {
            return new ComplexNumber()
            {
                RealNumber = this.RealNumber - second.RealNumber,
                Imaginary = this.Imaginary - second.Imaginary
            };
        }

        public override string ToString()
        {
            return $"({RealNumber} + {Imaginary}i)";
        }

        internal ComplexNumber Divide(ComplexNumber second)
        {
            ComplexNumber numerator = this.Multiply(new ComplexNumber() { RealNumber = second.RealNumber, Imaginary = -second.Imaginary });
            double denominator = second.RealNumber * second.RealNumber + second.Imaginary * second.Imaginary;

            return new ComplexNumber()
            {
                RealNumber = numerator.RealNumber / denominator,
                Imaginary = numerator.Imaginary / denominator
            };
        }
    }
}
