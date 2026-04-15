namespace MissionEngineering.Sensor;

public record SensorState
{
    public double Time_s { get; set; }

    public string SensorMode { get; set; }

    public double PointingAzimuthBody_deg { get; set; }

    public double PointingElevationBody_deg { get; set; }

    public double PointingAzimuthNorth_deg { get; set; }

    public double PointingElevationNorth_deg { get; set; }
}