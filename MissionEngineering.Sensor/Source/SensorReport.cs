using MissionEngineering.Platform;

namespace MissionEngineering.Sensor;

public class SensorReport
{
    public SensorReportHeader SensorReportHeader { get; set; }

    public PlatformState SensorPlatformState { get; set; }

    public SensorState SensorState { get; set; }

    public SensorTargetIdentification TargetIdentification { get; set; }

    public SensorTargetLocation TargetLocation { get; set; }
}