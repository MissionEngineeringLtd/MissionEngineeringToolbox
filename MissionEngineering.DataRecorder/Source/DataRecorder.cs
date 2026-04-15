using MissionEngineering.Core;
using MissionEngineering.Simdis;
using MissionEngineering.Simulation;

namespace MissionEngineering.DataRecorder;

public class DataRecorder : IDataRecorder
{
    public SimulationData SimulationData { get; set; }

    public ISimdisExporter SimdisExporter { get; set; }

    public DataRecorder(SimulationData simulationData, ISimdisExporter simdisExporter)
    {
        SimulationData = simulationData;
        SimdisExporter = simdisExporter;
    }

    public void Initialise(double time)
    {
    }

    public void Finalise(double time)
    {
        CreatePlatformDataPerPlatform();
        CreatePlatformDataRelativePerPlatform();

        CreatePlatformStateMessagesPerPlatform();
        CreatePlatformStateRelativeMessagesPerPlatform();

        WriteData();
    }

    public void WriteData()
    {
        if (!SimulationData.SimulationSettings.IsWriteData)
        {
            return;
        }

        CreateOutputFolder();

        WriteJsonData();
        WriteCsvData();
        WriteSimdisData();
    }

    public void CreateOutputFolder()
    {
        if (Directory.Exists(SimulationData.SimulationSettings.OutputFolder))
        {
            return;
        }

        Directory.CreateDirectory(SimulationData.SimulationSettings.OutputFolder);
    }

    public void CreatePlatformDataPerPlatform()
    {
        SimulationData.PlatformDataPerPlatform = [];

        var platformIds = SimulationData.ScenarioSettings.PlatformSettingsList.Select(s => s.PlatformHeader.PlatformId).Distinct();

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

        var platformIds = SimulationData.ScenarioSettings.PlatformSettingsList.Select(s => s.PlatformHeader.PlatformId).Distinct();

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
        WriteSimulationSettingsToJson();
        WriteScenarioSettingsToJson();
    }

    public void WriteCsvData()
    {
        WriteSimulationMessagesAllToCsv();

        WritePlatformStateAllToCsv();
        WritePlatformStatePerPlatformToCsv();

        WritePlatformStateRelativeAllToCsv();
        WritePlatformStateRelativePerPlatformToCsv();

        WritePlatformStateMessagesAllToCsv();
        WritePlatformStateMessagesPerPlatformToCsv();

        WritePlatformStateRelativeMessagesAllToCsv();
        WritePlatformStateRelativeMessagesPerPlatformToCsv();

        WriteSensorReportsAllToCsv();
    }

    public void WriteSimdisData()
    {
        SimdisExporter.GenerateSimdisData();
        SimdisExporter.WriteSimdisData();
    }

    public void WriteSimulationSettingsToJson()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationSettings.json";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Json File : {fileNameFull}");

        SimulationData.SimulationSettings.WriteToJsonFile(fileNameFull);
    }

    public void WriteScenarioSettingsToJson()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_ScenarioSettings.json";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Json File : {fileNameFull}");

        SimulationData.ScenarioSettings.WriteToJsonFile(fileNameFull);
    }

    public void WriteSimulationMessagesAllToCsv()
    {
        var data = SimulationData.SimulationMessages;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformStateAllToCsv()
    {
        var platformData = SimulationData.PlatformDataAll;

        var platformStateData = platformData.Select(s => s.PlatformState).ToList();

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformState_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

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

            LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

            platformStateData.WriteToCsvFile(fileNameFull);
        }
    }

    public void WritePlatformStateRelativeAllToCsv()
    {
        var platformDataRelative = SimulationData.PlatformDataRelativeAll;

        var platformStateDataRelative = platformDataRelative.ToList();

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformStateRelative_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

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

            LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

            platformDataRelative.WriteToCsvFile(fileNameFull);
        }
    }

    public void WritePlatformStateMessagesAllToCsv()
    {
        var data = SimulationData.PlatformStateMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_PlatformState_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

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

            LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

            platformData.WriteToCsvFile(fileNameFull);
        }
    }

    public void WritePlatformStateRelativeMessagesAllToCsv()
    {
        var data = SimulationData.PlatformStateRelativeMessagesAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_Messages_PlatformStateRelative_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

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

            LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

            psrm.WriteToCsvFile(fileNameFull);
        }
    }

    public void WriteSensorReportsAllToCsv()
    {
        var data = SimulationData.SensorReportsAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SensorReports_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

        data.WriteToCsvFile(fileNameFull);
    }

    public string GetFileNameFull(string fileName)
    {
        var fileNameFull = SimulationData.SimulationSettings.GetFileNameFull(fileName);

        return fileNameFull;
    }
}