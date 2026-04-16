using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Platform;
using MissionEngineering.Sensor;
using MissionEngineering.Track;
using static System.Math;

namespace MissionEngineering.Simulation;

public class Simulation : ISimulation
{
    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public List<IExecutableModel> SimulationModels { get; set; }

    public List<Platform.Platform> Platforms { get; set; }

    public List<PlatformRelative> RelativePlatforms { get; set; }

    public List<Sensor.Sensor> Sensors { get; set; }

    public TrackManager TrackManager { get; set; }

    public IDataRecorder DataRecorder { get; set; }

    private double nextDisplayTime;

    private int displayCount;

    private int trackPredictionCountActual;
    private int trackPredictionCountMax;

    public Simulation(SimulationSettings simulationSettings, ScenarioSettings scenarioSettings, ISimulationClock simulationClock, ILLAOrigin llaOrigin, IDataRecorder dataRecorder)
    {
        SimulationSettings = simulationSettings;
        ScenarioSettings = scenarioSettings;
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
        DataRecorder = dataRecorder;
    }

    public void Run()
    {
        LogUtilities.LogInformation("***");
        LogUtilities.LogInformation($"Run Number {SimulationSettings.RunNumber} Started...");
        LogUtilities.LogInformation("");

        var clockSettings = ScenarioSettings.SimulationClockSettings;

        var time = clockSettings.TimeStart;

        Initialise(time);

        RunSimulation(time);

        Finalise(time);

        LogUtilities.LogInformation($"Run Number {SimulationSettings.RunNumber} Finished.");
        LogUtilities.LogInformation("***");
        LogUtilities.LogInformation("");

        CreateZipFile(false, true);
    }

    public void Initialise(double time)
    {
        LogUtilities.LogInformation("Initialise Started...");
        LogUtilities.LogInformation("");

        SimulationClock.DateTimeOrigin.DateTimeStart = ScenarioSettings.SimulationClockSettings.DateTimeOrigin;

        LLAOrigin.PositionLLA = ScenarioSettings.LLAOrigin;

        DataRecorder.SimulationData.ScenarioSettings = ScenarioSettings;

        trackPredictionCountActual = 0;
        trackPredictionCountMax = (int)Round(ScenarioSettings.SimulationClockSettings.TrackPredictionTimeStep / ScenarioSettings.SimulationClockSettings.TimeStep);
    
        Platforms = [];
        RelativePlatforms = [];
        Sensors = [];
        SimulationModels = [];

        foreach (var platformSettings in ScenarioSettings.PlatformSettingsList)
        {
            var platform = new Platform.Platform(SimulationClock, LLAOrigin)
            {
                PlatformSettings = platformSettings
            };

            Platforms.Add(platform);
            SimulationModels.Add(platform);
        }

        for (int i = 1; i < Platforms.Count; i++)
        {
            var relativePlatform = new PlatformRelative(Platforms[0], Platforms[i]);

            RelativePlatforms.Add(relativePlatform);
            SimulationModels.Add(relativePlatform);
        }

        foreach (var sensorSettings in ScenarioSettings.SensorSettingsList)
        {
            var sensorPlatform = Platforms.Where(s => s.PlatformSettings.PlatformHeader.PlatformId == sensorSettings.PlatformId).First();

            var sensor = new Sensor.Sensor(SimulationClock)
            {
                SensorSettings = sensorSettings,
                SensorPlatform = sensorPlatform,
                TargetPlatforms = Platforms
            };

            Sensors.Add(sensor);
            SimulationModels.Add(sensor);
        }

        TrackManager = new TrackManager(LLAOrigin);

        InitialiseModels(time);

        DataRecorder.Initialise(time);

        var simulationSettingsString = SimulationSettings.ConvertToJsonString();
        var scenarioSettingsString = ScenarioSettings.ConvertToJsonString();

        nextDisplayTime = ScenarioSettings.SimulationClockSettings.TimeStart;

        LogUtilities.LogInformation($"Simulation Settings {Environment.NewLine} {simulationSettingsString}");
        LogUtilities.LogInformation($"Scenario Settings {Environment.NewLine} {scenarioSettingsString}");

        LogUtilities.LogInformation("Initialise Finished.");
        LogUtilities.LogInformation("");
    }

    public void RunSimulation(double time)
    {
        var clockSettings = ScenarioSettings.SimulationClockSettings;

        LogUtilities.LogInformation("Run Started...");
        LogUtilities.LogInformation("");

        while (time <= clockSettings.TimeEnd)
        {
            ShowProgress(time);

            Update(time);

            time += clockSettings.TimeStep;
        }

        LogUtilities.LogInformation("");
        LogUtilities.LogInformation("Run Finished.");
        LogUtilities.LogInformation("");
    }

    public void Update(double time)
    {
        UpdateModels(time);

        UpdateTracker(time);

        RecordData();
    }

