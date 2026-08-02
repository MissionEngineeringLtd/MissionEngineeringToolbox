namespace MissionEngineering.Simulation;

public static class SimulationRunSettingsFactory
{
    public static SimulationRunSettings SingleRun(string inputFolderBase, string simulationName, string outputFolderBase)
    {
        var simulationSettings = new SimulationRunSettings()
        {
            InputFolderBase = inputFolderBase,
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

    public static SimulationRunSettings MultipleRuns(string inputFolderBase, string simulationName, string outputFolderBase)
    {
        var simulationSettings = SingleRun(inputFolderBase, simulationName, outputFolderBase);

        simulationSettings.IsAddConsoleLogging = false;
        simulationSettings.IsAddFileLogging = true;
        simulationSettings.IsWriteData = true;
        simulationSettings.IsAddTimeStamp = true;
        simulationSettings.IsAddRunNumber = true;
        simulationSettings.IsCreateZipFile = true;

        return simulationSettings;
    }
}