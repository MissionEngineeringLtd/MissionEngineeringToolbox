using MissionEngineering.MathLibrary;
using System.Drawing;

namespace MissionEngineering.Platform;

public static class PlatformSettingsFactory
{
    public static PlatformSettings PlatformSettings_Aircraft_Friendly_1()
    {
        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader
            {
                PlatformId = 0,
                PlatformName = "AC_1",
                PlatformCallsign = "PLATFORM_CALLSIGN",
                PlatformDescription = "PLATFORM_DESCRIPTION",
                PlatformAffiliation = PlatformAffiliationType.Friendly,
                PlatformType = PlatformType.Aircraft,
                PlatformIcon = "F-35A",
                PlatformColor = Color.Blue,
                PlatformInterpolate = true,
                PlatformScaleLevel = 2.5
            },
            PlatformState = new PlatformState
            {
                PositionNED = new PositionNED { PositionNorth_m = 1000.0, PositionEast_m = 2000.0, PositionDown_m = -2000.0 },
                VelocityNED = new VelocityNED { VelocityNorth_ms = 100.0, VelocityEast_ms = 200.0, VelocityDown_ms = 0.0 },
            }
        };

        return platformSettings;
    }

    public static PlatformSettings PlatformSettings_Aircraft_Friendly_2()
    {
        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader
            {
                PlatformId = 0,
                PlatformName = "AC_2",
                PlatformCallsign = "PLATFORM_CALLSIGN",
                PlatformDescription = "PLATFORM_DESCRIPTION",
                PlatformAffiliation = PlatformAffiliationType.Friendly,
                PlatformType = PlatformType.Aircraft,
                PlatformIcon = "F-35A",
                PlatformColor = Color.Green,
                PlatformInterpolate = true,
                PlatformScaleLevel = 2.5
            },
            PlatformState = new PlatformState
            {
                PositionNED = new PositionNED { PositionNorth_m = 10000.0, PositionEast_m = 20000.0, PositionDown_m = -5000.0 },
                VelocityNED = new VelocityNED { VelocityNorth_ms = -200.0, VelocityEast_ms = -100.0, VelocityDown_ms = 0.0 },
            }
        };

        return platformSettings;
    }
}