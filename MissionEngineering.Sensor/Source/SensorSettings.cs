using MissionEngineering.Scanner;

namespace MissionEngineering.Sensor;

public class SensorSettings
{
    public int PlatformId { get; set; }

    public int SensorId { get; set; }

    public string SensorName { get; set; }

    public SensorType SensorType { get; set; }

    public ScanSettings ScanSettings { get; set; }
}