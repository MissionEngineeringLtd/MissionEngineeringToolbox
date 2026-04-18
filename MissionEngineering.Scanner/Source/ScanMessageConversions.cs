using MissionEngineering.Simulation.Messages;

namespace MissionEngineering.Scanner;

public static class ScanMessageConversions
{
    public static List<ScanDataMessage> ConvertToScanDataMessages(List<ScanData> scanDataList)
    {
        var scanDataMessages = scanDataList.Select(ConvertToScanDataMessage).ToList();

        return scanDataMessages;
    }

    public static ScanDataMessage ConvertToScanDataMessage(ScanData scanData)
    {
        var sd = scanData;

        var header = new SimulationMessageHeader()
        {
            MessageId = 1,
            WallClockDateTime = sd.TimeStamp.WallClockDateTime,
            SimulationDateTime = sd.TimeStamp.SimulationDateTime,
            SimulationTime_s = sd.TimeStamp.SimulationTime_s,
            SourceId = sd.ScannerId,
            SourceName = sd.ScannerName,
            MessageDescription = "Scan Data"
        };


        var scanDataMessage = new ScanDataMessage()
        {
            Header = header,
            PlatformId = sd.PlatformId,
            PlatformName = sd.PlatformName,
            ScannerId = sd.ScannerId,
            ScannerName = sd.ScannerName,
            IsStartOfScan = sd.IsStartOfScan,
            ScanNumber = sd.ScanNumber,
            PlatformHeadingAngle_deg = sd.PlatformHeadingAngle_deg,
            PlatformPitchAngle_deg = sd.PlatformPitchAngle_deg,
            ScanAzimuthAngle_Body_deg = sd.ScanAzimuthAngle_Body_deg,
            ScanElevationAngle_Body_deg = sd.ScanElevationAngle_Body_deg,
            ScanAzimuthAngle_NED_deg = sd.ScanAzimuthAngle_NED_deg,
            ScanElevationAngle_NED_deg = sd.ScanElevationAngle_NED_deg,
            ScanAzimuthRate_degs = sd.ScanAzimuthRate_degs,
            ScanElevationRate_degs = sd.ScanElevationRate_degs,
            ScanAzimuthRate_RPM = sd.ScanAzimuthRate_RPM,
        };

        return scanDataMessage;
    }
}