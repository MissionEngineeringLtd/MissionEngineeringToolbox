using System.Drawing;

namespace MissionEngineering.Platform;

public record PlatformHeaderSimdis
{
    public string PlatformType { get; set; }

    public string PlatformAffiliationFHN { get; set; }

    public required string PlatformIcon { get; set; }

    public string PlatformColor { get; set; }

    public string PlatformInterpolate { get; init; }

    public double PlatformScaleLevel { get; init; }

    public PlatformHeaderSimdis()
    {
        PlatformType = "UNKNOWN";
        PlatformAffiliationFHN = "F";
        PlatformIcon = "PLATFORM_ICON";
        PlatformColor = "Blue";
        PlatformInterpolate = "1";
        PlatformScaleLevel = 4.0;
    }
}