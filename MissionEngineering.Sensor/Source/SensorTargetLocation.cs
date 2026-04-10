using MissionEngineering.Math;

namespace MissionEngineering.Sensor;

public class SensorTargetLocation
{
    public SensorTargetLocationType TargetLocationType { get; set; }

    public bool IsPositionLLAValid { get; set; }

    public bool IsPositionNEDValid { get; set; }

    public bool IsVelocityNEDValid { get; set; }

    public bool IsRangeValid { get; set; }

    public bool IsRangeRateValid { get; set; }

    public bool IsAzimuthValid { get; set; }

    public bool IsElevationValid { get; set; }

    public bool IsAltitudeValid { get; set; }

    public PositionLLA PositionLLA { get; set; }

    public PositionNED PositionNED { get; set; }

    public VelocityNED VelocityNED { get; set; }

    public double Range_m { get; set; }

    public double RangeRate_ms { get; set; }

    public double AzimuthAngle_deg { get; set; }

    public double ElevationAngle_deg { get; set; }
}