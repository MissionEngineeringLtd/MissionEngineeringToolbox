namespace MissionEngineering.Platform;

public class PlatformFlightpathDemand
{
    public double PlatformId { get; set; }

    public string PlatformName { get; set; }

    public double Time_s { get; set; }

    public double HeadingAngleDemand_deg { get; set; }

    public double TotalSpeedDemand_ms { get; set; }

    public double AltitudeDemand_m { get; set; }
}