using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class MapOriginCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public double Latitude_deg { get; set; }

    public double Longitude_deg { get; set; }

    public MapOriginCommand()
    {
        CommandType = SimulationCommandType.MapOrigin;
    }
}