namespace MissionEngineering.Simulation.Messages;

public record PlatformStateRelativeMessage : SimulationMessage
{
    public int PlatformIdOrigin { get; set; }

    public string PlatformNameOrigin { get; set; }

    public int PlatformIdTarget { get; set; }

    public string PlatformNameTarget { get; set; }

    public double RelativePositionNorth_m { get; set; }

    public double RelativePositionEast_m { get; set; }

    public double RelativePositionDown_m { get; set; }

    public double RelativeVelocityNorth_ms { get; set; }

    public double RelativeVelocityEast_ms { get; set; }

    public double RelativeVelocityDown_ms { get; set; }

    public double RelativePositionLOSX_m { get; set; }

    public double RelativePositionLOSY_m { get; set; }

    public double RelativePositionLOSZ_m { get; set; }

    public double RelativeVelocityLOSX { get; set; }

    public double RelativeVelocityLOSY { get; set; }

    public double RelativeVelocityLOSZ { get; set; }

    public double Range_m { get; set; }

    public double RangeRate_ms { get; set; }

    public double AzimuthAngle_deg { get; set; }

    public double ElevationAngle_deg { get; set; }

    public double AzimuthRate_degs { get; set; }

    public double ElevationRate_degs { get; set; }

    public double AspectAngleAzimuth_deg { get; set; }

    public double AspectAngleElevation_deg { get; set; }

    public PlatformStateRelativeMessage()
    {
        MessageType = SimulationMessageType.PlatformStateRelative;
    }
}
