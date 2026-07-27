using MissionEngineering.Math;
using MissionEngineering.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class PlatformLaunchMissileCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public string MissileName { get; set; }

    public string LaunchPlatformName { get; set; }

    public string TargetPlatformName { get; set; }  

    public string PlatformIcon { get; set; }

    public string PlatformColor { get; set; }

    public PlatformLaunchMissileCommand()
    {
        CommandType = SimulationCommandType.PlatformLaunchMissile;
    }
}