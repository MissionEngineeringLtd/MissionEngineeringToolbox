
using MissionEngineering.Scanner;

namespace MissionEngineering.Sensor;

public static class SensorSettingsFactory
{
    public static SensorSettings SensorSettings_Radar_1()
    {
        var sensorSettings = new SensorSettings()
        {
            SensorId = 1,
            SensorName = "Platform_1_Radar_1",
            SensorType = SensorType.Radar,
            PlatformId = 1,
            ScanSettings = ScanSettingsFactory.ScanSettings_CircularScan_1()
        };

        return sensorSettings;
    }
}