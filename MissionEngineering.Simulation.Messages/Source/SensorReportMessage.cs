namespace MissionEngineering.Simulation.Messages;

public record SensorReportMessage : SimulationMessage
{
    public int SensorPlatformId { get; set; }

    public int TargetPlatformId { get; set; }

    public int SensorId { get; set; }

    public string TargetLocationType { get; set; }

    public bool IsPositionLLAValid { get; set; }

    public bool IsPositionNEDValid { get; set; }

    public bool IsVelocityNEDValid { get; set; }

    public bool IsRangeValid { get; set; }

    public bool IsRangeRateValid { get; set; }

    public bool IsAzimuthValid { get; set; }

    public bool IsElevationValid { get; set; }

    public bool IsAltitudeValid { get; set; }

    public double TargetLatitude_deg { get; set; }

    public double TargetLongitude_deg { get; set; }

    public double TargetAltitude_m { get; set; }

    public double TargetPositionNorth_m { get; set; }

    public double TargetPositionEast_m { get; set; }

    public double TargetPositionDown_m { get; set; }

    public double TargetVelocityNorth_ms { get; set; }

    public double TargetVelocityEast_ms { get; set; }

    public double TargetVelocityDown_ms { get; set; }

    public double TargetRange_m { get; set; }

    public double TargetRangeRate_ms { get; set; }

    public double TargetAzimuthAngle_deg { get; set; }

    public double TargetElevationAngle_deg { get; set; }

    public SensorReportMessage()
    {
        MessageType = SimulationMessageType.SensorReport;
    }
}