using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class PlatformCreateCommand : SimulationCommand
{
    public PlatformCreateCommand()
    {
        CommandTime = 0.0;
        CommandType = SimulationCommandType.PlatformCreate;
        CommandData = new PlatformCreateCommandData();
    }
}