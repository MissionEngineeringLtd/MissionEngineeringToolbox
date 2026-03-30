using System.Drawing;

namespace MissionEngineering.Platform;

public record PlatformHeader
{
    public int PlatformId { get; set; }

    public required string PlatformName { get; set; }

    public required string PlatformCallsign { get; set; }

    public required string PlatformDescription { get; set; }

    public PlatformType PlatformType { get; set; }

    public PlatformAffiliationType PlatformAffiliation { get; set; }

    public PlatformHeader()
    {
        PlatformId = 0;
        PlatformName = "PLATFORM_NAME";
        PlatformCallsign = "PLATFORM_CALLSIGN";
        PlatformDescription = "PLATFORM_DESCRIPTION";
        PlatformType = PlatformType.Undefined;
        PlatformAffiliation = PlatformAffiliationType.Undefined;
    }
}