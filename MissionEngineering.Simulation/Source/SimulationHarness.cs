using MissionEngineering.Core;

namespace MissionEngineering.Simulation;

public class SimulationHarness : ISimulationHarness
{
    public SimulationHarnessSettings SimulationHarnessSettings { get; set; }

    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public ISimulation Simulation { get; set; }

    public List<ISimulation> SimulationList { get; set; }

    public SimulationHarness()
    {
    }

    public SimulationHarness(SimulationHarnessSettings simulationHarnessSettings, ScenarioSettings scenarioSettings)
    {
        SimulationHarnessSettings = simulationHarnessSettings;
        ScenarioSettings = scenarioSettings;
    }

    public void Run()
    {
        var numberOfRuns = SimulationHarnessSettings.NumberOfRuns;

        SimulationList = new List<ISimulation>(numberOfRuns);

        var isRunParallel = numberOfRuns > 1;

        if (isRunParallel)
        {
            RunParallel();
        }
        else
        {
            RunSequential();
        }
    }

    public void RunSequential()
    {
        for (int i = 0; i < SimulationHarnessSettings.NumberOfRuns; i++)
        {
            var runNumber = i + 1;

            var simulation = RunSingle(runNumber);
        
            SimulationList.Add(simulation);
        }
    }

    public void RunParallel()
    {
        var numberOfRuns = SimulationHarnessSettings.NumberOfRuns;

        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        Parallel.For(0, numberOfRuns, parallelOptions, i =>
        {
            var runNumber = i + 1;

            var simulation = RunSingle(runNumber);

            SimulationList.Add(simulation);
        });
    }

    public ISimulation RunSingle(int runNumber)
    {
        var simulation = SimulationBuilder.CreateSimulation();

        var simulationSettings = SimulationSettings with { RunNumber = runNumber };

        simulation.SimulationSettings = simulationSettings;
        simulation.ScenarioSettings = ScenarioSettings;

        simulation.DataRecorder.SimulationData.SimulationSettings = simulationSettings;

        simulation.Run();

        Simulation = simulation;

        return simulation;
    }
}