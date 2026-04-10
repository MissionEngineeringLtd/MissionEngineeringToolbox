using MissionEngineering.Platform;

namespace MissionEngineering.Sensor;

public class SensorTargetIdentification
{
    public bool IsPositiveId { get; set; }

    public PlatformAffiliationType PlatformAffiliation { get; set; }

    public PlatformType PlatformType { get; set; }

    public string PlatformClass { get; set; }

    public string PlatformName { get; set; }
}