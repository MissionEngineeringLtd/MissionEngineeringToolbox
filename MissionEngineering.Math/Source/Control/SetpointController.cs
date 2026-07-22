using System;
using System.Collections.Generic;
using System.Text;

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

    public double Update()
    {
        Error = SetpointValue - ActualValue;

        ControlOutput = ControllerGain * Error;

        ControlOutput = MathFunctions.LimitWithinRange(MinimumValue, MaximumValue, ControlOutput);

        return ControlOutput;
    }
}