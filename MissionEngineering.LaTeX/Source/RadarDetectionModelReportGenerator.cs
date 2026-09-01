using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Radar;
using System.Reflection;

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

        WriteReportFileTex();

        WriteReportFilePdf();
    }

    public void GenerateReportData()
    {
        var currentUser = Environment.UserName.ConvertToDisplayString();
        var modelName = AppDomain.CurrentDomain.FriendlyName;
        var modelVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        ReportData = new Dictionary<string, string>();

        ReportData["XXX_InputsTableFileName"] = InputDataTableFileName;
        ReportData["XXX_OutputDataFileName"] = RadarDetectionModelHarnessOutputFileName;
        ReportData["XXX_CreatedBy"] = currentUser;
        ReportData["XXX_CreatedDate"] = DateTime.Now.ToString("dd MMM yyyy HH:mm:ss");
        ReportData["XXX_ModelName"] = modelName;
        ReportData["XXX_ModelVersion"] = modelVersion;
        ReportData["XXX_PowerLevelMin_dBW"] = "-150.0";
        ReportData["XXX_PowerLevelMax_dBW"] = "-40.0";
        ReportData["XXX_PowerLevelMin_dBmW"] = "-120.0";
        ReportData["XXX_PowerLevelMax_dBmW"] = "-10.0";
        ReportData["XXX_SignalToNoiseRatioMin_dB"] = "-20.0";
        ReportData["XXX_SignalToNoiseRatioMax_dB"] = "40.0";
    }

    public void GenerateReportInputDataTable()
    {
        var h = RadarDetectionModelHarness.RadarDetectionModelHarnessInputs;
        var i = h.RadarDetectionModelInputs;
        var w = i.WaveformParameters;

        InputDataTable =
        [
            new InputDataTableRow("System", "", "", ""),
            new InputDataTableRow("", i.SystemName.ConvertToDisplayString(), "", ""),
            new InputDataTableRow("", i.SystemProfile.ConvertToDisplayString(), "", ""),
            new InputDataTableRow("", i.RfSystemType.ToString(), "", ""),
            new InputDataTableRow("Transmitter", "", "", ""),
            new InputDataTableRow("", "RF Frequency", "Hz", w.RfFrequency_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kHz", w.RfFrequency_kHz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "MHz", w.RfFrequency_MHz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "GHz", w.RfFrequency_GHz.ToEngineeringFormat()),
            new InputDataTableRow("", "RF Wavelength", "m", w.RfWavelength_m.ToEngineeringFormat()),
            new InputDataTableRow("", "", "cm", w.RfWavelength_cm.ToEngineeringFormat()),
            new InputDataTableRow("", "", "mm", w.RfWavelength_mm.ToEngineeringFormat()),
            new InputDataTableRow("", "Transmit Power", "W", i.TransmitPeakPower_W.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kW", i.TransmitPeakPower_kW.ToEngineeringFormat()),
            new InputDataTableRow("", "", "MW", i.TransmitPeakPower_MW.ToEngineeringFormat()),
            new InputDataTableRow("", "", "mW", i.TransmitPeakPower_mW.ToEngineeringFormat()),
            new InputDataTableRow("", "", "dBW", i.TransmitPeakPower_dBW.ToEngineeringFormat()),
            new InputDataTableRow("", "", "dBmW", i.TransmitPeakPower_dBmW.ToEngineeringFormat()),
            new InputDataTableRow("", "Transmit Antenna Gain", "dB", i.TransmitGain_dB.ToEngineeringFormat()),
            new InputDataTableRow("", "EIRP", "dBW", i.EIRP_dBW.ToEngineeringFormat()),
            new InputDataTableRow("", "", "dBmW", i.EIRP_dBmW.ToEngineeringFormat()),
            new InputDataTableRow("", "System Losses", "dB", i.SystemLosses_dB.ToEngineeringFormat()),
            new InputDataTableRow("Waveform", "", "", ""),
            new InputDataTableRow("", "Pulse Width", "s", w.PulseWidth_s.ToEngineeringFormat()),
            new InputDataTableRow("", "", "ms", w.PulseWidth_ms.ToEngineeringFormat()),
            new InputDataTableRow("", "", "us", w.PulseWidth_us.ToEngineeringFormat()),
            new InputDataTableRow("", "", "ns", w.PulseWidth_ns.ToEngineeringFormat()),
            new InputDataTableRow("", "Pulse Bandwidth", "Hz", w.PulseBandwidth_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kHz", w.PulseBandwidth_kHz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "MHz", w.PulseBandwidth_MHz.ToEngineeringFormat()),
            new InputDataTableRow("", "Pulse Repetition Frequency", "Hz", w.PulseRepetitionFrequency_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kHz", w.PulseRepetitionFrequency_kHz.ToEngineeringFormat()),
            new InputDataTableRow("", "Pulse Repetition Interval", "s", w.PulseRepetitionInterval_s.ToEngineeringFormat()),
            new InputDataTableRow("", "", "ms", w.PulseRepetitionInterval_ms.ToEngineeringFormat()),
            new InputDataTableRow("", "", "us", w.PulseRepetitionInterval_us.ToEngineeringFormat()),
            new InputDataTableRow("", "Duty Ratio", "-", w.DutyRatio.ToFixedFormat(3)),
            new InputDataTableRow("", "", @"\%", w.DutyRatioPercent.ToFixedFormat(3)),
            new InputDataTableRow("", "Pulse Width (Uncompressed)", "m", w.UncompressedPulseWidth_m.ToEngineeringFormat()),
            new InputDataTableRow("", "Pulse Width (Compressed)", "m", w.CompressedPulseWidth_m.ToEngineeringFormat()),
            new InputDataTableRow("", "Pulse Compression Ratio", "-", w.PulseCompressionRatio.ToEngineeringFormat()),
            new InputDataTableRow("", "", "dB", w.PulseCompressionRatio_dB.ToEngineeringFormat()),
            new InputDataTableRow("", "Number Of Pulses", "-", w.NumberOfPulses.ToString()),
            new InputDataTableRow("", "Burst Time", "s", w.BurstTime_s.ToEngineeringFormat()),
            new InputDataTableRow("", "", "ms", w.BurstTime_ms.ToEngineeringFormat()),
            new InputDataTableRow("", "", "us", w.BurstTime_us.ToEngineeringFormat()),
            new InputDataTableRow("", "Maximum Unambiguous Range", "m", w.MaximumUnambiguousRange_m.ToEngineeringFormat()),
            new InputDataTableRow("", "", "km", w.MaximumUnambiguousRange_km.ToEngineeringFormat()),
            new InputDataTableRow("", "", "NM", w.MaximumUnambiguousRange_NM.ToEngineeringFormat()),
            new InputDataTableRow("", "Maximum Unambiguous Doppler", "Hz", w.MaximumUnambiguousDoppler_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kHz", w.MaximumUnambiguousDoppler_kHz.ToEngineeringFormat()),
            new InputDataTableRow("", "Maximum Unambiguous Range Rate", "m/s", w.MaximumUnambiguousRangeRate_ms.ToEngineeringFormat()),
            new InputDataTableRow("", "", "km/h", w.MaximumUnambiguousRangeRate_kph.ToEngineeringFormat()),
            new InputDataTableRow("", "", "knots", w.MaximumUnambiguousRangeRate_kts.ToEngineeringFormat()),
            new InputDataTableRow("", "Range Resolution", "m", w.RangeResolution_m.ToEngineeringFormat()),
            new InputDataTableRow("", "Doppler Resolution", "Hz", w.DopplerResolution_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "kHz", w.DopplerResolution_kHz.ToEngineeringFormat()),
            new InputDataTableRow("", "Velocity Resolution", "m/s", w.VelocityResolution_ms.ToEngineeringFormat()),
            new InputDataTableRow("", "", "km/h", w.VelocityResolution_kph.ToEngineeringFormat()),
            new InputDataTableRow("", "", "knots", w.VelocityResolution_kts.ToEngineeringFormat()),
            new InputDataTableRow("Receiver", "", "", ""),
            new InputDataTableRow("", "Receive Antenna Gain", "dB", i.ReceiveGain_dB.ToEngineeringFormat()),
            new InputDataTableRow("", "Receive Bandwidth", "Hz", w.PulseBandwidth_Hz.ToEngineeringFormat()),
            new InputDataTableRow("", "", "MHz", w.PulseBandwidth_MHz.ToEngineeringFormat()),
            new InputDataTableRow("", "Receive Noise Figure", "dB", i.ReceiverNoiseFigure_dB.ToEngineeringFormat()),
            new InputDataTableRow("Target", "", "", ""),
            new InputDataTableRow("", "Radar Cross Section", "m2", i.TargetRadarCrossSection_m2.ToEngineeringFormat()),
            new InputDataTableRow("", "", "dBsm", i.TargetRadarCrossSection_dBsm.ToEngineeringFormat()),
            new InputDataTableRow("Environment", "", "", ""),
            new InputDataTableRow("", "Atmospheric Loss (1 way)", "dB/km", i.AtmosphericLoss_dB_per_km.ToFixedFormat(3)),
            new InputDataTableRow("Harness", "", "", ""),
            new InputDataTableRow("", "Range Start", "km", h.TargetRangeMin_km.ToFixedFormat(3)),
            new InputDataTableRow("", "Range End", "km", h.TargetRangeMax_km.ToFixedFormat(3)),
            new InputDataTableRow("", "Range Step", "km", h.TargetRangeStep_km.ToFixedFormat(3)),
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

    public void WriteReportFileTex()
    {
        File.WriteAllText(ReportFileNameFull, TemplateData);
    }

    public void WriteReportFilePdf()
    {
        LaTexUtilities.ConvertTexToPdf(ReportFileNameFull);
    }
}