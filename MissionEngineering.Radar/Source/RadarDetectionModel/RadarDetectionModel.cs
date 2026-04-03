using static MissionEngineering.Radar.RadarRangeEquationFunctions;

namespace MissionEngineering.Radar;

public class RadarDetectionModel
{
    public RadarDetectionModelInputs Inputs { get; set; }

    public RadarDetectionModelOutputs Outputs { get; set; }

    public RadarDetectionModel()
    {
    }

    public void Run()
    {
        var signalPower_W = CalculateSignalPower_W(Inputs);

        var noisePower_W = CalculateNoisePower_W(Inputs);

        Outputs = new RadarDetectionModelOutputs()
        {
            SignalPower_W = signalPower_W,
            NoisePower_W = noisePower_W,
        };
    }
}