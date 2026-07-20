using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public static class SimulationCommandExamples
{
    public static List<ISimulationCommand> Example_1()
    {
        var c0 = new SimulationSettingsCommand()
        {
            CommandTime = 0.0,
            SimulationStartDateTime = "2024-11-21T21:20:30Z",
            SimulationStartTime_s = 10.0,
            SimulationEndTime_s = 100.0,
            SimulationTimeStep_s = 0.1
        };

        var c1 = new MapOriginCommand()
        {
            CommandTime = 0.0,
            Latitude_deg = 56.0,
            Longitude_deg = 12.5,
        };

        var c2 = new PlatformCreateCommand()
        {
            CommandTime = 10.0,
            PlatformId = 1,
            PlatformName = "FF_1",
            PlatformCallsign = "FF_1",
            PlatformDescription = "FF_1 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "F-35A",
            PlatformColor = "Blue",
            PositionNorth_m = 1000.0,
            PositionEast_m = 500.0,
            Altitude_m = 100.0,
            TotalSpeed_ms = 50.0,
            HeadingAngle_deg = 90.0,
            PitchAngle_deg = 5.0
        };

        var c3 = new PlatformCreateCommand()
        {
            CommandTime = 10.0,
            PlatformId = 2,
            PlatformName = "FF_2",
            PlatformCallsign = "FF_2",
            PlatformDescription = "FF_2 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "F-35A",
            PlatformColor = "Blue",
            PositionNorth_m = 2000.0,
            PositionEast_m = 1000.0,
            Altitude_m = 100.0,
            TotalSpeed_ms = 50.0,
            HeadingAngle_deg = 90.0,
            PitchAngle_deg = 5.0
        };

        return new List<ISimulationCommand> { c0, c1, c2, c3 };
    }
}