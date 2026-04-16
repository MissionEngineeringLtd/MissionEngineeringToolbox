namespace MissionEngineering.Platform;

public static class PlatformSettingsFactory
{
    public static PlatformSettings PlatformSettings_Aircraft_Friendly_1()
    {
        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader
            {
                PlatformId = 1,
                PlatformName = "AC_1",
                PlatformCallsign = "PLATFORM_CALLSIGN",
                PlatformDescription = "PLATFORM_DESCRIPTION",
                PlatformAffiliation = PlatformAffiliationType.Friendly,
                PlatformType = PlatformType.Aircraft
            },
            PlatformHeaderSimdis = new PlatformHeaderSimdis
            {
                PlatformType = "Aircraft",
                PlatformAffiliationFHN = "F",
                PlatformIcon = "F-35A",
                PlatformColor = "Blue",
                PlatformInterpolate = "1",
                PlatformScaleLevel = 2.5
            },
            PlatformStateInitial = new PlatformStateInitial
            {
                PositionNorth_m = 0.0,
                PositionEast_m = 0.0,
                Altitude_m = 5000.0,
                TotalSpeed_ms = 250.0,
                HeadingAngle_deg = 0.0,
                PitchAngle_deg = 0.0
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
                PlatformId = 2,
                PlatformName = "AC_2",
                PlatformCallsign = "PLATFORM_CALLSIGN",
                PlatformDescription = "PLATFORM_DESCRIPTION",
                PlatformAffiliation = PlatformAffiliationType.Friendly,
                PlatformType = PlatformType.Aircraft,
            },
            PlatformHeaderSimdis = new PlatformHeaderSimdis
            {
                PlatformType = "Aircraft",
                PlatformAffiliationFHN = "F",
                PlatformIcon = "F-35A",
                PlatformColor = "Green",
                PlatformInterpolate = "1",
                PlatformScaleLevel = 2.5
            },

            PlatformStateInitial = new PlatformStateInitial
            {
                PositionNorth_m = 40000.0,
                PositionEast_m = -10000.0,
                Altitude_m = 5000.0,
                TotalSpeed_ms = 250.0,
                HeadingAngle_deg = 180.0,
                PitchAngle_deg = 0.0
            }
        };

        return platformSettings;
    }

    public static PlatformSettings PlatformSettings_Aircraft_Friendly_3()
    {
        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader
            {
                PlatformId = 3,
                PlatformName = "AC_3",
                PlatformCallsign = "PLATFORM_CALLSIGN",
                PlatformDescription = "PLATFORM_DESCRIPTION",
                PlatformAffiliation = PlatformAffiliationType.Friendly,
                PlatformType = PlatformType.Aircraft,
            },
            PlatformHeaderSimdis = new PlatformHeaderSimdis
            {
                PlatformType = "Aircraft",
                PlatformAffiliationFHN = "F",
                PlatformIcon = "F-35A",
                PlatformColor = "Green",
                PlatformInterpolate = "1",
                PlatformScaleLevel = 2.5
            },

            PlatformStateInitial = new PlatformStateInitial
            {
                PositionNorth_m = 50000.0,
                PositionEast_m = -8000.0,
                Altitude_m = 6000.0,
                TotalSpeed_ms = 200.0,
                HeadingAngle_deg = 135.0,
                PitchAngle_deg = 0.0
            }
        };

        return platformSettings;
    }
}