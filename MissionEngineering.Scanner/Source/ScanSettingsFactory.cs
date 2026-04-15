namespace MissionEngineering.Scanner;

public static class ScanSettingsFactory
{
    public static ScanSettings ScanSettings_CircularScan_1()
    {
        var scanSettings = new ScanSettings()
        {
            PlatformId = 1,
            PlatformName = "Platform_1",
            SensorId = 1,
            SensorName = "Sensor_1",
            ScannerId = 1,
            ScannerName = "TestScanner",
            ScanType = ScanType.CircularScan,
            InitialScanAzimuthAngle_Body_deg = 30.0,
            InitialScanElevationAngle_Body_deg = 1.0,
            MaximumScanAzimuthRate_RPM = 48.0,
        };

        return scanSettings;
    }
}
