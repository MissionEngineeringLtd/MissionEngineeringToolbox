using MissionEngineering.Math;

namespace MissionEngineering.Simulation;

public class PlatformAutopilotCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public string PlatformName { get; set; }

    public double HeadingAngleDemand_deg { get; set; }

    public double AltitudeDemand_m { get; set; }

    public double AltitudeDemand_ft { get => AltitudeDemand_m.MetersToFeet(); set => AltitudeDemand_m = value.FeetToMeters(); }

    public double AltitudeDemand_FL { get => AltitudeDemand_ft.FeetToFlightLevel(); set => AltitudeDemand_ft = value.FlightLevelToFeet(); }

    public double TotalSpeedDemand_ms { get; set; }

    public PlatformAutopilotCommand()
    {
        CommandType = SimulationCommandType.PlatformAutopilot;
    }
}