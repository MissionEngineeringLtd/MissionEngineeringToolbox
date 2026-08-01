using MissionEngineering.Math;
using MissionEngineering.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class PlatformLaunchMissileEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public string MissileName { get; set; }

    public string LaunchPlatformName { get; set; }

    public string TargetPlatformName { get; set; }  

    public string PlatformIcon { get; set; }

    public string PlatformColor { get; set; }

    public PlatformLaunchMissileEvent()
    {
        EventType = SimulationEventType.PlatformLaunchMissile;
    }
}