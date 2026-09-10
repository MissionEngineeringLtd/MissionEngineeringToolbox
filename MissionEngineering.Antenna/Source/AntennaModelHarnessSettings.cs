using MissionEngineering.Core;

namespace MissionEngineering.Antenna;

public class AntennaModelHarnessSettings
{
    public bool IsCreateExampleFile { get; set; }

    public string InputFolder { get; set; }

    public string OutputFolder { get; set; }

    public string InputFileName { get; set; }

    public string LogFileName { get; set; }

    public string OutputFileNameCsv { get; set; }

    public string OutputFileNameAprf { get; set; }

    public string OutputFileNameApbf { get; set; }

    public string ReportFileNameTex { get; set; }

    public string ReportFileNamePdf { get; set; }

    public string InputDataTableFileCsv { get; set; }

    public string InputDataTableName => Path.GetFileName(InputDataTableFileCsv);

    public void DisplaySettings()
    {
        LogUtilities.LogInformation($"   Antenna Model Harness Settings");
        LogUtilities.LogInformation($"      IsCreateExampleFile        = {IsCreateExampleFile}");
        LogUtilities.LogInformation($"      InputFolder                = {InputFolder}");
        LogUtilities.LogInformation($"      OutputFolder               = {OutputFolder}");
        LogUtilities.LogInformation($"      InputFileName              = {InputFileName}");
        LogUtilities.LogInformation($"      LogFile                    = {LogFileName}");
        LogUtilities.LogInformation($"      OutputFileCsv              = {OutputFileNameCsv}");
        LogUtilities.LogInformation($"      OutputFileAprf             = {OutputFileNameAprf}");
        LogUtilities.LogInformation($"      OutputFileApbf             = {OutputFileNameApbf}");
        LogUtilities.LogInformation($"      InputDataTableFileCsv      = {InputDataTableFileCsv}");
        LogUtilities.LogInformation($"      ReportFileTex              = {ReportFileNameTex}");
        LogUtilities.LogInformation($"      ReportFilePdf              = {ReportFileNamePdf}");
        LogUtilities.LogInformation($"   End.");
        LogUtilities.LogInformation($"");
    }
}