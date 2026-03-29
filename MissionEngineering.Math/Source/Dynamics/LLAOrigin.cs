namespace MissionEngineering.Math;

public class LLAOrigin : ILLAOrigin
{
    public PositionLLA PositionLLA { get; set; }

    public LLAOrigin()
    {
        PositionLLA = new PositionLLA();
    }
}