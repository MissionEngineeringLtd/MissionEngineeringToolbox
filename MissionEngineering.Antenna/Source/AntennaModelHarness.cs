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

        var fileNameBase = ahs.InputFileName.Replace(".yaml", "");
        fileNameBase = fileNameBase.Replace("_AntennaModelSettings", "");

        ahs.InputFolder = Path.GetDirectoryName(ahs.InputFileName);
        ahs.OutputFolder = ahs.InputFolder;
        ahs.LogFileName = fileNameBase + "_AntennaModel.log";
        ahs.OutputFileNameCsv = fileNameBase + "_AntennaPattern.csv";
        ahs.OutputFileNameAprf = fileNameBase + "_AntennaPattern.aprf";
        ahs.OutputFileNameApbf = fileNameBase + "_AntennaPattern.apbf";
        ahs.ReportFileNameTex = fileNameBase + "_AntennaModel_Report.tex";
        ahs.ReportFileNamePdf = fileNameBase + "_AntennaModel_Report.pdf";
        ahs.InputDataTableFileCsv = fileNameBase + "_AntennaModelSettingsTable.csv";

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
            AntennaModelSettings = AntennaModelSettings,
            AntennaModelHarnessSettings = AntennaModelHarnessSettings
        };

        AntennaModel.GenerateAntenna();

        LogUtilities.LogInformation($"   Finished.");
        LogUtilities.LogInformation($"");
    }

    private void WriteOutputFiles()
    {
        var ahs = AntennaModelHarnessSettings;

        if (!Directory.Exists(ahs.OutputFolder))
        {
            Directory.CreateDirectory(ahs.OutputFolder);
        }

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
