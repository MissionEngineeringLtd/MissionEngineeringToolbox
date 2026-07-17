namespace MissionEngineering.Simulation;

public class SimulationCommand
{
    public double CommandTime { get; set; }

    public SimulationCommandType CommandType { get; set; }

    public string CommandTypeString => CommandType.ToString();

    public SimulationCommandData CommandData { get; set; }
}