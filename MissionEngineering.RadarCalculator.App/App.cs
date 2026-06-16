using System;
using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Core;         // BackdropKind
using Microsoft.UI.Reactor.Layout;        // FlexDirection, FlexJustify, FlexAlign
using Microsoft.UI.Xaml;                  // Thickness, HorizontalAlignment, VerticalAlignment
using Microsoft.UI.Xaml.Controls;         // Orientation, InfoBarSeverity, etc.
using MissionEngineering.Radar;
using static Microsoft.UI.Reactor.Factories;

ReactorApp.Run<App>("MissionEngineering.RadarCalculator.App", width: 900, height: 600);

class App : Component
{
    public override Element Render()
    {
        var waveformParameters1 = WaveformParametersExamples.Waveform_1();

        var (waveformParameters, setWaveformParameters) = UseState(waveformParameters1);

        var (name, setName) = UseState(waveformParameters.WaveformName);

        var (rfFrequency, setRFFrequency) = UseState(waveformParameters.RfFrequency_GHz);

        waveformParameters.RfFrequency_GHz = rfFrequency;

        var titleBar = TitleBar("MissionEngineering.RadarCalculator.App").Flex(shrink: 0);

        var body = Border(
            FlexColumn(
                Heading($"Hello, {waveformParameters.WaveformName}!"),
                TextBox(waveformParameters.WaveformName, setName, placeholderText: "Your name", "WaveformName").AutomationName("WaveformNameInput"),
                TextBox(waveformParameters.RfFrequency_GHz.ToString(), (string value) => setRFFrequency(double.Parse(value)), placeholderText: "Your name", "RF Frequency (GHz)").AutomationName("WaveformNameInput1"),
                TextBox(waveformParameters.RfFrequency_MHz.ToString(), null, placeholderText: "Your name", "RF Frequency (MHz)").AutomationName("WaveformNameInput2"),
                TextBox(waveformParameters.RfWavelength_m.ToString(), null, placeholderText: "Your name", "RF Wavelength (m)").AutomationName("WaveformNameInput3"),
                TextBox(waveformParameters.PulseWidth_us.ToString(), null, placeholderText: "Your name", "Pulse Width (μs)").AutomationName("WaveformNameInput4"),
                TextBox(waveformParameters.PulseRepetitionFrequency_Hz.ToString(), null, placeholderText: "Your name", "Pulse Repetition Frequency (Hz)").AutomationName("WaveformNameInput5"),
                TextBox(waveformParameters.RangeResolution_m.ToString(), null, placeholderText: "Your name", "Range Resolution (m)").AutomationName("WaveformNameInput6"),
                TextBox(name, setName, placeholderText: "Your name").AutomationName("NameInput")
            ) with { RowGap = 16 }
        ).Padding(24).Flex(grow: 1, basis: 0);

        return FlexColumn(titleBar, body)
            .Backdrop(BackdropKind.Mica);
    }
}
