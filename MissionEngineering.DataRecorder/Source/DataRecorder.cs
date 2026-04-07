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
        CreatePlatformData();
        CreatePlatformDataRelative();

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

    public void CreatePlatformData()
    {
        SimulationData.PlatformDataPerPlatform = [];

        foreach (var platformSettings in SimulationData.ScenarioSettings.PlatformSettingsList)
        {
            var platformId = platformSettings.PlatformHeader.PlatformId;

            var platformData = SimulationData.PlatformDataAll.Where(s => s.PlatformHeader.PlatformId == platformId).ToList();

            SimulationData.PlatformDataPerPlatform.Add(platformData);
        }
    }

    public void CreatePlatformDataRelative()
    {
        SimulationData.PlatformDataRelativePerPlatform = [];

        var platformIds = SimulationData.PlatformDataRelativeAll.Select(s => s.PlatformIdTarget).Distinct();

        foreach (var platformId in platformIds)
        {
            var platformDataRelative = SimulationData.PlatformDataRelativeAll.Where(s => s.PlatformIdTarget == platformId).ToList();

            SimulationData.PlatformDataRelativePerPlatform.Add(platformDataRelative);
        }
    }

    public void WriteJsonData()
    {
        WriteSimulationSettingsToJson();
        WriteScenarioSettingsToJson();
    }

    public void WriteCsvData()
    {
        WritePlatformDataAllToCsv();
        WritePlatformDataPerPlatformToCsv();

        WriteRelativePlatformDataAllToCsv();
        WriteRelativePlatformDataPerPlatformToCsv();
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

    public void WritePlatformDataAllToCsv()
    {
        var platformData = SimulationData.PlatformDataAll;

        var platformStateData = platformData.Select(s => s.PlatformState).ToList();

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformData_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

        platformStateData.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformDataPerPlatformToCsv()
    {
        int index = 0;

        foreach (var platformSettings in SimulationData.ScenarioSettings.PlatformSettingsList)
        {
            var platformData = SimulationData.PlatformDataPerPlatform[index];

            var platformStateData = platformData.Select(s => s.PlatformState).ToList();

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformData_{platformSettings.PlatformHeader.PlatformName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

            platformStateData.WriteToCsvFile(fileNameFull);

            index++;
        }
    }

    public void WriteRelativePlatformDataAllToCsv()
    {
        var platformDataRelative = SimulationData.PlatformDataRelativeAll;

        var platformStateDataRelative = platformDataRelative.ToList();

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformDataRelative_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

        platformDataRelative.WriteToCsvFile(fileNameFull);
    }

    public void WriteRelativePlatformDataPerPlatformToCsv()
    {
        foreach (var platformDataRelative in SimulationData.PlatformDataRelativePerPlatform)
        {
            var first = platformDataRelative.First();

            var originName = first.PlatformNameOrigin;
            var targetName = first.PlatformNameTarget;

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformDataRelative_O_{originName}_T_{targetName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            LogUtilities.LogInformation($"Writing     Csv  File : {fileNameFull}");

            platformDataRelative.WriteToCsvFile(fileNameFull);
        }
    }

    public string GetFileNameFull(string fileName)
    {
        var fileNameFull = SimulationData.SimulationSettings.GetFileNameFull(fileName);

        return fileNameFull;
    }
}