using MissionEngineering.Math;
using MissionEngineering.Platform;
using MissionEngineering.Sensor;

namespace MissionEngineering.Simulation;

public static class ScenarioSettingsFactory
{
    public static ScenarioSettings ScenarioSettings_Test_1()
    {
        var dateTimeOrigin = new DateTime(2024, 12, 24, 15, 45, 10, 123);

        var scenarioSettings = new ScenarioSettings()
        {
            SimulationName = "Scenario_Test_1",
            DateTimeOrigin = dateTimeOrigin.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            TimeStart_s = 10.0,
            TimeEnd_s = 200.0,
            TimeStep_s = 0.01,
            TrackPredictionTimeStep_s = 0.1,
            Latitude_deg = 55.1,
            Longitude_deg = 12.0
        };

        return scenarioSettings;
    }

    public static ScenarioSettings ScenarioSettings_FF_1()
    {
        var dateTimeOrigin = new DateTime(2024, 12, 24, 15, 45, 10, 123);

        var scenarioSettings = new ScenarioSettings()
        {
            SimulationName = "Simulation_FF_1",
            DateTimeOrigin = dateTimeOrigin.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            TimeStart_s = 10.0,
            TimeEnd_s = 200.0,
            TimeStep_s = 0.01,
            TrackPredictionTimeStep_s = 0.1,
            Latitude_deg = 64.5,
            Longitude_deg = 9.0
        };

        return scenarioSettings;
    }
}