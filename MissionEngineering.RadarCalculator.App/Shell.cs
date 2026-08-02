using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Core;         // BackdropKind
using MissionEngineering.Radar;
using static Microsoft.UI.Reactor.Factories;

public class Shell : Component
{
    public RadarDetectionModelHarness Harness { get; set; }

    public Shell()
    {
        InitialiseHarness();
    }

    public void InitialiseHarness()
    {
        var harnessInputs = RadarDetectionModelHarnessInputExamples.Example_1();

        var harness = new RadarDetectionModelHarness()
        {
            RadarDetectionModelHarnessInputs = harnessInputs
        };

        if (harness.RadarDetectionModelData is null)
        {
            harness.Run();
        }

        Harness = harness;
    }

    public override Element Render()
    {
        var titleBar = TitleBar("Radar Calculator");

        var detectionModelInputs = Harness.RadarDetectionModelHarnessInputs.RadarDetectionModelInputs;
        var waveformParameters = detectionModelInputs.WaveformParameters;

        var c1 = Component<SystemComponent, RadarDetectionModelInputs>(detectionModelInputs);
        var c2 = Component<WaveformComponent, WaveformParameters>(waveformParameters);
        var c3 = Component<ChartComponent, RadarDetectionModelHarness>(Harness);

        Harness.Run();

        return ScrollView(
            HStack(24,
                Heading("Radar Calculator"),
                c1,
                c2,
                c3
            ).Padding(24)
        );
    }
}