namespace MissionEngineering.Simulation;

public static class SimulationSettingsFactory
{
    public static SimulationSettings SimulationSettings_Test_1_Single()
    {
        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_1",
            RunNumber = 1,
            RunTimeStamp = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss"),
            IsAddConsoleLogging = true,
            IsAddFileLogging = false,
            IsWriteData = false,
            IsAddTimeStamp = false,
            IsAddRunNumber = true,
            IsCreateZipFile = true,
            OutputFolderBase = @"C:\Temp\MissionEngineeringToolbox\"
        };

        return simulationSettings;
    }

    public static SimulationSettings SimulationSettings_Test_1_Multiple()
    {
        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_1",
            RunNumber = 1,
            RunTimeStamp = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss"),
            IsAddConsoleLogging = false,
            IsAddFileLogging = true,
            IsWriteData = true,
            IsAddTimeStamp = true,
            IsAddRunNumber = true,
            IsCreateZipFile = true,
            OutputFolderBase = @"C:\Temp\MissionEngineeringToolbox\"
        };

        return simulationSettings;
    }
}