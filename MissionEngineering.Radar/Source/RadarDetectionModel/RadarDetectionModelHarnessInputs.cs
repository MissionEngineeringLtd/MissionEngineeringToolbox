namespace MissionEngineering.Radar;

public class RadarDetectionModelHarnessInputs
{
    public RadarDetectionModelInputs RadarDetectionModelInputs { get; set; }

    public double TargetRangeMin_m { get; set; }

    public double TargetRangeMax_m { get; set; }

    public double TargetRangeStep_m { get; set; }

    public double TargetRangeMin_km => TargetRangeMin_m / 1000.0;

    public double TargetRangeMax_km => TargetRangeMax_m / 1000.0;

    public double TargetRangeStep_km => TargetRangeStep_m / 1000.0;
}