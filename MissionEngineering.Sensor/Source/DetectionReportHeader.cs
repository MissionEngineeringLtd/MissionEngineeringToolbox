namespace MissionEngineering.Sensor;

public class DetectionReportHeader
{
    public DateTime DateTime { get; set; }

    public int Time_s { get; set; }
 
    public int DetectionReportId { get; set; }

    public int PlatformId { get; set; }

    public int SensorId { get; set; }

    public SensorType SensorType {  get; set; }
}