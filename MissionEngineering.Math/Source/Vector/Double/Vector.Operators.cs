using static System.Math;
using MathNet.Numerics.IntegralTransforms;

namespace MissionEngineering.Math;

public partial class Vector
{
    public static Vector operator +(Vector left, double right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] + right;
        }

        return result;
    }

    public static Vector operator +(double left, Vector right)
    {
        var result = right + left;

        return result;
    }

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

    public static Vector operator -(double left, Vector right)
    {
        var result = new Vector(right.NumberOfElements);

        for (int i = 0; i < right.NumberOfElements; i++)
        {
            result.Data[i] = left - right.Data[i];
        }

        return result;
    }

    public static Vector operator -(Vector left, double right)
    {
        var result = new Vector(left.NumberOfElements);

        for (int i = 0; i < left.NumberOfElements; i++)
        {
            result.Data[i] = left.Data[i] - right;
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
}