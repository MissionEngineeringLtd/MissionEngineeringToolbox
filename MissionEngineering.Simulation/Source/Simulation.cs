using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Platform;
using MissionEngineering.Scanner;
using MissionEngineering.Sensor;
using MissionEngineering.Tracker;
using static System.Math;

namespace MissionEngineering.Simulation;

public class Simulation : ISimulation
{
    public ILogClass Log { get; set; }

    public SimulationRunSettings SimulationRunSettings { get; set; }

    public SimulationSettings SimulationSettings { get; set; }

    public List<ISimulationEvent> SimulationEvents { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public IPlatformManager PlatformManager { get; set; }

    public ISimulationEventProcessor SimulationEventProcessor { get; set; }

    public List<IExecutableModel> SimulationModels { get; set; }

    public List<PlatformRelative> RelativePlatforms { get; set; }

    public List<Sensor.Sensor> Sensors { get; set; }

    public TrackManager TrackManager { get; set; }

    public IDataRecorder DataRecorder { get; set; }

    private double nextDisplayTime;

    private int displayCount;

    private int platformDataRecordCountActual;
    private int platformDataRecordCountMax;

    private int trackPredictionCountActual;
    private int trackPredictionCountMax;

    public Simulation(SimulationRunSettings simulationRunSettings, SimulationSettings simulationSettings, ISimulationClock simulationClock, ILLAOrigin llaOrigin, IPlatformManager platformManager, ISimulationEventProcessor simulationEventProcessor, IDataRecorder dataRecorder, ILogClass log)
    {
        SimulationRunSettings = simulationRunSettings;
        SimulationSettings = simulationSettings;
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
        PlatformManager = platformManager;
        SimulationEventProcessor = simulationEventProcessor;
        DataRecorder = dataRecorder;
        Log = log;
    }

    public ISimulation Run()
    {
        Log.LogInformation("***");
        Log.LogInformation($"Run Number {SimulationRunSettings.RunNumber} Started...");
        Log.LogInformation("");

        var clockSettings = new SimulationClockSettings()
        { 
            DateTimeOrigin = SimulationSettings.DateTimeOrigin, 
            TimeStart_s = SimulationSettings.TimeStart_s, 
            TimeEnd_s = SimulationSettings.TimeEnd_s, 
            TimeStep_s = SimulationSettings.TimeStep_s, 
            TrackPredictionTimeStep_s = SimulationSettings.TrackPredictionTimeStep_s 
        };

        var time = clockSettings.TimeStart_s;

        Initialise(time);

        RunSimulation(time);

        Finalise(time);

        Log.LogInformation($"Run Number {SimulationRunSettings.RunNumber} Finished.");
        Log.LogInformation("***");
        Log.LogInformation("");

        CreateZipFile(false, true);

        return this;
    }

    public void Initialise(double time)
    {
        CreateLogger();

        Log.LogInformation("Initialise Started...");
        Log.LogInformation("");

        SimulationClock.DateTimeOrigin.DateTimeStart = DateTime.Parse(SimulationSettings.DateTimeOrigin);

        LLAOrigin.PositionLLA.Latitude_deg = SimulationSettings.Latitude_deg;
        LLAOrigin.PositionLLA.Longitude_deg = SimulationSettings.Longitude_deg;

        DataRecorder.SimulationData.SimulationSettings = SimulationSettings;

        platformDataRecordCountActual = 0;
        platformDataRecordCountMax = (int)Round(SimulationSettings.PlatformDataRecordTimeStep_s / SimulationSettings.TimeStep_s);

        trackPredictionCountActual = 0;
        trackPredictionCountMax = (int)Round(SimulationSettings.TrackPredictionTimeStep_s / SimulationSettings.TimeStep_s);

        RelativePlatforms = [];
        Sensors = [];
        SimulationModels = [];

        SimulationEventProcessor.SimulationEvents = SimulationEvents;

        SimulationEventProcessor.Initialise(time);

        DataRecorder.SimulationData.SimulationEvents = SimulationEventProcessor.SimulationEvents;

        SimulationModels.Add(PlatformManager);

        for (int i = 1; i < PlatformManager.Platforms.Count; i++)
        {
            var relativePlatform = new PlatformRelative(PlatformManager.Platforms[0], PlatformManager.Platforms[i]);

            RelativePlatforms.Add(relativePlatform);
            SimulationModels.Add(relativePlatform);
        }

        List<SensorSettings> sensorSettingsList = [];

        foreach (var sensorSettings in sensorSettingsList)
        {
            var sensorPlatform = PlatformManager.Platforms.Where(s => s.PlatformSettings.PlatformHeader.PlatformId == sensorSettings.PlatformId).First();

            var sensor = new Sensor.Sensor(SimulationClock)
            {
                SensorSettings = sensorSettings,
                SensorPlatform = sensorPlatform,
                TargetPlatforms = PlatformManager.Platforms
            };

            Sensors.Add(sensor);
            SimulationModels.Add(sensor);
        }

        TrackManager = new TrackManager(LLAOrigin);

        InitialiseModels(time);

        DataRecorder.Initialise(time);

        var simulationRunSettingsString = SimulationRunSettings.ConvertToJsonString();
        var simulationSettingsString = SimulationSettings.ConvertToJsonString();

        nextDisplayTime = SimulationSettings.TimeStart_s;

        Log.LogInformation($"Simulation Run Settings {Environment.NewLine} {simulationRunSettingsString}");
        Log.LogInformation($"Simulation Settings {Environment.NewLine} {simulationSettingsString}");

        Log.LogInformation("Initialise Finished.");
        Log.LogInformation("");
    }

    public void CreateLogger()
    {
        Log.CreateLogger(SimulationRunSettings.LogFileName, SimulationRunSettings.IsAddConsoleLogging, SimulationRunSettings.IsAddFileLogging);

        Log.RunNumber = SimulationRunSettings.RunNumber;
    }

    public void RunSimulation(double time)
    {
        Log.LogInformation("Run Started...");
        Log.LogInformation("");

        while (time <= SimulationSettings.TimeEnd_s)
        {
            ShowProgress(time);

            Update(time);

            time += SimulationSettings.TimeStep_s;
        }

        Log.LogInformation("");
        Log.LogInformation("Run Finished.");
        Log.LogInformation("");
    }

    public void Update(double time)
    {
        SimulationEventProcessor.Update(time);

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

    public bool IsRecordPlatformData()
    {
        var isRecordPlatformData = platformDataRecordCountActual == 0;

        platformDataRecordCountActual++;

        if (platformDataRecordCountActual == platformDataRecordCountMax)
        {
            platformDataRecordCountActual = 0;
        }

        return isRecordPlatformData;
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
        RecordSensorScannerData();
        RecordSensorReports();
    }

    public void RecordPlatformData()
    {
        if (!IsRecordPlatformData())
        {
            return;
        }

        var sd = DataRecorder.SimulationData;

        foreach (var platform in PlatformManager.Platforms)
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

    public void RecordSensorScannerData()
    {
        var sd = DataRecorder.SimulationData;

        var scanDataList = Sensors.Select(s => s.Scanner.ScanData).ToList();

        sd.ScanDataAll.AddRange(scanDataList);

        var scanDataMessages = ScanMessageConversions.ConvertToScanDataMessages(scanDataList);

        sd.ScanDataMessagesAll.AddRange(scanDataMessages);
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
        Log.LogInformation("Finalise Started...");
        Log.LogInformation("");

        SimulationEventProcessor.Finalise(time);

        FinaliseModels(time);

        DataRecorder.Finalise(time);

        CreateZipFile(true, false);

        Log.LogInformation("");
        Log.LogInformation("Finalise Finished.");
        Log.LogInformation("");
    }

    public void ShowProgress(double time)
    {
        var isDisplayTime = (time >= nextDisplayTime);

        if (isDisplayTime)
        {
            Log.LogInformation($"Time = {nextDisplayTime:000}s");

            displayCount++;
            nextDisplayTime = SimulationSettings.TimeStart_s + displayCount * 5.0;
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
        if (!DataRecorder.SimulationData.SimulationRunSettings.IsWriteData)
        {
            return;
        }

        if (!DataRecorder.SimulationData.SimulationRunSettings.IsCreateZipFile)
        {
            return;
        }

        var zipFileName = $"{DataRecorder.SimulationData.SimulationRunSettings.SimulationName}.zip";

        var zipFileNameFull = DataRecorder.SimulationData.SimulationRunSettings.GetFileNameFull(zipFileName);

        if (isWriteToLog)
        {
            Log.LogInformation($"Writing File : {zipFileNameFull}");
        }

        if (isWriteData)
        {
            Log.CloseLog();

            ZipUtilities.ZipDirectory(DataRecorder.SimulationData.SimulationRunSettings.OutputFolder, zipFileNameFull);
        }
    }
}