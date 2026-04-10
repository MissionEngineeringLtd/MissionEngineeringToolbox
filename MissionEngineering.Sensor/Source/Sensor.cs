namespace MissionEngineering.Sensor;

public abstract class Sensor : ISensor
{
    public abstract List<SensorReport> GenerateSensorReports(double time_s);
}