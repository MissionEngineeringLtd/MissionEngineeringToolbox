using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Scanner;

public class Scanner : IScanner
{
    public ScanSettings ScanSettings { get; set; }

    public ScanData ScanData { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public PlatformState PlatformState { get; set; }

    public Scanner(ISimulationClock simulationClock)
    {
        SimulationClock = simulationClock;
    }

    public void Initialise(double time_s)
    {
        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        var scanAzimuthAngle_NED_deg = GetScanAzimuthAngle_NED_deg(ScanSettings.InitialScanAzimuthAngle_Body_deg, PlatformState.Attitude.HeadingAngle_deg);
        var scanElevation_NED_deg = GetScanElevationAngle_NED_deg(ScanSettings.InitialScanElevationAngle_Body_deg, PlatformState.Attitude.PitchAngle_deg);

        ScanData = new ScanData()
        {
            TimeStamp = timeStamp,
            PlatformId = ScanSettings.PlatformId,
            PlatformName = ScanSettings.PlatformName,
            ScannerId = ScanSettings.ScannerId,
            ScannerName = ScanSettings.ScannerName,
            ScanNumber = 1,
            PlatformHeadingAngle_deg = PlatformState.Attitude.HeadingAngle_deg,
            PlatformPitchAngle_deg = PlatformState.Attitude.PitchAngle_deg,
            ScanAzimuthAngle_Body_deg = ScanSettings.InitialScanAzimuthAngle_Body_deg,
            ScanElevationAngle_Body_deg = ScanSettings.InitialScanElevationAngle_Body_deg,
            ScanAzimuthAngle_NED_deg = scanAzimuthAngle_NED_deg,
            ScanElevationAngle_NED_deg = scanElevation_NED_deg,
            ScanAzimuthRate_degs = ScanSettings.MaximumScanAzimuthRate_degs,
            ScanElevationRate_degs = 0.0,
        };
    }

    public void Update(double time_s)
    {
        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        var dT = time_s - ScanData.TimeStamp.SimulationTime_s;

        var scanAzimuthAngle_Body_deg = ScanData.ScanAzimuthAngle_Body_deg + ScanData.ScanAzimuthRate_degs * dT;
        var scanElevationAngle_Body_deg = ScanData.ScanElevationAngle_Body_deg;

        var isStartOfScan = false;
        var scanNumber = ScanData.ScanNumber;

        if (scanAzimuthAngle_Body_deg > 360.0)
        {
            scanAzimuthAngle_Body_deg -= 360.0;
            isStartOfScan = true;
            scanNumber++;
        }

        var scanAzimuthAngle_NED_deg = GetScanAzimuthAngle_NED_deg(scanAzimuthAngle_Body_deg, PlatformState.Attitude.HeadingAngle_deg);
        var scanElevation_NED_deg = GetScanElevationAngle_NED_deg(scanElevationAngle_Body_deg, PlatformState.Attitude.PitchAngle_deg);

        ScanData = ScanData with
        {
            TimeStamp = timeStamp,
            IsStartOfScan = isStartOfScan,
            PlatformHeadingAngle_deg = PlatformState.Attitude.HeadingAngle_deg,
            PlatformPitchAngle_deg = PlatformState.Attitude.PitchAngle_deg,
            ScanAzimuthAngle_Body_deg = scanAzimuthAngle_Body_deg,
            ScanElevationAngle_Body_deg = scanElevationAngle_Body_deg,
            ScanAzimuthAngle_NED_deg = scanAzimuthAngle_NED_deg,
            ScanElevationAngle_NED_deg = scanElevation_NED_deg,
            ScanNumber = scanNumber
        };
    }

    public void Finalise(double time_s)
    {
    }

    public double GetScanAzimuthAngle_NED_deg(double scanAzimuthAngle_Body_deg, double platformHeadingAngle_deg)
    {
        var scanAzimuthAngle_NED_deg = (scanAzimuthAngle_Body_deg + platformHeadingAngle_deg).ConstrainAnglePlusMinus180();

        return scanAzimuthAngle_NED_deg;
    }

    public double GetScanElevationAngle_NED_deg(double scanElevationAngle_Body_deg, double platformPitchAngle_deg)
    {
        var scanElevationAngle_NED_deg = (scanElevationAngle_Body_deg + platformPitchAngle_deg).ConstrainAnglePlusMinus90();

        return scanElevationAngle_NED_deg;
    }
}