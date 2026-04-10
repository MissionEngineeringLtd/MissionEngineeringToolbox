using MissionEngineering.Core;
using MissionEngineering.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Platform;

public class PlatformStateRelative
{
    public SimulationTimeStamp TimeStamp { get; set; }

    public int PlatformIdOrigin { get; set; }

    public string PlatformNameOrigin { get; set; }

    public int PlatformIdTarget { get; set; }

    public string PlatformNameTarget { get; set; }

    public PositionNED RelativePositionNED { get; set; }

    public VelocityNED RelativeVelocityNED { get; set; }

    public PositionNED RelativePositionLOS { get; set; }

    public VelocityNED RelativeVelocityLOS { get; set; }

    public Polars RelativePolarsNED { get; set; }

    public Polars RelativePolarsLOS { get; set; }

    public double AspectAngleAzimuth_deg { get; set; }

    public double AspectAngleElevation_deg { get; set; }
}
