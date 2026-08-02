namespace MissionEngineering.Simulation;

public class PlatformDeleteEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public string PlatformName { get; set; }

    public PlatformDeleteEvent()
    {
        EventType = SimulationEventType.PlatformDelete;
    }
}