using MissionEngineering.Core;
using MissionEngineering.Simdis;
using MissionEngineering.Simulation;

namespace MissionEngineering.DataRecorder;

public class DataRecorder : IDataRecorder
{
    public ILogClass Log { get; set; }

    public SimulationData SimulationData { get; set; }

    public ISimdisExporter SimdisExporter { get; set; }

    public DataRecorder(SimulationData simulationData, ISimdisExporter simdisExporter, ILogClass log)
    {
        SimulationData = simulationData;
        SimdisExporter = simdisExporter;

        Log = log;
    }

    public void Initialise(double time)
    {
    }

    public void Finalise(double time)
    {
        CreateSimulationEventsPerEventType();

        CreatePlatformDataPerPlatform();
        CreatePlatformDataRelativePerPlatform();

        CreatePlatformStateMessagesPerPlatform();
        CreatePlatformStateRelativeMessagesPerPlatform();

        WriteData();
    }

    public void WriteData()
    {
        if (!SimulationData.SimulationRunSettings.IsWriteData)
        {
            return;
        }

        CreateOutputFolder();

        WriteJsonData();
        WriteYamlData();
        WriteCsvData();
        WriteSimdisData();
    }

    public void CreateOutputFolder()
    {
        if (Directory.Exists(SimulationData.SimulationRunSettings.OutputFolder))
        {
            return;
        }

        Directory.CreateDirectory(SimulationData.SimulationRunSettings.OutputFolder);
    }

    public void CreateSimulationEventsPerEventType()
    {
        SimulationData.SimulationEventsPerEventType = [];

        var eventTypes = SimulationData.SimulationEvents.Select(s => s.EventType).Distinct();

        foreach (var eventType in eventTypes)
        {
            var events = SimulationData.SimulationEvents.Where(s => s.EventType == eventType).ToList();

            SimulationData.SimulationEventsPerEventType.Add(events);
        }
    }   

    public void CreatePlatformDataPerPlatform()
    {
        SimulationData.PlatformDataPerPlatform = [];

        var platformIds = SimulationData.PlatformDataAll.Select(s => s.PlatformHeader.PlatformId).Distinct();

        foreach (var platformId in platformIds)
        {
            var platformData = SimulationData.PlatformDataAll.Where(s => s.PlatformHeader.PlatformId == platformId).ToList();

            SimulationData.PlatformDataPerPlatform.Add(platformData);
        }
    }

    public void CreatePlatformDataRelativePerPlatform()
    {
        SimulationData.PlatformDataRelativePerPlatform = [];

        var platformIds = SimulationData.PlatformDataRelativeAll.Select(s => s.PlatformIdTarget).Distinct();

        foreach (var platformId in platformIds)
        {
            var platformDataRelative = SimulationData.PlatformDataRelativeAll.Where(s => s.PlatformIdTarget == platformId).ToList();

            SimulationData.PlatformDataRelativePerPlatform.Add(platformDataRelative);
        }
    }

    public void CreatePlatformStateMessagesPerPlatform()
    {
        SimulationData.PlatformStateMessagesPerPlatform = [];

        var platformIds = SimulationData.PlatformDataAll.Select(s => s.PlatformHeader.PlatformId).Distinct();

        foreach (var platformId in platformIds)
        {
            var psm = SimulationData.PlatformStateMessagesAll.Where(s => s.PlatformId == platformId).ToList();

            SimulationData.PlatformStateMessagesPerPlatform.Add(psm);
        }
    }

    public void CreatePlatformStateRelativeMessagesPerPlatform()
    {
        SimulationData.PlatformStateRelativeMessagesPerPlatform = [];

        var platformIds = SimulationData.PlatformStateRelativeMessagesAll.Select(s => s.PlatformIdTarget).Distinct();

        foreach (var platformId in platformIds)
        {
            var psrm = SimulationData.PlatformStateRelativeMessagesAll.Where(s => s.PlatformIdTarget == platformId).ToList();

            SimulationData.PlatformStateRelativeMessagesPerPlatform.Add(psrm);
        }
    }

    public void WriteJsonData()
    {
        WriteSimulationRunSettingsToJson();
        WriteSimulationSettingsToJson();
        WriteSimulationEventsAllToJson();
        WriteSimulationEventsPerEventTypeToJson();
    }

    public void WriteYamlData()
    {
        WriteSimulationRunSettingsToYaml();
        WriteSimulationSettingsToYaml();
        WriteSimulationEventsAllToYaml();
        WriteSimulationEventsPerEventTypeToYaml();
    }

    public void WriteCsvData()
    {
        WriteSimulationEventsAllToCsv();

        WritePlatformStateAllToCsv();
        WritePlatformStatePerPlatformToCsv();

        WritePlatformStateRelativeAllToCsv();
        WritePlatformStateRelativePerPlatformToCsv();

        WriteScanDataAllToCsv();
        WriteSensorReportsAllToCsv();

        WriteTrackDataPredictedAllToCsv();

        WriteSimulationMessagesAllToCsv();

        WritePlatformStateMessagesAllToCsv();
        WritePlatformStateMessagesPerPlatformToCsv();

        WritePlatformStateRelativeMessagesAllToCsv();
        WritePlatformStateRelativeMessagesPerPlatformToCsv();

        WriteScanDataMessagesAllToCsv();
        WriteSensorReportMessagesAllToCsv();

        WriteTrackDataPredictedMessagesAllToCsv();
    }

    public void WriteSimdisData()
    {
        SimdisExporter.GenerateSimdisData();
        SimdisExporter.WriteSimdisData();
    }

    public void WriteSimulationRunSettingsToJson()
    {
        var fileName = $"{SimulationData.SimulationRunSettings.SimulationName}_SimulationRunSettings.json";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        SimulationData.SimulationRunSettings.WriteToJsonFile(fileNameFull);
    }

