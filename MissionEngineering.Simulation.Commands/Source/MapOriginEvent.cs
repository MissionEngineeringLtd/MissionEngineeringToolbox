using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class MapOriginEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public double Latitude_deg { get; set; }

    public double Longitude_deg { get; set; }

    public MapOriginEvent()
    {
        EventType = SimulationEventType.MapOrigin;
    }
}