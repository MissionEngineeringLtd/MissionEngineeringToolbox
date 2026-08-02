namespace MissionEngineering.Simulation;

public static class SimulationSettingsFactory
{
    public static SimulationSettings SimulationSettings_Single(string simulationName, string outputFolderBase)
    {
        var simulationSettings = new SimulationSettings()
        {
            SimulationName = simulationName,
            RunNumber = 1,
            RunTimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss"),
            IsAddConsoleLogging = true,
            IsAddFileLogging = true,
            IsWriteData = true,
            IsAddTimeStamp = false,
            IsAddRunNumber = true,
            IsCreateZipFile = true,
            OutputFolderBase = outputFolderBase,
        };

        return simulationSettings;
    }

    public static SimulationSettings SimulationSettings_Multiple(string simulationName, string outputFolderBase)
    {
        var simulationSettings = SimulationSettings_Single(simulationName, outputFolderBase);

        simulationSettings.IsAddConsoleLogging = false;
        simulationSettings.IsAddFileLogging = true;
        simulationSettings.IsWriteData = true;
        simulationSettings.IsAddTimeStamp = true;
        simulationSettings.IsAddRunNumber = true;
        simulationSettings.IsCreateZipFile = true;

        return simulationSettings;
    }
}