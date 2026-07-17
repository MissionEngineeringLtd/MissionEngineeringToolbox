namespace MissionEngineering.Simulation;

public static class SimulationCommandExamples
{
    public static List<SimulationCommand> Example_1()
    {
        var command1 = new PlatformCreateCommand()
        {
            CommandTime = 10.0,
            CommandData = new PlatformCreateCommandData()
            {
                PlatformId = 1,
                PlatformName = "Platform_1",
                PositionNorth_m = 1000.0,
                PositionEast_m = 500.0,
                Altitude_m = 100.0,
                TotalSpeed_ms = 50.0,
                HeadingAngle_deg = 90.0,
                PitchAngle_deg = 5.0
            }
        };

        var command2 = new PlatformCreateCommand()
        {
            CommandTime = 10.0,
            CommandData = new PlatformCreateCommandData()
            {
                PlatformId = 2,
                PlatformName = "Platform_2",
                PositionNorth_m = 2000.0,
                PositionEast_m = 1000.0,
                Altitude_m = 100.0,
                TotalSpeed_ms = 50.0,
                HeadingAngle_deg = 90.0,
                PitchAngle_deg = 5.0
            }
        };

        return new List<SimulationCommand> { command1, command2 };
    }
}