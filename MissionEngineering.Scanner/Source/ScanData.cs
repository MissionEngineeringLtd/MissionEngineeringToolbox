using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Scanner;

public record ScanData
{
    public SimulationTimeStamp TimeStamp { get; set; }

    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public int ScannerId { get; set; }

    public string ScannerName { get; set; }

    public int ScanNumber { get; set; }

    public bool IsStartOfScan { get; set; }

    public double PlatformHeadingAngle_deg { get; set; }

    public double PlatformPitchAngle_deg { get; set; }

    public double ScanAzimuthAngle_Body_deg { get; set; }

    public double ScanElevationAngle_Body_deg { get; set; }

    public double ScanAzimuthAngle_NED_deg { get; set; }

    public double ScanElevationAngle_NED_deg { get; set; }

    public double ScanAzimuthRate_degs { get; set; }

    public double ScanElevationRate_degs { get; set; }

    public double ScanAzimuthRate_RPM => ScanAzimuthRate_degs.DegreesToRpm();
}