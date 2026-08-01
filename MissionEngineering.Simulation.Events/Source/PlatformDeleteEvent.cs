using MissionEngineering.Math;
using MissionEngineering.Platform;
using System;
using System.Collections.Generic;
using System.Text;

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