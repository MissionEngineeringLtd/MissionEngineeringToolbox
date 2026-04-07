using static System.Math;

namespace MissionEngineering.Math;

public partial class Vector
{
    public static Vector operator +(Vector left, Vector right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] + right.Data[i];
        }

        return result;
    }

    public static Vector operator -(Vector left)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = -left.Data[i];
        }

        return result;
    }

    public static Vector operator -(Vector left, Vector right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] - right.Data[i];
        }

        return result;
    }

    public static Vector operator *(Vector left, double right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] * right;
        }

        return result;
    }

    public static Vector operator *(double left, Vector right)
    {
        var result = right * left;

        return result;
    }

    public static Vector operator *(Vector left, Vector right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] * right.Data[i];
        }

        return result;
    }

    public static Vector operator /(Vector left, double right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] / right;
        }

        return result;
    }

    public static Vector operator /(double left, Vector right)
    {
        var result = new Vector(right.NumberOfElements);

        for (int i = 0; i < right.NumberOfElements; i++)
        {
            result.Data[i] = left / right.Data[i];
        }

        return result;
    }

    public static Vector LinearlySpacedVector(double start, double end, double step)
    {
        int numberOfElements = (int)Ceiling((end - start) / step) + 1;

        var data = new double[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            data[i] = start + i * step;
        }

        return new Vector(data);
    }
}