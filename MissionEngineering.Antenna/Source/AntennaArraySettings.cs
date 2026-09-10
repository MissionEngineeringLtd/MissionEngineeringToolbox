using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Antenna;

public class AntennaArrayPattern
{
    public int NumberOfAntennaElements { get; set; }

    public Vector AzimuthAngles_deg { get; set; }

    public int NumberOfAzimuthAngles => AzimuthAngles_deg.NumberOfElements;

    public VectorComplex AntennaWeights { get; set; }

    public VectorComplex ArrayFactor { get; set; }

    public VectorComplex ElementFactor { get; set; }

    public VectorComplex AntennaFactor { get; set; }

    public Vector ArrayFactor_dB { get; set; }

    public Vector ElementFactor_dB { get; set; }
}
