namespace MissionEngineering.Math;

public class SetpointController
{
    public double SetpointValue { get; set; }

    public double ActualValue { get; set; }

    public double MaximumValue { get; set; }

    public double MinimumValue { get; set; }

    public double ControllerGain { get; set; }

    public double Error { get; set; }

    public double ControlOutput { get; set; }

    public bool IsAngleController { get; set; } = false;

    public double Update()
    {
        Error = SetpointValue - ActualValue;

        if (IsAngleController)
        {
            Error = MathFunctions.ConstrainAnglePlusMinus180(Error);
        }

        ControlOutput = ControllerGain * Error;

        ControlOutput = MathFunctions.LimitWithinRange(MinimumValue, MaximumValue, ControlOutput);

        return ControlOutput;
    }
}