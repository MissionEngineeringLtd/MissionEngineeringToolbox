using MissionEngineering.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Platform;

public class PlatformAutopilot : IPlatformAutopilot
{
    public PlatformState PlatformState { get; set; }

    public SetpointController AxialAccelerationController { get; set; }

    public SetpointController LateralAccelerationController { get; set; }

    public SetpointController VerticalAccelerationController { get; set; }

    public AccelerationTBA AccelerationTBA { get; set; }

    public void Initialise()
    {
        AxialAccelerationController = new SetpointController()
        { 
            MinimumValue = -20.0, 
            MaximumValue = 20.0, 
            ControllerGain = 10.0 
        };

        LateralAccelerationController = new SetpointController()
        {
            MinimumValue = -50.0,
            MaximumValue = 50.0,
            ControllerGain = 10.0
        };

        VerticalAccelerationController = new SetpointController()
        {
            MinimumValue = -20.0,
            MaximumValue = 20.0,
            ControllerGain = 10.0
        };
    }

    public void Update()
    {
        AxialAccelerationController.ActualValue = PlatformState.VelocityNED.TotalSpeed_ms;
        AxialAccelerationController.SetpointValue = 300.0;

        LateralAccelerationController.ActualValue = 0.0;
        LateralAccelerationController.SetpointValue = 0.0;

        VerticalAccelerationController.ActualValue = 0.0;
        VerticalAccelerationController.SetpointValue = 0.0;

        var axialAcceleration_ms2 = AxialAccelerationController.Update();
        var lateralAcceleration_ms2 = LateralAccelerationController.Update();
        var verticalAcceleration_ms2 = VerticalAccelerationController.Update();

        AccelerationTBA = new AccelerationTBA(axialAcceleration_ms2, lateralAcceleration_ms2, verticalAcceleration_ms2);
    }

    public void Finalise()
    {
    }
}
