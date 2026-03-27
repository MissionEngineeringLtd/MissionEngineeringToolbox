using MissionEngineering.Core;

namespace MissionEngineering.Simulation;

public class SimulationHarness : ISimulationHarness
{
    public SimulationHarnessSettings SimulationHarnessSettings { get; set; }

    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public ISimulation Simulation { get; set; }

    public SimulationHarness(SimulationHarnessSettings simulationHarnessSettings, SimulationSettings simulationSettings, ScenarioSettings scenarioSettings, ISimulation simulation)
    {
        SimulationHarnessSettings = simulationHarnessSettings;
        SimulationSettings = simulationSettings;
        ScenarioSettings = scenarioSettings;
        Simulation = simulation;
    }

    public void Run()
    {
        for (int i = 0; i < SimulationHarnessSettings.NumberOfRuns; i++)
        {
            var runNumber = i + 1;

            RunSingle(runNumber);
        }
    }

    public void RunSingle(int runNumber)
    {
        Simulation = SimulationBuilder.CreateSimulation();

        Simulation.SimulationSettings = SimulationSettings;
        Simulation.ScenarioSettings = ScenarioSettings;

        Simulation.SimulationSettings.RunNumber = runNumber;

        Simulation.DataRecorder.SimulationData.SimulationSettings = SimulationSettings;

        LogUtilities.CreateLogger(SimulationSettings.LogFileName);

        Simulation.Run();
    }
}