using MissionEngineering.Core;

namespace MissionEngineering.Platform;

public interface IPlatformManager : IExecutableModel
{
    List<Platform> Platforms { get; set; }

    void AddPlatform(Platform platform);

    Platform CreatePlatformMissile(Platform platformOrigin, string platformType, Platform platformTarget);

    void Initialise(double time_s);

    void Update(double time_s);

    void Finalise(double time_s);

    Platform GetPlatformByName(string platformName);
}