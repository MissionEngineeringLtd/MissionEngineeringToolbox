namespace MissionEngineering.Sensor;

public interface ISensor
{
    List<SensorReport> GenerateSensorReports(double time_s);
}