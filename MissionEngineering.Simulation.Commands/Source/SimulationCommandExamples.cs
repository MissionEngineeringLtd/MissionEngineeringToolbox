namespace MissionEngineering.Simulation;

public static class SimulationCommandExamples
{
    public static List<ISimulationCommand> Example_1()
    {
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
            PlatformName = "Platform_1",
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
            PlatformName = "Platform_2",
            PositionNorth_m = 2000.0,
            PositionEast_m = 1000.0,
            Altitude_m = 100.0,
            TotalSpeed_ms = 50.0,
            HeadingAngle_deg = 90.0,
            PitchAngle_deg = 5.0
        };

        return new List<ISimulationCommand> { c1, c2, c3 };
    }
}