using MissionEngineering.Core;

namespace MissionEngineering.Sensor;

public class SensorReportHeader
{
    public SimulationTimeStamp TimeStamp { get; set; }
 
    public int SensorReportId { get; set; }

    public int SensorPlatformId { get; set; }

    public int TargetPlatformId { get; set; }

    public int SensorId { get; set; }

    public SensorType SensorType {  get; set; }
}