using CommunityToolkit.Diagnostics;
using static System.Math;

namespace MissionEngineering.MathLibrary;

public static class MathFunctions
{
    public static double ConstrainAngle0To2PI(double x)
    {
        var result = x;

        if (x > 2.0 * PI)
        {
            result -= 2.0 * PI;
        }

        if (x < 0.0)
        {
            result += 2.0 * PI;
        }

        return result;
    }

    public static double ConstrainAnglePlusMinusPI(double x)
    {
        var result = x;

        if (x > PI)
        {
            result -= 2.0 * PI;
        }

        if (x < -180.0)
        {
            result += 2.0 * PI;
        }

        return result;
    }

    public static double ConstrainAngle0To360(double x)
    {
        var result = x;

        if (x > 360.0)
        {
            result -= 360.0;
        }

        if (x < 0.0)
        {
            result += 360.0;
        }

        return result;
    }

    public static double ConstrainAnglePlusMinus180(double x)
    {
        var result = x;

        if (x > 180.0)
        {
            result -= 360.0;
        }

        if (x < -180.0)
        {
            result += 360.0;
        }

        return result;
    }

    public static double LimitWithinRange(double min, double max, double value)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }

    public static double AzimuthDifferenceDeg(double a, double b)
    {
        var result = a - b;

        result = ConstrainAnglePlusMinus180(result);

        return result;
    }

    public static double CalculateBankAngleDegFromLateralAcceleration(double lateralAcceleration)
    {
        var lateralAcceleration_g = lateralAcceleration.MetersPerSecondSquaredToG();

        var loadFactor_g = Abs(lateralAcceleration_g) + 1.0;

        var bankAngle_deg = CalculateBankAngleFromLoadFactor(loadFactor_g);

        bankAngle_deg = bankAngle_deg * Sign(lateralAcceleration_g);

        return bankAngle_deg;
    }

    public static double CalculateLoadFactorFromBankAngleDeg(double bankAngleDeg)
    {
        var bankAngle = bankAngleDeg.DegreesToRadians();

        var loadFactor_g = 1 / Cos(bankAngle);

        return loadFactor_g;
    }

    public static double CalculateBankAngleFromLoadFactor(double loadFactor_g)
    {
        var bankAngle = Acos(1 / loadFactor_g);

        var bankAngle_deg = bankAngle.RadiansToDegrees();

        return bankAngle_deg;
    }

    public static double FloorToStepSize(double value, double stepSize)
    {
        Guard.IsGreaterThan(stepSize, 0, nameof(stepSize));

        return Math.Floor(value / stepSize) * stepSize;
    }

    public static double RoundToStepSize(double value, double stepSize)
    {
        Guard.IsGreaterThan(stepSize, 0, nameof(stepSize));

        return Math.Round(value / stepSize) * stepSize;
    }
}