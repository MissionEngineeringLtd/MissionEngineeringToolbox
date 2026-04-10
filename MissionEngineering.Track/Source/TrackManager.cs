using MissionEngineering.Math;
using MissionEngineering.Sensor;

namespace MissionEngineering.Track;

public class TrackManager
{
    public TrackList TrackList { get; set; }

    public int NextTrackId { get; set; }

    public List<SensorReport> SensorReports { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public TrackManager(ILLAOrigin llaOrigin)
    {
        TrackList = new TrackList();

        NextTrackId = 1001;

        LLAOrigin = llaOrigin;
    }

    public void ProcessSensorReports()
    {
        foreach (var sensorReport in SensorReports)
        {
            var track = GetTrackForSensorReport(sensorReport.SensorReportHeader.TargetPlatformId);

            ProcessSensorReport(sensorReport, track);
        }
    }

    public void ProcessSensorReport(SensorReport sensorReport, Track track)
    {
        if (track is null)
        {
            InitialiseTrack(sensorReport);
        }
        else
        {
            UpdateTrack(sensorReport, track);
        }
    }

    public void InitialiseTrack(SensorReport sensorReport)
    {
        var track = new Track(LLAOrigin);

        track.InitialiseTrack(sensorReport);

        track.TrackDataSmoothed.TrackId = NextTrackId;

        TrackList.AddTrack(track);

        NextTrackId++;
    }

    public void UpdateTrack(SensorReport sensorReport, Track track)
    {
        track.UpdateTrack(sensorReport);
    }

    public void PredictTracks(double time)
    {
        foreach (var track in TrackList.Tracks)
        {
            track.PredictTrack(time);
        }
    }

    public Track GetTrackForSensorReport(int targetPlatformId)
    {
        var track = TrackList.Tracks.FirstOrDefault(t => t.TrackDataSmoothed.TargetPlatformId == targetPlatformId);

        return track;
    }
}