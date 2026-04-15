using MissionEngineering.Math;
using MissionEngineering.Platform;
using static System.Math;

namespace MissionEngineering.Sensor;

public static class SensorFunctions
{
    public static bool IsInSensorCoverage(PlatformStateRelative platformStateRelative, SensorState sensorState)
    {
        var isInRangeCoverage = IsInRangeCoverage(platformStateRelative, sensorState);
        var isInAzimuthCoverage = IsInAzimuthCoverage(platformStateRelative, sensorState);
        var isInElevationCoverage = IsInElevationCoverage(platformStateRelative, sensorState);

        var isInCoverage = isInRangeCoverage && isInAzimuthCoverage && isInElevationCoverage;

        return isInCoverage;
    }

    public static bool IsInRangeCoverage(PlatformStateRelative platformStateRelative, SensorState sensorState)
    {
        var isInRangeCoverage = true;

        return isInRangeCoverage;
    }

    public static bool IsInAzimuthCoverage(PlatformStateRelative platformStateRelative, SensorState sensorState)
    {
        var azimuthBeamwidth_deg = 3.0;

        var azimuthDifference_deg = MathFunctions.AzimuthDifferenceDeg(platformStateRelative.RelativePolarsNED.AzimuthAngle_deg, sensorState.PointingAzimuthNorth_deg);

        var isInAzimuthCoverage = Abs(azimuthDifference_deg) <= azimuthBeamwidth_deg;

        return isInAzimuthCoverage;
    }

    public static bool IsInElevationCoverage(PlatformStateRelative platformStateRelative, SensorState sensorState)
    {
        var isInElevationCoverage = true;

        return isInElevationCoverage;
    }
}