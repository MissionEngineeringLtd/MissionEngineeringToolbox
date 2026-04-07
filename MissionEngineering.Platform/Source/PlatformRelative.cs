using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public class PlatformRelative : IExecutableModel
{
    public Platform PlatformOrigin { get; set; }

    public Platform PlatformTarget { get; set; }

    public List<PlatformStateRelative> PlatformStatesRelative { get; set; }

    public PlatformRelative(Platform platformOrigin, Platform platformTarget)
    {
        PlatformOrigin = platformOrigin;

        PlatformTarget = platformTarget;

       PlatformStatesRelative = [];
    }

    public void Initialise(double time_s)
    {
    }

    public void Update(double time_s)
    {
        var platformStateOrigin = PlatformOrigin.PlatformState;
        var platformStateTarget = PlatformTarget.PlatformState;

        var platformStateRelative = PlatformFunctions.GeneratePlatformStateRelative(platformStateOrigin, platformStateTarget);

        PlatformStatesRelative.Add(platformStateRelative);
    }

    public void Finalise(double time_s)
    {
    }
}