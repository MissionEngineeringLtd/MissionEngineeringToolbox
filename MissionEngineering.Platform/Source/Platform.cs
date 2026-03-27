using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class Platform : IExecutableModel
{
    public ILLAOrigin LLAOrigin { get; set; }

    public PlatformSettings PlatformSettings { get; set; }

    public PlatformModel PlatformModel { get; set; }

    public PlatformState PlatformState { get; set; }

    public List<PlatformData> PlatformDataList { get; set; }

    public Platform(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;

        PlatformDataList = new List<PlatformData>();
    }

    public void Initialise(double time_s)
    {
        PlatformModel = new PlatformModel(LLAOrigin);

        PlatformState = PlatformSettings.PlatformState with { Time_s = time_s };

        Update(time_s);
    }

    public void Update(double time_s)
    {
        PlatformState = PlatformModel.Update(time_s, PlatformState);

        var platformData = new PlatformData
        {
             PlatformHeader = PlatformSettings.PlatformHeader,
             PlatformState = PlatformState
        };

        PlatformDataList.Add(platformData);
    }

    public void Finalise(double time_s)
    {
    }

}
