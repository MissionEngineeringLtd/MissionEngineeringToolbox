using MissionEngineering.Math;

namespace MissionEngineering.Sensor;

public class DetectionLocation
{
    public DetectionLocationType LocationType { get; set; }

    public bool IsPositionLLAValid { get; set; }

    public bool IsPositionNEDValid { get; set; }

    public bool IsVelocityNEDValid { get; set; }

    public bool IsRangeValid { get; set; }

    public bool IsAzimuthValid { get; set; }

    public bool IsElevationValid { get; set; }

    public bool IsAltitudeValid { get; set; }

    public PositionLLA PositionLLA { get; set; }

    public PositionNED PositionNED { get; set; }

    public VelocityNED VelocityNED { get; set; }

    public double Range_m { get; set; }

    public double RangeRate_m { get; set; }

    public double Azimuth_deg { get; set; }

    public double Elevation_deg { get; set; }
}