    public void WriteSimulationSettingsToJson()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationSettings.json";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        SimulationData.SimulationSettings.WriteToJsonFile(fileNameFull);
    }

    public void WriteSimulationEventsAllToJson()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationEvents_All.json";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        SimulationData.SimulationEvents.WriteToJsonFile(fileNameFull);
    }

    public void WriteSimulationEventsPerEventTypeToJson()
    {
        foreach (var events in SimulationData.SimulationEventsPerEventType)
        {
            var eventType = events.First().EventType;

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationEvents_{eventType}.json";

            var fileNameFull = GetFileNameFull(fileName);

            Log.LogInformation($"Writing File : {fileNameFull}");

            events.WriteToJsonFile(fileNameFull);
        }
    }

    public void WriteSimulationRunSettingsToYaml()
    {
        var fileName = $"{SimulationData.SimulationRunSettings.SimulationName}_SimulationRunSettings.yaml";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        SimulationData.SimulationRunSettings.WriteToYamlFile(fileNameFull);
    }

    public void WriteSimulationSettingsToYaml()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}SimulationSettings.yaml";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        SimulationData.SimulationSettings.WriteToYamlFile(fileNameFull);
    }

    public void WriteSimulationEventsAllToYaml()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationEvents_All.yaml";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        SimulationData.SimulationEvents.WriteToYamlFile(fileNameFull);
    }

    public void WriteSimulationEventsPerEventTypeToYaml()
    {
        foreach (var events in SimulationData.SimulationEventsPerEventType)
        {
            var eventType = events.First().EventType;

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationEvents_{eventType}.yaml";

            var fileNameFull = GetFileNameFull(fileName);

            Log.LogInformation($"Writing File : {fileNameFull}");

            events.WriteToYamlFile(fileNameFull);
        }
    }

    public void WriteSimulationEventsAllToCsv()
    {
        var data = SimulationData.SimulationEvents.Select(s => new { s.EventTime, s.EventType, V = s.ConvertToJsonString(false, true) });

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationEvents_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WriteSimulationMessagesAllToCsv()
    {
        var data = SimulationData.SimulationMessages;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformStateAllToCsv()
    {
        var platformData = SimulationData.PlatformDataAll;

        var platformStateData = platformData.Select(s => s.PlatformState).ToList();

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformState_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        platformStateData.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformStatePerPlatformToCsv()
    {
        foreach (var platformData in SimulationData.PlatformDataPerPlatform)
        {
            var first = platformData.First();

            var platformName = first.PlatformHeader.PlatformName;

            var platformStateData = platformData.Select(s => s.PlatformState).ToList();

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformState_{platformName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            Log.LogInformation($"Writing File : {fileNameFull}");

            platformStateData.WriteToCsvFile(fileNameFull);
        }
    }

    public void WritePlatformStateRelativeAllToCsv()
    {
        var platformDataRelative = SimulationData.PlatformDataRelativeAll;

        var platformStateDataRelative = platformDataRelative.ToList();

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformStateRelative_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        platformDataRelative.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformStateRelativePerPlatformToCsv()
    {
        foreach (var platformDataRelative in SimulationData.PlatformDataRelativePerPlatform)
        {
            var first = platformDataRelative.First();

            var originName = first.PlatformNameOrigin;
            var targetName = first.PlatformNameTarget;

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformStateRelative_O_{originName}_T_{targetName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            Log.LogInformation($"Writing File : {fileNameFull}");

            platformDataRelative.WriteToCsvFile(fileNameFull);
        }
    }

    public void WritePlatformStateMessagesAllToCsv()
    {
        var data = SimulationData.PlatformStateMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_PlatformState_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformStateMessagesPerPlatformToCsv()
    {
        foreach (var platformData in SimulationData.PlatformStateMessagesPerPlatform)
        {
            var first = platformData.First();

            var platformName = first.PlatformName;

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_PlatformState_{platformName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            Log.LogInformation($"Writing File : {fileNameFull}");

            platformData.WriteToCsvFile(fileNameFull);
        }
    }

    public void WritePlatformStateRelativeMessagesAllToCsv()
    {
        var data = SimulationData.PlatformStateRelativeMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_PlatformStateRelative_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformStateRelativeMessagesPerPlatformToCsv()
    {
        foreach (var psrm in SimulationData.PlatformStateRelativeMessagesPerPlatform)
        {
            var first = psrm.First();

            var originName = first.PlatformNameOrigin;
            var targetName = first.PlatformNameTarget;

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_PlatformDataRelative_O_{originName}_T_{targetName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            Log.LogInformation($"Writing File : {fileNameFull}");

            psrm.WriteToCsvFile(fileNameFull);
        }
    }

    public void WriteScanDataAllToCsv()
    {
        var data = SimulationData.ScanDataAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_ScanData_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WriteSensorReportsAllToCsv()
    {
        var data = SimulationData.SensorReportsAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SensorReports_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WriteTrackDataPredictedAllToCsv()
    {
        var data = SimulationData.TrackDataPredictedAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_TrackDataPredicted_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WriteScanDataMessagesAllToCsv()
    {
        var data = SimulationData.ScanDataMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_ScanData_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WriteSensorReportMessagesAllToCsv()
    {
        var data = SimulationData.SensorReportMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_SensorReports_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WriteTrackDataPredictedMessagesAllToCsv()
    {
        var data = SimulationData.TrackDataPredictedMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_TrackDataPredicted_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public string GetFileNameFull(string fileName)
    {
        var fileNameFull = SimulationData.SimulationRunSettings.GetFileNameFull(fileName);

        return fileNameFull;
    }
}