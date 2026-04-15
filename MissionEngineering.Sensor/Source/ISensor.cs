using MissionEngineering.Core;

namespace MissionEngineering.Sensor;

public interface ISensor : IExecutableModel
{
    List<SensorReport> SensorReports { get; set; }

    void GenerateSensorReports(double time_s);
}