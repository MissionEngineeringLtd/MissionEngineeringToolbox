using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Charting;
using Microsoft.UI.Reactor.Charting.Accessibility;
using Microsoft.UI.Reactor.Charting.D3;
using Microsoft.UI.Reactor.Core;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MissionEngineering.Radar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.UI.Reactor.Charting.Charts;
using static Microsoft.UI.Reactor.Factories;

public class ChartComponent : Component<RadarDetectionModelHarness>
{
    public RadarDetectionModelHarness Harness { get; set; }

    public record DataPoint(double X, double Y);

    public override Element Render()
    {
        var Harness = Props;

        var (saved, setSaved) = UseState(false);

        var updateCommand = new Command
        {
            Label = "Update",
            Execute = () => { Harness.Run(); setSaved(!saved); },
        };

        Harness.Run();

        var data = Harness.RadarDetectionModelData.Select(d => new DataPoint(d.Inputs.TargetRange_km, d.Outputs.SignalToNoiseRatio_dB)).ToList();

        var lineChart = LineChart(data, d => d.X, d => d.Y)
            .Title("Signal To Noise Ratio (dB)")
            .AxisLabel(ChartAxisType.X, "Target Range (km)")
            .AxisLabel(ChartAxisType.Y, "Signal To Noise Ratio (dB)")
            .Units("km", "dB")
            //XTickLabelView(t => VStack(0,
            //    (TextBlock($"{Math.Round(t):F0}") with { FontSize = 12 }).FontWeight(FontWeights.SemiBold))).
            //YTickLabelView(t => VStack(0,
            //    (TextBlock($"{Math.Round(t):F0}") with { FontSize = 8 }).FontWeight(FontWeights.SemiBold))).
            .ShowAxes(true).ShowGrid(true)
            .Width(800).Height(400);

        var body = Border(
            FlexColumn(
                Heading("Signal To Noise Ratio"),
                Button(updateCommand),
                lineChart
            ) with { RowGap = 16 }
        ).Padding(0).Flex(grow: 1, basis: 0);

        return FlexColumn(body)
            .Backdrop(BackdropKind.Mica);
    }
}
