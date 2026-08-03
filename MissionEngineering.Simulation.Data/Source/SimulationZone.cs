namespace MissionEngineering.Simulation;

public class SimulationZone
{
    public string ZoneName { get; set; }

    public string ZoneColor { get; set; }

    public double[] ZonePointsLatitude_DMS { get; set; }

    public double[] ZonePointsLongitude_DMS { get; set; }

    public double ZoneHeightMin_ft { get; set; }

    public double ZoneHeightMax_ft { get; set; }
}