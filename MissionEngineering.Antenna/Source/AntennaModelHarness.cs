using MissionEngineering.Core;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Antenna;

public class AntennaModelHarness
{
    public AntennaModelHarnessSettings AntennaModelHarnessSettings { get; set; }

    public AntennaModelSettings AntennaModelSettings { get; set; }

    public AntennaModel AntennaModel { get; set; }

    public Logger Logger { get; set; }

    public void Run()
    {
        var ahs = AntennaModelHarnessSettings;

        ahs.InputFolder = Path.GetDirectoryName(ahs.InputFileName);
        ahs.OutputFolder = ahs.InputFolder;
        ahs.LogFileName = ahs.InputFileName.Replace(".yaml", ".log");
        ahs.OutputFileNameCsv = ahs.InputFileName.Replace(".yaml", ".csv");
        ahs.OutputFileNameAprf = ahs.InputFileName.Replace(".yaml", ".aprf");
        ahs.OutputFileNameApbf = ahs.InputFileName.Replace(".yaml", ".apbf");
        ahs.ReportFileNameTex = ahs.InputFileName.Replace(".yaml", "_Report.tex");
        ahs.ReportFileNamePdf = ahs.InputFileName.Replace(".yaml", "_Report.pdf");
        ahs.InputDataTableFileCsv = ahs.OutputFileNameCsv.Replace(".csv", "_InputDataTable.csv");

        DisplaySettings();

        if (ahs.IsCreateExampleFile)
        {
            WriteInputFile();
        }

        ReadInputFile();

        RunAntennaModel();

        WriteOutputFiles();

        //WriteReportFile();

        LogUtilities.LogInformation($"Finished.");
    }

    private void DisplaySettings()
    {
        AntennaModelHarnessSettings.DisplaySettings();
    }

    private void WriteInputFile()
    {
        var ahs = AntennaModelHarnessSettings;

        LogUtilities.LogInformation($"   Writing Input File...");

        AntennaModelSettings = AntennaModelSettingsExamples.Example_1();

        LogUtilities.LogInformation($"       {ahs.InputFileName}");

        AntennaModelSettings.WriteToYamlFile(ahs.InputFileName);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private void ReadInputFile()
    {
        var ahs = AntennaModelHarnessSettings;

        LogUtilities.LogInformation($"   Reading Input File...");

        if (string.IsNullOrEmpty(ahs.InputFileName))
        {
            LogUtilities.LogError($"      Input file name must not be empty.");
            return;
        }

        if (!File.Exists(ahs.InputFileName))
        {
            LogUtilities.LogError($"      Input file does not exist: {ahs.InputFileName}");
            return;
        }

        AntennaModelSettings = YamlUtilities.ReadFromYamlFile<AntennaModelSettings>(ahs.InputFileName);

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private void RunAntennaModel()
    {
        var ahs = AntennaModelHarnessSettings;

        LogUtilities.LogInformation($"   Running...");

        AntennaModel = new AntennaModel()
        {
            OutputFolder = ahs.OutputFolder,
            AntennaModelSettings = AntennaModelSettings
        };

        AntennaModel.GenerateAntenna();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private void WriteOutputFiles()
    {
        var ahs = AntennaModelHarnessSettings;

        LogUtilities.LogInformation($"   Writing Output Files...");

        LogUtilities.LogInformation($"       {ahs.OutputFileNameCsv}");

        AntennaModel.WriteAntennaPatternDataCsv();

        LogUtilities.LogInformation($"       {ahs.OutputFileNameAprf}");

        AntennaModel.WriteAntennaPatternAprf();

        LogUtilities.LogInformation($"       {ahs.OutputFileNameApbf}");

        AntennaModel.WriteAntennaPatternApbf();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }
}
