using MissionEngineering.Simulation.Messages;

namespace MissionEngineering.Tracker;

public static class TrackMessageConversions
{
    public static List<TrackDataPredictedMessage> ConvertToTrackDataPredictedMessages(List<TrackDataPredicted> trackDataPredictedList)
    {
        var trackDataPredictedMessages = trackDataPredictedList.Select(ConvertToTrackDataPredictedMessage).ToList();

        return trackDataPredictedMessages;
    }

    public static TrackDataPredictedMessage ConvertToTrackDataPredictedMessage(TrackDataPredicted trackDataPredicted)
    {
        var tp = trackDataPredicted;

        var header = new SimulationMessageHeader
        {
            WallClockDateTime = DateTime.UtcNow,
            SimulationTime_s = trackDataPredicted.PredictionTime,
            SourceId = trackDataPredicted.SensorId,
            SourceName = trackDataPredicted.SensorId.ToString(),
            MessageId = 0,
            MessageDescription = "Track Data Predicted Message",
        };

        var trackDataPredictedMessage = new TrackDataPredictedMessage
        {
            Header = header,
            TrackId = trackDataPredicted.TrackId,
            PredictionTime = trackDataPredicted.PredictionTime,
            LastUpdateTime = trackDataPredicted.LastUpdateTime,
            TimeSinceLastUpdate = trackDataPredicted.TimeSinceLastUpdate,
            NumberOfUpdates = trackDataPredicted.NumberOfUpdates,
            SensorId = trackDataPredicted.SensorId,
            SensorPlatformId = trackDataPredicted.SensorPlatformId,
            TargetPlatformId = trackDataPredicted.TargetPlatformId,
            TargetReportId = trackDataPredicted.TargetReportId,
            Latitude_deg = tp.PositionLLA.Latitude_deg,
            Longitude_deg = tp.PositionLLA.Longitude_deg,
            Altitude_m = tp.PositionLLA.Altitude_m,
            PositionNorth_m = tp.PositionNED.PositionNorth_m,
            PositionEast_m = tp.PositionNED.PositionEast_m,
            PositionDown_m = tp.PositionNED.PositionDown_m,
            VelocityNorth_ms = tp.VelocityNED.VelocityNorth_ms,
            VelocityEast_ms = tp.VelocityNED.VelocityEast_ms,
            VelocityDown_ms = tp.VelocityNED.VelocityDown_ms,
        };

        return trackDataPredictedMessage;
    }
}