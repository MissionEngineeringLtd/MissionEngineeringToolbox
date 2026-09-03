using MathNet.Numerics.IntegralTransforms;

namespace MissionEngineering.Math;

public static class VectorExtensions
{
    extension(Vector x)
    {
        public Vector PowerToDecibels()
        {
            var result = new Vector(x.NumberOfElements);

            for (int i = 0; i < x.NumberOfElements; i++)
            {
                result[i] = 10.0 * System.Math.Log10(x.Data[i]);
            }

            return result;
        }

        public Vector DecibelsToPower()
        {
            var result = new Vector(x.NumberOfElements);

            for (int i = 0; i < x.NumberOfElements; i++)
            {
                result[i] = System.Math.Pow(10.0, x.Data[i] / 10.0);
            }

            return result;
        }

        public Vector FFT()
        {
            var data = x.Data;

            // Calculate the FFT as an array of complex numbers:
            var spectrum = FftSharp.FFT.Forward(x.Data);

            // Get the magnitude:
            double[] magnitude = FftSharp.FFT.Magnitude(spectrum);

            var y = new Vector(magnitude);

            return y;
        }
    }
}