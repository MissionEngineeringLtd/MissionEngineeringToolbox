using MissionEngineering.Simulation.Messages;

namespace MissionEngineering.Sensor;

public static class SensorMessageConversions
{
    public static SensorReportMessage ConvertToSensorReportMessage(SensorReport sensorReport)
    {
        var header = new SimulationMessageHeader()
        {
            MessageId = 1,
            WallClockDateTime = sensorReport.SensorReportHeader.TimeStamp.WallClockDateTime,
            SimulationDateTime = sensorReport.SensorReportHeader.TimeStamp.SimulationDateTime,
            SimulationTime_s = sensorReport.SensorReportHeader.TimeStamp.SimulationTime_s,
            SourceId = sensorReport.SensorReportHeader.SensorId,
            SourceName = sensorReport.SensorReportHeader.SensorName,
            MessageDescription = "Sensor Report"
        };

        var loc = sensorReport.TargetLocation;

        var sensorReportMessage = new SensorReportMessage()
        {
            Header = header,
            SensorPlatformId = sensorReport.SensorReportHeader.SensorPlatformId,
            TargetPlatformId = sensorReport.SensorReportHeader.TargetPlatformId,
            SensorId = sensorReport.SensorReportHeader.SensorId,
            TargetLocationType = loc.TargetLocationType.ToString(),
            IsPositionLLAValid = loc.IsPositionLLAValid,
            IsPositionNEDValid = loc.IsPositionNEDValid,
            IsVelocityNEDValid = loc.IsVelocityNEDValid,
            IsRangeValid = loc.IsRangeValid,
            IsRangeRateValid = loc.IsRangeRateValid,
            IsAzimuthValid = loc.IsAzimuthValid,
            IsElevationValid = loc.IsElevationValid,
            IsAltitudeValid = loc.IsAltitudeValid,
            TargetLatitude_deg = loc.PositionLLA.Latitude_deg,
            TargetLongitude_deg = loc.PositionLLA.Longitude_deg,
            TargetAltitude_m = loc.PositionLLA.Altitude_m,
            TargetPositionNorth_m = loc.PositionNED.PositionNorth_m,
            TargetPositionEast_m = loc.PositionNED.PositionEast_m,
            TargetPositionDown_m = loc.PositionNED.PositionDown_m,
            TargetVelocityNorth_ms = loc.VelocityNED.VelocityNorth_ms,
            TargetVelocityEast_ms = loc.VelocityNED.VelocityEast_ms,
            TargetVelocityDown_ms = loc.VelocityNED.VelocityDown_ms,
            TargetRange_m = loc.Range_m,
            TargetRangeRate_ms = loc.RangeRate_ms,
            TargetAzimuthAngle_deg = loc.AzimuthAngle_deg,
            TargetElevationAngle_deg = loc.ElevationAngle_deg,
        };

        return sensorReportMessage;
    }
}