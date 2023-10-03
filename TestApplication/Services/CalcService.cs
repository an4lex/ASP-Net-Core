using TestApplication.Interfaces;

namespace TestApplication.Services {
    public class CalcService: CalcServiceInterface {
        public float Multiply(float a, float b) => a * b;

        public float Divide(float a, float b) {
            if (b == 0)
                throw new DivideByZeroException();

            return (float)a / b;
        }

        public float Sum(float a, float b) => a + b;

        public float Subtract(float a, float b) => a - b;
    }
}