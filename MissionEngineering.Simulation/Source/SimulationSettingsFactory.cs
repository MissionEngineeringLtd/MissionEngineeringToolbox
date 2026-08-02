namespace MissionEngineering.Simulation;

public static class SimulationSettingsFactory
{
    public static SimulationSettings SimulationSettings_Test_1()
    {
        var dateTimeOrigin = new DateTime(2024, 12, 24, 15, 45, 10, 123);

        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_Test_1",
            DateTimeOrigin = dateTimeOrigin.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            TimeStart_s = 10.0,
            TimeEnd_s = 200.0,
            TimeStep_s = 0.01,
            PlatformDataRecordTimeStep_s = 0.1,
            TrackPredictionTimeStep_s = 0.1,
            Latitude_deg = 55.1,
            Longitude_deg = 12.0
        };

        return simulationSettings;
    }

    public static SimulationSettings SimulationSettings_FF_1()
    {
        var dateTimeOrigin = new DateTime(2024, 12, 24, 15, 45, 10, 123);

        var simulationSettings = new SimulationSettings()
        {
            SimulationName = "Simulation_FF_1",
            DateTimeOrigin = dateTimeOrigin.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            TimeStart_s = 10.0,
            TimeEnd_s = 200.0,
            TimeStep_s = 0.01,
            PlatformDataRecordTimeStep_s = 0.1,
            TrackPredictionTimeStep_s = 0.1,
            Latitude_deg = 64.5,
            Longitude_deg = 9.0
        };

        return simulationSettings;
    }
}