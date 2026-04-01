using MissionEngineering.Platform;

namespace MissionEngineering.Sensor;

public class DetectionReport
{
    public DetectionReportHeader DetectionReportHeader { get; set; }

    public PlatformState PlatformState { get; set; }

    public SensorState SensorState { get; set; }

    public DetectionIdentification Identification { get; set; }

    public DetectionLocation Location { get; set; }
}