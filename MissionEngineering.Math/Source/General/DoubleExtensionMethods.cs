using static MissionEngineering.Math.PhysicalConstants;
using static MissionEngineering.Math.UnitConversions;
using static System.Math;

namespace MissionEngineering.Math;

public static class DoubleExtensionMethods
{
    extension(double x)
    {
        public double PowerToDecibels()
        {
            var result = 10.0 * Log10(x);

            return result;
        }

        public double DecibelsToPower()
        {
            var result = Pow(10.0, x / 10.0);

            return result;
        }

        public double PowerToDecibelsm()
        {
            var result = x.PowerToDecibels().DecibelsToDecibelsm();

            return result;
        }

        public double DecibelsmToPower()
        {
            var result = x.DecibelsmToDecibels().DecibelsToPower();

            return result;
        }

        public double DecibelsToDecibelsm()
        {
            var result = x + 30.0;

            return result;
        }

        public double DecibelsmToDecibels()
        {
            var result = x - 30.0;

            return result;
        }

        public double RadiansToDegrees()
        {
            var result = x * RadianToDegrees;

            return result;
        }

        public double DegreesToRadians()
        {
            var result = x * DegreesToRadian;

            return result;
        }

        public double MetersToFeet()
        {
            var result = x * MeterToFoot;

            return result;
        }

        public double FeetToMeters()
        {
            var result = x * FootToMeter;

            return result;
        }

        public double MetersToKilometers()
        {
            var result = x * MeterToKilometer;

            return result;
        }

        public double KilometersToMeters()
        {
            var result = x * KilometerToMeter;

            return result;
        }

        public double MetersToNauticalMiles()
        {
            var result = x * MeterToNauticalMile;

            return result;
        }

        public double NauticalMilesToMeters()
        {
            var result = x * NauticalMileToMeter;

            return result;
        }

        public double MetersPerSecondToKnots()
        {
            var result = x * MeterPerSecondToKnot;

            return result;
        }

        public double KnotsToMetersPerSecond()
        {
            var result = x * KnotToMeterPerSecond;

            return result;
        }

        public double FrequencyToWavelength()
        {
            var result = SpeedOfLight / x;

            return result;
        }

        public double WavelengthToFrequency()
        {
            var result = SpeedOfLight / x;

            return result;
        }

        public double MetersPerSecondSquaredToG()
        {
            var result = x * MeterPerSecondSquaredToG;

            return result;
        }

        public double GToMetersPerSecondSquared()
        {
            var result = x * GToMeterPerSecondSquared;

            return result;
        }

        public double RpmToDegrees()
        {
            var result = x * UnitConversions.RpmToDegrees;

            return result;
        }

        public double DegreesToRpm()
        {
            var result = x * UnitConversions.DegreesToRpm;

            return result;
        }

        public double RadiansToRpm()
        {
            var result = x.RadiansToDegrees().DegreesToRpm();

            return result;
        }

        public double RpmToRadians()
        {
            var result = x.RpmToDegrees().DegreesToRadians();

            return result;
        }

        public double SecondsToMilliseconds()
        {
            var result = x * 1.0e3;

            return result;
        }

        public double MillisecondsToSeconds()
        {
            var result = x / 1.0e3;

            return result;
        }

        public double SecondsToMicroseconds()
        {
            var result = x * 1.0e6;

            return result;
        }

        public double MicrosecondsToSeconds()
        {
            var result = x / 1.0e6;

            return result;
        }

        public double SecondsToNanoseconds()
        {
            var result = x * 1.0e9;

            return result;
        }

        public double NanosecondsToSeconds()
        {
            var result = x / 1.0e9;

            return result;
        }

        public double ConstrainAngle0To2PI()
        {
            var result = MathFunctions.ConstrainAngle0To2PI(x);

            return result;
        }

        public double ConstrainAnglePlusMinusPI()
        {
            var result = MathFunctions.ConstrainAnglePlusMinusPI(x);

            return result;
        }

        public double ConstrainAngle0To360()
        {
            var result = MathFunctions.ConstrainAngle0To360(x);

            return result;
        }

        public double ConstrainAnglePlusMinus180()
        {
            var result = MathFunctions.ConstrainAnglePlusMinus180(x);

            return result;
        }
    }
}