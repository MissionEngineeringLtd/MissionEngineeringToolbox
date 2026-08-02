namespace MissionEngineering.Radar;

public class RadarDetectionModelHarness
{
    public RadarDetectionModelHarnessInputs RadarDetectionModelHarnessInputs { get; set; }

    public RadarDetectionModel RadarDetectionModel { get; set; }

    public List<RadarDetectionModelData> RadarDetectionModelData { get; set; }

    public RadarDetectionModelHarness()
    {
    }

    public void Run()
    {
        RadarDetectionModelData = new List<RadarDetectionModelData>();

        var range = RadarDetectionModelHarnessInputs.TargetRangeMin_m;

        RadarDetectionModel = new RadarDetectionModel();

        while (range <= RadarDetectionModelHarnessInputs.TargetRangeMax_m)
        {
            var inputs = RadarDetectionModelHarnessInputs.RadarDetectionModelInputs with { TargetRange_m = range };

            RadarDetectionModel.Inputs = inputs;

            RadarDetectionModel.Run();

            var data = new RadarDetectionModelData()
            {
                Inputs = RadarDetectionModel.Inputs,
                Outputs = RadarDetectionModel.Outputs
            };

            RadarDetectionModelData.Add(data);

            range += RadarDetectionModelHarnessInputs.TargetRangeStep_m;
        }
    }
}