using MissionEngineering.Core;

namespace MissionEngineering.Platform;

public class PlatformManager : IExecutableModel
{
    public List<Platform> Platforms { get; set; }

    public PlatformManager()
    {
        Platforms = new List<Platform>();
    }

    public void AddPlatform(Platform platform)
    {
        Platforms.Add(platform);
    }

    public void Initialise(double time_s)
    {
        foreach (var platform in Platforms)
        {
            platform.Initialise(time_s);
        }
    }

    public void Update(double time_s)
    {
        foreach (var platform in Platforms)
        {
            platform.Update(time_s);
        }
    }

    public void Finalise(double time_s)
    {
        foreach (var platform in Platforms)
        {
            platform.Finalise(time_s);
        }
    }

    public Platform CreatePlatformMissile(Platform platformOrigin, string platformType, Platform platformTarget)
    {
        var ps = platformOrigin.PlatformState;

        var platform = new Platform(platformOrigin.SimulationClock, platformOrigin.LLAOrigin)
        {
            LLAOrigin = platformOrigin.LLAOrigin,
            PlatformSettings = new PlatformSettings()
            {
                PlatformHeader = new PlatformHeader()
                {
                    PlatformAffiliation = PlatformAffiliationType.Friendly,
                    PlatformCallsign = "Missile",
                    PlatformDescription = "Missile",
                    PlatformId = 2001,
                    PlatformName = "Missile XXX",
                    PlatformType = PlatformType.Missile
                },
                PlatformHeaderSimdis = new PlatformHeaderSimdis()
                {
                    PlatformAffiliationFHN = "F",
                    PlatformColor = "Red",
                    PlatformIcon = "Air Missile",
                    PlatformInterpolate = "True",
                    PlatformScaleLevel = 1.0,
                    PlatformType = "Missile"
                },
                PlatformStateInitial = new PlatformStateInitial()
                {
                    PositionNorth_m = ps.PositionNED.PositionNorth_m,
                    PositionEast_m = ps.PositionNED.PositionEast_m,
                    Altitude_m = ps.PositionLLA.Altitude_m,
                    HeadingAngle_deg = ps.Attitude.HeadingAngle_deg,
                    PitchAngle_deg = ps.Attitude.PitchAngle_deg,
                    TotalSpeed_ms = ps.VelocityNED.TotalSpeed_ms
                }
            }
        };

        return platform;
    }
}
