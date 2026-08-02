using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Core;         // BackdropKind
using MissionEngineering.Radar;
using static Microsoft.UI.Reactor.Factories;

public class WaveformComponent : Component<WaveformParameters>
{
    public WaveformParameters WaveformParameters { get; set; }

    public override Element Render()
    {
        WaveformParameters = Props;

        var (waveformParameters, setWaveformParameters) = UseState(WaveformParameters);

        var (name, setName) = UseState(waveformParameters.WaveformName);

        var (rfFrequency, setRFFrequency) = UseState(waveformParameters.RfFrequency_GHz);

        waveformParameters.RfFrequency_GHz = rfFrequency;

        var body = Border(
            FlexColumn(
                Heading("Waveform"),
                TextBox(waveformParameters.WaveformName, setName, placeholderText: "Empty", "WaveformName"),
                TextBox(waveformParameters.RfFrequency_GHz.ToString(), (string value) => setRFFrequency(double.Parse(value)), placeholderText: "Empty", "RF Frequency (GHz)"),
                TextBox(waveformParameters.RfFrequency_MHz.ToString(), null, placeholderText: "Empty", "RF Frequency (MHz)"),
                TextBox(waveformParameters.RfWavelength_m.ToString(), null, placeholderText: "Empty", "RF Wavelength (m)"),
                TextBox(waveformParameters.PulseWidth_us.ToString(), null, placeholderText: "Empty", "Pulse Width (μs)"),
                TextBox(waveformParameters.PulseWidth_ns.ToString(), null, placeholderText: "Empty", "Pulse Width (ns)"),
                TextBox(waveformParameters.PulseBandwidth_Hz.ToString(), null, placeholderText: "Empty", "Pulse Bandwidth (Hz)"),
                TextBox(waveformParameters.PulseBandwidth_MHz.ToString(), null, placeholderText: "Empty", "Pulse Bandwidth (MHz)"),
                TextBox(waveformParameters.PulseRepetitionFrequency_Hz.ToString(), null, placeholderText: "Empty", "Pulse Repetition Frequency (Hz)"),
                TextBox(waveformParameters.PulseRepetitionFrequency_kHz.ToString(), null, placeholderText: "Empty", "Pulse Repetition Frequency (kHz)"),
                TextBox(waveformParameters.PulseRepetitionInterval_s.ToString(), null, placeholderText: "Empty", "Pulse Repetition Interval (s)"),
                TextBox(waveformParameters.PulseRepetitionInterval_ms.ToString(), null, placeholderText: "Empty", "Pulse Repetition Interval (ms)"),
                TextBox(waveformParameters.NumberOfPulses.ToString(), null, placeholderText: "Empty", "Number Of Pulses"),
                TextBox(waveformParameters.BurstTime_s.ToString(), null, placeholderText: "Empty", "Burst Time (s)"),
                TextBox(waveformParameters.BurstTime_ms.ToString(), null, placeholderText: "Empty", "Burst Time (ms)"),
                TextBox(waveformParameters.RangeResolution_m.ToString(), null, placeholderText: "Empty", "Range Resolution (m)")
            ) with
            { RowGap = 16 }
        ).Padding(24).Flex(grow: 1, basis: 0);

        return FlexColumn(body)
            .Backdrop(BackdropKind.Mica);
    }
}