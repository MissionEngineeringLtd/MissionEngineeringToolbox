namespace MissionEngineering.Simulation;

public class ZoneDeleteEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public string ZoneName { get; set; }

    public ZoneDeleteEvent()
    {
        EventType = SimulationEventType.ZoneDelete;
    }
}