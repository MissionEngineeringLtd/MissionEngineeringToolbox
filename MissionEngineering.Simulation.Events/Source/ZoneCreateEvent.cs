namespace MissionEngineering.Simulation;

public class ZoneCreateEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public string ZoneName { get; set; }

    public string ZoneColor { get; set; }

    public double[] ZonePointsLatitude_DMS { get; set; }

    public double[] ZonePointsLongitude_DMS { get; set; }

    public double ZoneHeightMin_ft { get; set; }

    public double ZoneHeightMax_ft { get; set; }

    public ZoneCreateEvent()
    {
        EventType = SimulationEventType.ZoneCreate;
    }
}