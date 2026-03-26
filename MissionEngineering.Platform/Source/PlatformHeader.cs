using System.Drawing;

namespace MissionEngineering.Platform;

public record PlatformHeader
{
    public int PlatformId { get; set; }

    public required string PlatformName { get; set; }

    public required string PlatformDescription { get; set; }

    public required string PlatformCallsign { get; set; }

    public PlatformType PlatformType { get; set; }

    public PlatformAffiliationType PlatformAffiliation { get; set; }

    public required string PlatformIcon { get; set; }

    public Color PlatformColor { get; set; }
}