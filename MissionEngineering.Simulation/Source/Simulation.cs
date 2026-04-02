using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class Simulation : ISimulation
{
    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public List<IExecutableModel> SimulationModels { get; set; }

    public IDataRecorder DataRecorder { get; set; }

    private double nextDisplayTime;

    private int displayCount;

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

        SimulationModels = [];

        foreach (var platformSettings in ScenarioSettings.PlatformSettingsList)
        {
            var platformModel = new Platform.Platform(SimulationClock, LLAOrigin)
            {
                PlatformSettings = platformSettings
            };

            SimulationModels.Add(platformModel);
        }

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
    }

    public void Finalise(double time)
    {
        LogUtilities.LogInformation("Finalise Started...");
        LogUtilities.LogInformation("");

        FinaliseModels(time);

        var platformDataAll = GeneratePlatformDataAll();

        DataRecorder.SimulationData.PlatformDataAll = platformDataAll;

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

    public List<PlatformData> GeneratePlatformDataAll()
    {
        var platformDataAll = new List<PlatformData>();

        foreach (var model in SimulationModels)
        {
            platformDataAll.AddRange(((Platform.Platform)model).PlatformDataList);
        }

        return platformDataAll;
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
            LogUtilities.LogInformation($"Writing     Zip  File : {zipFileNameFull}");
        }

        if (isWriteData)
        {
            LogUtilities.CloseLog();

            ZipUtilities.ZipDirectory(DataRecorder.SimulationData.SimulationSettings.OutputFolder, zipFileNameFull);
        }
    }
}