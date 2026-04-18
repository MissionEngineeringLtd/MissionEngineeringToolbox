using MissionEngineering.Core;
using MissionEngineering.Platform;

namespace MissionEngineering.Scanner;

public class ScannerHarness
{
    public ISimulationClock SimulationClock { get; set; }

    public ScanSettings ScanSettings { get; set; }

    public PlatformState PlatformState { get; set; }

    public double StartTime_s { get; set; }

    public double EndTime_s { get; set; }

    public double TimeStep_s { get; set; }

    public Scanner Scanner { get; set; }

    public List<ScanData> ScanData { get; set; }

    public ScannerHarness()
    {
    }

    public void Run()
    {
        ScanData = [];

        Scanner = new Scanner(SimulationClock)
        {
            ScanSettings = ScanSettings,
            PlatformState = PlatformState
        };

        var time_s = StartTime_s;

        Scanner.Initialise(time_s);

        while (time_s < EndTime_s)
        {
            Scanner.Update(time_s);

            ScanData.Add(Scanner.ScanData);

            time_s += TimeStep_s;
        }
    }
}