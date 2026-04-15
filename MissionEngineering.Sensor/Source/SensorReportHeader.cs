using MissionEngineering.Core;

namespace MissionEngineering.Sensor;

public class SensorReportHeader
{
    public SimulationTimeStamp TimeStamp { get; set; }

    public int SensorPlatformId { get; set; }

    public string SensorPlatformName { get; set; }

    public int TargetPlatformId { get; set; }

    public string TargetPlatformName { get; set; }

    public int SensorId { get; set; }

    public string SensorName { get; set; }

    public SensorType SensorType { get; set; }

    public int SensorReportId { get; set; }
}