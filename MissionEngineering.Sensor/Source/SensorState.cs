namespace MissionEngineering.Sensor;

public class SensorState
{
    public double Time_s { get; set; }

    public string SensorMode { get; set; }

    public double PointingAzimuth_deg { get; set; }

    public double PointingElevation_deg { get; set; }
}