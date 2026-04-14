using MissionEngineering.Math;

namespace MissionEngineering.Scanner;

public class ScanSettings
{
    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public int ScannerId { get; set; }

    public string ScannerName { get; set; }

    public ScanType ScanType { get; set; }

    public double MaximumScanAzimuthRate_degs { get; set; }

    public double MaximumScanAzimuthRate_RPM
    {
        get => MaximumScanAzimuthRate_degs.DegreesToRpm();
        set => MaximumScanAzimuthRate_degs = value.RpmToDegrees();
    }

    public double InitialScanAzimuthAngle_Body_deg { get; set; }

    public double InitialScanElevationAngle_Body_deg { get; set; }
}