namespace MissionEngineering.Simulation;

public interface ISimulationHarness
{
    SimulationHarnessSettings SimulationHarnessSettings { get; set; }

    SimulationRunSettings SimulationRunSettings { get; set; }

    SimulationSettings SimulationSettings { get; set; }

    List<ISimulationEvent> SimulationEvents { get; set; }

    ISimulation Simulation { get; set; }

    void Run();
}