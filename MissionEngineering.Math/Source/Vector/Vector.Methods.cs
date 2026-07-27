using System.Numerics;
using System.Security.Cryptography;

namespace MissionEngineering.Math;

public partial class Vector
{
    public static double DotProduct(Vector x, Vector y)
    {
        var result = 0.0;

        for (int i = 0; i < x.NumberOfElements; i++)
        {
            result += x.Data[i] * y.Data[i];
        }

        return result;
    }

    public static Vector CrossProduct(Vector x, Vector y)
    {
        var result = new Vector(3);

        result[0] = x[1] * y[2] - x[2] * y[1];
        result[1] = x[2] * y[0] - x[0] * y[2];
        result[2] = x[0] * y[1] - x[1] * y[0];

        return result;
    }

    public double Norm()
    {
        double sum = DotProduct(this, this);

        return System.Math.Sqrt(sum);
    }

    public Vector Sqrt()
    {
        var result = new Vector(NumberOfElements);

        for (int i = 0; i < NumberOfElements; i++)
        {
            result[i] = System.Math.Sqrt(Data[i]);
        }

        return result;
    }

    public Vector UnitVector()
    {
        var result = this / Norm();

        return result;
    }

    public double DotProduct(Vector x)
    {
        var result = DotProduct(this, x);

        return result;
    }

    public Vector CrossProduct(Vector x)
    {
        var result = CrossProduct(this, x);

        return result;
    }

    public static double AngleBetweenVectors_rad(Vector x, Vector y)
    {
        var xUnit = x.UnitVector();
        var yUnit = y.UnitVector();

        var dotProduct = xUnit.DotProduct(yUnit);

        var angle = System.Math.Acos(dotProduct);

        return angle;
    }

    public static double AngleBetweenVectors_deg(Vector x, Vector y)
    {
        var angle_rad = AngleBetweenVectors_rad(x, y);

        var angle_deg = angle_rad.RadiansToDegrees();

        return angle_deg;
    }

    public double AngleBetweenVectors_rad(Vector x)
    {
        var angle_rad = AngleBetweenVectors_rad(this, x);

        return angle_rad;
    }

    public double AngleBetweenVectors_deg(Vector x)
    {
        var angle_deg = AngleBetweenVectors_deg(this, x);

        return angle_deg;
    }
}