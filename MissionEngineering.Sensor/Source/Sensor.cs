using MissionEngineering.Core;
using MissionEngineering.Platform;
using MissionEngineering.Scanner;

namespace MissionEngineering.Sensor;

public class Sensor : ISensor
{
    public SensorSettings SensorSettings { get; set; }

    public ScanSettings ScanSettings { get; set; }

    public Platform.Platform SensorPlatform { get; set; }

    public List<Platform.Platform> TargetPlatforms { get; set; }

    public List<SensorReport> SensorReports { get; set; }

    public int SensorReportId { get; set; }
    
    public SensorState SensorState { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public Scanner.Scanner Scanner { get; set; }

    public Sensor(ISimulationClock simulationClock)
    {
        SimulationClock = simulationClock;
    }

    public void CreateScanner()
    {
        Scanner = new Scanner.Scanner(SimulationClock)
        {
            ScanSettings = SensorSettings.ScanSettings,
        };
    }

    public void Initialise(double time_s)
    {
        CreateScanner();

        Scanner.PlatformState = SensorPlatform.PlatformState;

        Scanner.Initialise(time_s);

        UpdateSensorState(time_s);
    }

    public void Update(double time_s)
    {
        Scanner.PlatformState = SensorPlatform.PlatformState;

        Scanner.Update(time_s);

        UpdateSensorState(time_s);

        GenerateSensorReports(time_s);
    }

    public void Finalise(double time_s)
    {
    }

    public void GenerateSensorReports(double time_s)
    {
        SensorReports = [];

        foreach (var targetPlatform in TargetPlatforms)
        {
            // Skip if the target platform is also the sensor platform.
            if (targetPlatform.PlatformState.PlatformId == SensorPlatform.PlatformState.PlatformId)
            {
                continue;
            }

            var sensorReport = GenerateSensorReport(time_s, SensorPlatform, targetPlatform);

            if (sensorReport is not null)
            {
                SensorReports.Add(sensorReport);
            }
        }
    }

    public void UpdateSensorState(double time_s)
    {
        SensorState = new SensorState
        {
            Time_s = time_s,
            PointingAzimuthBody_deg = Scanner.ScanData.ScanAzimuthAngle_Body_deg,
            PointingElevationBody_deg = Scanner.ScanData.ScanElevationAngle_Body_deg,
            PointingAzimuthNorth_deg = Scanner.ScanData.ScanAzimuthAngle_NED_deg,
            PointingElevationNorth_deg = Scanner.ScanData.ScanElevationAngle_NED_deg
        };
    }
    public SensorReport GenerateSensorReport(double time_s, Platform.Platform sensorPlatform, Platform.Platform targetPlatform)
    {
        var relativeStates = PlatformFunctions.GeneratePlatformStateRelative(sensorPlatform.PlatformState, targetPlatform.PlatformState);

        var isInCoverage = SensorFunctions.IsInSensorCoverage(relativeStates, SensorState);

        if (!isInCoverage)
        {
            return null;
        }

        SensorReportId++;

        var timeStamp = SimulationClock.GetTimeStamp(time_s);

        var polars = relativeStates.RelativePolarsNED;

        var sensorReport = new SensorReport()
        {
            SensorReportHeader = new SensorReportHeader()
            {
                TimeStamp = timeStamp,
                SensorPlatformId = SensorSettings.PlatformId,
                SensorPlatformName = sensorPlatform.PlatformState.PlatformName,
                TargetPlatformId = targetPlatform.PlatformState.PlatformId,
                TargetPlatformName = targetPlatform.PlatformState.PlatformName,
                SensorId = SensorSettings.SensorId,
                SensorName = SensorSettings.SensorName,
                SensorType = SensorSettings.SensorType,
                SensorReportId = SensorReportId,
            },
            SensorPlatformState = sensorPlatform.PlatformState,
            SensorState = SensorState,
            TargetIdentification = new SensorTargetIdentification(),
            TargetLocation = new SensorTargetLocation()
            {
                TargetLocationType = SensorTargetLocationType.Geolocation,
                IsRangeValid = true,
                IsRangeRateValid = true,
                IsAzimuthValid = true,
                IsElevationValid = true,
                IsAltitudeValid = true,
                IsPositionLLAValid = true,
                IsPositionNEDValid = true,
                IsVelocityNEDValid = true,
                Range_m = polars.Range_m,
                RangeRate_ms = polars.RangeRate_ms,
                AzimuthAngle_deg = polars.AzimuthAngle_deg,
                ElevationAngle_deg = polars.ElevationAngle_deg,
                PositionLLA = targetPlatform.PlatformState.PositionLLA,
                PositionNED = targetPlatform.PlatformState.PositionNED,
                VelocityNED = targetPlatform.PlatformState.VelocityNED
            }
        };

        return sensorReport;
    }
}