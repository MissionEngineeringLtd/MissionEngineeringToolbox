using MissionEngineering.Core;
using MissionEngineering.Radar;

namespace MissionEngineering.LaTeX;

public record InputDataTableRow(string Category, string Name, string Units, string Value);

public class RadarDetectionModelReportGenerator
{
    public string OutputFolder { get; set; }

    public string ReportFileNameFull { get; set; }

    public Dictionary<string, string> ReportData { get; set; }

    public string TemplateData { get; set; }

    public string RadarDetectionModelHarnessInputFileName { get; set; }

    public string RadarDetectionModelHarnessOutputFileName { get; set; }

    public RadarDetectionModelHarness RadarDetectionModelHarness { get; set; }

    public List<InputDataTableRow> InputDataTable { get; set; }

    public string InputDataTableFileName { get; set; }

    public string InputDataTableFileNameFull { get; set; }

    public void GenerateReport()
    {
        GenerateReportData();

        GenerateReportInputDataTable();

        GenerateReportFile();

        WriteReportFile();
    }

    public void GenerateReportData()
    {
        ReportData = new Dictionary<string, string>();

        ReportData["XXX_1"] = InputDataTableFileName;
    }

    public void GenerateReportInputDataTable()
    {
        var i = RadarDetectionModelHarness.RadarDetectionModelHarnessInputs.RadarDetectionModelInputs;
        var w = i.WaveformParameters;

        InputDataTable =
        [
            new InputDataTableRow("System", "RF Frequency", "Hz", w.RfFrequency_Hz.ToString()),
            new InputDataTableRow("", "", "MHz", w.RfFrequency_MHz.ToString()),
            new InputDataTableRow("", "", "GHz", w.RfFrequency_GHz.ToString()),
            new InputDataTableRow("", "RF Wavelength", "m", w.RfWavelength_m.ToString()),
            new InputDataTableRow("", "", "cm", w.RfWavelength_cm.ToString()),
            new InputDataTableRow("", "", "mm", w.RfWavelength_mm.ToString()),
            new InputDataTableRow("Waveform", "Pulse Width", "s", w.PulseWidth_s.ToString()),
            new InputDataTableRow("", "", "us", w.PulseWidth_us.ToString()),
            new InputDataTableRow("", "", "ns", w.PulseWidth_ns.ToString()),
            new InputDataTableRow("", "Pulse Bandwidth", "Hz", w.PulseBandwidth_Hz.ToString()),
            new InputDataTableRow("", "", "MHz", w.PulseBandwidth_MHz.ToString()),
        ];

        InputDataTable.WriteToCsvFile(InputDataTableFileNameFull);
    }

    public void GenerateReportFile()
    {
        TemplateData = Properties.Resources.RadarDetectionModel_ReportTemplate;

        foreach (var item in ReportData)
        {
            TemplateData = TemplateData.Replace(item.Key, item.Value);
        }
    }

    public void WriteReportFile()
    {
        File.WriteAllText(ReportFileNameFull, TemplateData);
    }
}