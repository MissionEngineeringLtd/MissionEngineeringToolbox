using System.Numerics;
using static System.Math;

namespace MissionEngineering.Math;

public partial class VectorComplex
{
    public Complex Sum()
    {
        Complex result = 0.0;

        for (int i = 0; i < NumberOfElements; i++)
        {
            result += Data[i];
        }

        return result;
    }

    public Vector Magnitude()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result.Data[i] = Complex.Abs(Data[i]);
        }

        return result;
    }

    public static VectorComplex Ones(int numberOfElements)
    {
        var result = new VectorComplex(numberOfElements);
        
        for (int i = 0; i < numberOfElements; i++)
        {
            result.Data[i] = 1.0;
        }
     
        return result;
    }

    public static VectorComplex LinearlySpacedVector(double start, double end, double step)
    {
        int numberOfElements = (int)Ceiling((end - start) / step) + 1;

        var data = new double[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            data[i] = start + i * step;
        }

        return new VectorComplex(data);
    }

    public static VectorComplex LinearlySpacedVector(double start, double step, int numberOfElements)
    {
        var data = new double[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            data[i] = start + i * step;
        }

        return new VectorComplex(data);
    }
}