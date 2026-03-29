namespace MissionEngineering.MathLibrary;

public class LLAOrigin : ILLAOrigin
{
    public PositionLLA PositionLLA { get; set; }

    public LLAOrigin()
    {
        PositionLLA = new PositionLLA();
    }
}