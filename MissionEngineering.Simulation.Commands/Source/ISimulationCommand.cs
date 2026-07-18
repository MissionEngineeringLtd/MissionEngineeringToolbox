namespace MissionEngineering.Simulation;

public interface ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }
}