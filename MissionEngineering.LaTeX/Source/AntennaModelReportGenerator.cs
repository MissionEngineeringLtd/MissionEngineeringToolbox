using MissionEngineering.Antenna;
using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Radar;
using System.Reflection;

namespace MissionEngineering.LaTeX;

public class AntennaModelReportGenerator    
{
    public string OutputFolder { get; set; }

    public string ReportFileNameFull { get; set; }

    public Dictionary<string, string> ReportData { get; set; }

    public string TemplateData { get; set; }

    public string AntennaModelInputFileName { get; set; }

    public string AntennaModelOutputFileName { get; set; }

    public AntennaModel AntennaModel { get; set; }

    public List<InputDataTableRow> InputDataTable { get; set; }

    public string InputDataTableFileName { get; set; }

    public string InputDataTableFileNameFull { get; set; }

    public void GenerateReport()
    {
        GenerateReportData();

        GenerateReportInputDataTable();

        GenerateReportFile();

        WriteReportFileTex();

        WriteReportFilePdf();
    }

    public void GenerateReportData()
    {
        var currentUser = Environment.UserName.ConvertToDisplayString();
        var modelName = AppDomain.CurrentDomain.FriendlyName;
        var modelVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        var s = AntennaModel.AntennaModelSettings;

        ReportData = new Dictionary<string, string>();

        ReportData["XXX_InputsTableFileName"] = InputDataTableFileName;
        ReportData["XXX_OutputDataFileName"] = AntennaModelOutputFileName;
        ReportData["XXX_CreatedBy"] = currentUser;
        ReportData["XXX_CreatedDate"] = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
        ReportData["XXX_ModelName"] = modelName;
        ReportData["XXX_ModelVersion"] = modelVersion;
        ReportData["XXX_AzimuthAngleMin_deg"] = s.AzimuthAngleMin_deg.ToString();
        ReportData["XXX_AzimuthAngleMax_deg"] = s.AzimuthAngleMax_deg.ToString();
        ReportData["XXX_DirectivityNormalisedMin_dB"] = "-60.0";
        ReportData["XXX_DirectivityNormalisedMax_dB"] = "0.0";
    }

    public void GenerateReportInputDataTable()
    {
        var i = AntennaModel.AntennaModelSettings;

        InputDataTable =
        [
            new InputDataTableRow("System", "", "", ""),
            new InputDataTableRow("", i.AntennaName.ConvertToDisplayString(), "", ""),
            new InputDataTableRow("RF Frequency", "", "", ""),
            new InputDataTableRow("", "", "Hz", i.RfFrequency_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kHz", i.RfFrequency_kHz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "MHz", i.RfFrequency_MHz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "GHz", i.RfFrequency_GHz.ToEngineeringFormat()),
            new InputDataTableRow("", "RF Wavelength", "m", i.RfWavelength_m.ToEngineeringFormat()),
            new InputDataTableRow("", "", "cm", i.RfWavelength_cm.ToEngineeringFormat()),
            new InputDataTableRow("", "", "mm", i.RfWavelength_mm.ToEngineeringFormat()),
            new InputDataTableRow("Element", "", "", ""),
            new InputDataTableRow("", "Element Name", "dB", i.ElementName),
            new InputDataTableRow("Array", "", "", ""),
            new InputDataTableRow("", "Array Width", "m", i.AntennaWidth_m.ToFixedFormat()),
            new InputDataTableRow("", "", "ft", i.AntennaWidth_ft.ToFixedFormat()),
            new InputDataTableRow("", "Array Element Spacing", "m", i.AntennaElementSpacing_m.ToEngineeringFormat()),
            new InputDataTableRow("", "", "cm", i.AntennaElementSpacing_cm.ToFixedFormat()),
            new InputDataTableRow("", "", "mm", i.AntennaElementSpacing_mm.ToFixedFormat()),
            new InputDataTableRow("", "", "wavelengths", i.AntennaElementSpacing_wavelengths.ToFixedFormat()),
            new InputDataTableRow("", "Number of Array Elements", "-", i.NumberOfAntennaElements.ToString()),
            new InputDataTableRow("Losses", "", "", ""),
            new InputDataTableRow("", "Antenna Losses", "dB", i.AntennaLosses_dB.ToFixedFormat()),
            new InputDataTableRow("Azimuth", "", "", ""),
            new InputDataTableRow("", "Azimuth Start", "deg", i.AzimuthAngleMin_deg.ToFixedFormat(3)),
            new InputDataTableRow("", "Azimuth End", "deg", i.AzimuthAngleMax_deg.ToFixedFormat(3)),
            new InputDataTableRow("", "Azimuth Step", "deg", i.AzimuthAngleStep_deg.ToFixedFormat(3)),
        ];

        InputDataTable.WriteToCsvFile(InputDataTableFileNameFull);
    }

    public void GenerateReportFile()
    {
        TemplateData = Properties.Resources.AntennaModel_ReportTemplate;

        foreach (var item in ReportData)
        {
            TemplateData = TemplateData.Replace(item.Key, item.Value);
        }
    }

    public void WriteReportFileTex()
    {
        File.WriteAllText(ReportFileNameFull, TemplateData);
    }

    public void WriteReportFilePdf()
    {
        LaTexUtilities.ConvertTexToPdf(ReportFileNameFull);
    }
}