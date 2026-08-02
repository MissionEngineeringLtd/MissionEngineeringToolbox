using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using MissionEngineering.Core;

namespace MissionEngineering.Simulation;

public class SimulationSampleDataManager
{
    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public List<ISimulationEvent> SimulationEvents { get; set; }

    public string SamplesFolder { get; set; }

    public void WriteSampleData()
    {
        var simulationName = SimulationSettings.SimulationName;

        var sampleFolder = Path.Combine(SamplesFolder, simulationName);

        Console.WriteLine("    Writing Sample Data: " + simulationName);

        if (!Directory.Exists(sampleFolder))
        {
            Directory.CreateDirectory(sampleFolder);
        }

        var simulationSettingsFile = Path.Combine(sampleFolder, $"{simulationName}_SimulationSettings.yaml");

        Console.WriteLine("        Writing: " + simulationSettingsFile);

        SimulationSettings.WriteToYamlFile(simulationSettingsFile);

        var scenarioSettingsFile = Path.Combine(sampleFolder, $"{simulationName}_ScenarioSettings.yaml");

        Console.WriteLine("        Writing: " + scenarioSettingsFile);

        ScenarioSettings.WriteToYamlFile(scenarioSettingsFile);

        var simulationEventsFile = Path.Combine(sampleFolder, $"{simulationName}_SimulationEvents_All.yaml");

        SimulationEvents.WriteToYamlFile(simulationEventsFile);

        Console.WriteLine("        Writing: " + simulationEventsFile);

        Console.WriteLine("    Done.");
        Console.WriteLine();
    }
}
