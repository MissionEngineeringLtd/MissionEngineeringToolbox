using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class SimulationSettingsEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public string SimulationStartDateTime { get; set; }

    public double SimulationStartTime_s { get; set; }

    public double SimulationEndTime_s { get; set; }

    public double SimulationTimeStep_s { get; set; }

    public SimulationSettingsEvent()
    {
        EventType = SimulationEventType.SimulationSettings;
    }
}