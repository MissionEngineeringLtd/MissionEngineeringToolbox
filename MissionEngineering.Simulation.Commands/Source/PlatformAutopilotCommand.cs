namespace MissionEngineering.Simulation;

public class PlatformAutopilotCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public string PlatformName { get; set; }

    public double HeadingAngleDemand_deg { get; set; }

    public double AltitudeDemand_m { get; set; }

    public double TotalSpeedDemand_ms { get; set; }

    public PlatformAutopilotCommand()
    {
        CommandType = SimulationCommandType.PlatformAutopilot;
    }
}