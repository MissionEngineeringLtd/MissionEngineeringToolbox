namespace MissionEngineering.Simulation;

public static class SimulationSettingsFactory
{
    public static SimulationSettings SimulationSettings_Test_1_Single()
    {
        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_1",
            RunNumber = 1,
            DateTime = DateTime.Now,
            IsWriteData = true,
            IsAddTimeStamp = false,
            IsAddRunNumber = false,
            IsCreateZipFile = false,
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
            DateTime = DateTime.Now,
            IsWriteData = true,
            IsAddTimeStamp = true,
            IsAddRunNumber = true,
            IsCreateZipFile = true,
            OutputFolderBase = @"C:\Temp\MissionEngineeringToolbox\"
        };

        return simulationSettings;
    }
}