    public void UpdateTracker(double time)
    {
        var sensorReports = Sensors.SelectMany(s => s.SensorReports).ToList();

        TrackManager.SensorReports = sensorReports;

        TrackManager.ProcessSensorReports();

        var isUpdatePredictedTracks = IsUpdatePredictedTracks();

        if (isUpdatePredictedTracks)
        {
            TrackManager.PredictTracks(time);
            RecordTrackDataPredicted();
        }
    }

    public bool IsUpdatePredictedTracks()
    {
        var IsUpdatePredictedTracks = trackPredictionCountActual == 0;

        trackPredictionCountActual++;

        if (trackPredictionCountActual == trackPredictionCountMax)
        {
            trackPredictionCountActual = 0;
        }

        return IsUpdatePredictedTracks;
    }

    public void RecordData()
    {
        RecordPlatformData();
        RecordRelativePlatformData();
        RecordSensorReports();
    }

    public void RecordPlatformData()
    {
        var sd = DataRecorder.SimulationData;

        foreach (var platform in Platforms)
        {
            sd.PlatformDataAll.Add(platform.PlatformData);

            var psm = PlatformMessageConversions.ConvertToPlatformStateMessage(platform.PlatformState);

            sd.PlatformStateMessagesAll.Add(psm);

            sd.SimulationMessages.Add(psm);
        }
    }

    public void RecordRelativePlatformData()
    {
        var sd = DataRecorder.SimulationData;

        foreach (var relativePlatform in RelativePlatforms)
        {
            sd.PlatformDataRelativeAll.Add(relativePlatform.PlatformStateRelative);

            var psrm = PlatformMessageConversions.ConvertToPlatformStateRelativeMessage(relativePlatform.PlatformStateRelative);

            sd.PlatformStateRelativeMessagesAll.Add(psrm);

            sd.SimulationMessages.Add(psrm);
        }
    }

    public void RecordSensorReports()
    {
        var sd = DataRecorder.SimulationData;

        foreach (var sensor in Sensors)
        {
            sd.SensorReportsAll.AddRange(sensor.SensorReports);

            foreach (var sensorReport in sensor.SensorReports)
            {
                var srm = SensorMessageConversions.ConvertToSensorReportMessage(sensorReport);

                sd.SensorReportMessagesAll.Add(srm);

                sd.SimulationMessages.Add(srm);
            }
        }
    }

    public void RecordTrackDataPredicted()
    {
        var sd = DataRecorder.SimulationData;

        var trackDataPredicted = TrackManager.TrackList.Tracks.Select(s => s.TrackDataPredicted).ToList();

        sd.TrackDataPredictedAll.AddRange(trackDataPredicted);

        var trackDataPredictedMessages = TrackMessageConversions.ConvertToTrackDataPredictedMessages(trackDataPredicted);

        sd.TrackDataPredictedMessagesAll.AddRange(trackDataPredictedMessages);
    }

    public void Finalise(double time)
    {
        LogUtilities.LogInformation("Finalise Started...");
        LogUtilities.LogInformation("");

        FinaliseModels(time);

        DataRecorder.Finalise(time);

        CreateZipFile(true, false);

        LogUtilities.LogInformation("");
        LogUtilities.LogInformation("Finalise Finished.");
        LogUtilities.LogInformation("");
    }

    public void ShowProgress(double time)
    {
        var isDisplayTime = (time >= nextDisplayTime);

        if (isDisplayTime)
        {
            LogUtilities.LogInformation($"Time = {nextDisplayTime:000}s");

            displayCount++;
            nextDisplayTime = ScenarioSettings.SimulationClockSettings.TimeStart + displayCount * 5.0;
        }
    }

    public void InitialiseModels(double time)
    {
        foreach (var model in SimulationModels)
        {
            model.Initialise(time);
        }
    }

    public void UpdateModels(double time)
    {
        foreach (var model in SimulationModels)
        {
            model.Update(time);
        }
    }

    public void FinaliseModels(double time)
    {
        foreach (var model in SimulationModels)
        {
            model.Finalise(time);
        }
    }

    public void CreateZipFile(bool isWriteToLog, bool isWriteData)
    {
        if (!DataRecorder.SimulationData.SimulationSettings.IsWriteData)
        {
            return;
        }

        if (!DataRecorder.SimulationData.SimulationSettings.IsCreateZipFile)
        {
            return;
        }

        var zipFileName = $"{DataRecorder.SimulationData.SimulationSettings.SimulationName}.zip";

        var zipFileNameFull = DataRecorder.SimulationData.SimulationSettings.GetFileNameFull(zipFileName);

        if (isWriteToLog)
        {
            LogUtilities.LogInformation($"Writing File : {zipFileNameFull}");
        }

        if (isWriteData)
        {
            LogUtilities.CloseLog();

            ZipUtilities.ZipDirectory(DataRecorder.SimulationData.SimulationSettings.OutputFolder, zipFileNameFull);
        }
    }
}