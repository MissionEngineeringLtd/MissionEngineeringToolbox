using MissionEngineering.Math;
using MissionEngineering.Platform;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class PlatformDeleteCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public string PlatformName { get; set; }

    public PlatformDeleteCommand()
    {
        CommandType = SimulationCommandType.PlatformDelete;
    }
}