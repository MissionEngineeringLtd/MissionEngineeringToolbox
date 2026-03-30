namespace MissionEngineering.Platform;

public record PlatformStateInitial
{
    public double PositionNorth_m { get; set; }

    public double PositionEast_m { get; set; }

    public double Altitude_m { get; set; }

    public double TotalSpeed_ms { get; set; }

    public double HeadingAngle_deg { get; set; }

    public double PitchAngle_deg { get; set; }
}