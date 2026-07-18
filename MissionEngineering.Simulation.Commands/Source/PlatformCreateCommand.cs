using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class PlatformCreateCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public double PositionNorth_m { get; set; }

    public double PositionEast_m { get; set; }

    public double Altitude_m { get; set; }

    public double TotalSpeed_ms { get; set; }

    public double HeadingAngle_deg { get; set; }

    public double PitchAngle_deg { get; set; }

    public PlatformCreateCommand()
    {
        CommandType = SimulationCommandType.PlatformCreate;
    }
}