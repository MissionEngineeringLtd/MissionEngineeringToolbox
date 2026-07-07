using System;
using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Core;         // BackdropKind
using Microsoft.UI.Reactor.Layout;        // FlexDirection, FlexJustify, FlexAlign
using Microsoft.UI.Xaml;                  // Thickness, HorizontalAlignment, VerticalAlignment
using Microsoft.UI.Xaml.Controls;         // Orientation, InfoBarSeverity, etc.
using MissionEngineering.Radar;
using static Microsoft.UI.Reactor.Factories;

public class SystemComponent : Component<RadarDetectionModelInputs>
{
    public RadarDetectionModelInputs Inputs { get; set; }

    public override Element Render()
    {
        Inputs = Props;

        var (inputs, setInputs) = UseState(Inputs);

        var (name, setName) = UseState(inputs.SystemName);

        var (transmitPower, setTransmitPower) = UseState(inputs.TransmitPeakPower_W);

        inputs.TransmitPeakPower_W = transmitPower;

        Inputs.TransmitPeakPower_W = transmitPower;

        var body = Border(
            FlexColumn(
                Heading("System"),
                TextBox(inputs.SystemName, setName, placeholderText: "Empty", "System Name"),
                TextBox(inputs.TransmitPeakPower_W.ToString(), (string value) => setTransmitPower(double.Parse(value)), placeholderText: "10.0", "Transmit Power (W)"),
                TextBox(inputs.TransmitPeakPower_dB.ToString(), null, placeholderText: "1.0", "Transmit Power (dBW)").IsReadOnly(true),
                TextBox(inputs.EIRP_dB.ToString(), null, placeholderText: "1.0",  "EIRP (dBW)").IsReadOnly(true)
            ) with { RowGap = 16 }
        ).Padding(24).Flex(grow: 1, basis: 0);

        return FlexColumn(body)
            .Backdrop(BackdropKind.Mica);
    }
}
