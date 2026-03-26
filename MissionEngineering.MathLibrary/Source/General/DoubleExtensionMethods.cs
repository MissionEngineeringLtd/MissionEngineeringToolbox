using static MissionEngineering.MathLibrary.PhysicalConstants;
using static MissionEngineering.MathLibrary.UnitConversions;
using static System.Math;

namespace MissionEngineering.MathLibrary;

public static class DoubleExtensionMethods
{
    public static double PowerToDecibels(this double x)
    {
        var result = 10.0 * Log10(x);

        return result;
    }

    public static double DecibelsToPower(this double x)
    {
        var result = Pow(10.0, x / 10.0);

        return result;
    }

    public static double RadiansToDegrees(this double x)
    {
        var result = x * RadianToDegrees;

        return result;
    }

    public static double DegreesToRadians(this double x)
    {
        var result = x * DegreesToRadian;

        return result;
    }

    public static double MetersToFeet(this double x)
    {
        var result = x * MeterToFoot;

        return result;
    }

    public static double FeetToMeters(this double x)
    {
        var result = x * FootToMeter;

        return result;
    }

    public static double MetersToKilometers(this double x)
    {
        var result = x * MeterToKilometer;

        return result;
    }

    public static double KilometersToMeters(this double x)
    {
        var result = x * KilometerToMeter;

        return result;
    }

    public static double MetersToNauticalMiles(this double x)
    {
        var result = x * MeterToNauticalMile;

        return result;
    }

    public static double NauticalMilesToMeters(this double x)
    {
        var result = x * NauticalMileToMeter;

        return result;
    }

    public static double MetersPerSecondToKnots(this double x)
    {
        var result = x * MeterPerSecondToKnot;

        return result;
    }

    public static double KnotsToMetersPerSecond(this double x)
    {
        var result = x * KnotToMeterPerSecond;

        return result;
    }

    public static double FrequencyToWavelength(this double x)
    {
        var result = SpeedOfLight / x;

        return result;
    }

    public static double WavelengthToFrequency(this double x)
    {
        var result = SpeedOfLight / x;

        return result;
    }

    public static double MetersPerSecondSquaredToG(this double x)
    {
        var result = x * MeterPerSecondSquaredToG;

        return result;
    }

    public static double GToMetersPerSecondSquared(this double x)
    {
        var result = x * GToMeterPerSecondSquared;

        return result;
    }

    public static double RpmToDegrees(this double x)
    {
        var result = x * UnitConversions.RpmToDegrees;

        return result;
    }

    public static double DegreesToRpm(this double x)
    {
        var result = x * UnitConversions.DegreesToRpm;

        return result;
    }

    public static double RadiansToRpm(this double angleRate)
    {
        var angleRateRpm = angleRate.RadiansToDegrees().DegreesToRpm();

        return angleRateRpm;
    }

    public static double RpmToRadians(this double angleRateRpm)
    {
        var angleRateRad = angleRateRpm.RpmToDegrees().DegreesToRadians();

        return angleRateRad;
    }

    public static double SecondsToMilliseconds(this double x)
    {
        var result = x * 1.0e3;

        return result;
    }

    public static double MillisecondsToSeconds(this double x)
    {
        var result = x / 1.0e3;

        return result;
    }

    public static double SecondsToMicroseconds(this double x)
    {
        var result = x * 1.0e6;

        return result;
    }

    public static double MicrosecondsToSeconds(this double x)
    {
        var result = x / 1.0e6;

        return result;
    }

    public static double SecondsToNanoseconds(this double x)
    {
        var result = x * 1.0e9;

        return result;
    }

    public static double NanosecondsToSeconds(this double x)
    {
        var result = x / 1.0e9;

        return result;
    }

    public static double ConstrainAngle0To2PI(this double x)
    {
        var result = MathFunctions.ConstrainAngle0To2PI(x);

        return result;
    }

    public static double ConstrainAnglePlusMinusPI(this double x)
    {
        var result = MathFunctions.ConstrainAnglePlusMinusPI(x);

        return result;
    }

    public static double ConstrainAngle0To360(this double x)
    {
        var result = MathFunctions.ConstrainAngle0To360(x);

        return result;
    }

    public static double ConstrainAnglePlusMinus180(this double x)
    {
        var result = MathFunctions.ConstrainAnglePlusMinus180(x);

        return result;
    }
}