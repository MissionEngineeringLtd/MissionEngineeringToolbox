namespace MissionEngineering.Simulation;

public static class SimulationSettingsFactory
{
    public static SimulationSettings SimulationSettings_Test_1_Single()
    {
        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_1",
            RunNumber = 1,
            RunTimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss"),
            IsAddConsoleLogging = true,
            IsAddFileLogging = true,
            IsWriteData = true,
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

    public static SimulationSettings SimulationSettings_FF_1_Single()
    {
        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_FF_1",
            RunNumber = 1,
            RunTimeStamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss"),
            IsAddConsoleLogging = true,
            IsAddFileLogging = true,
            IsWriteData = true,
            IsAddTimeStamp = false,
            IsAddRunNumber = true,
            IsCreateZipFile = true,
            OutputFolderBase = @"C:\Temp\MissionEngineeringToolbox\"
        };

        return simulationSettings;
    }
}