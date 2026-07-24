using MissionEngineering.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Platform;

public class PlatformAutopilot : IPlatformAutopilot
{
    public PlatformState PlatformState { get; set; }

    public PlatformFlightpathDemand PlatformFlightpathDemand { get; set; }

    public PlatformDynamics PlatformDynamics { get; set; }

    public SetpointController AxialAccelerationController { get; set; }

    public SetpointController LateralAccelerationController { get; set; }

    public SetpointController VerticalAccelerationController { get; set; }

    public SetpointController PitchAngleController { get; set; }

    public AccelerationTBA AccelerationTBA { get; set; }

    public double PitchAngleDemand_deg { get; set; }


    public void Initialise()
    {
        PlatformDynamics = new PlatformDynamics();

        AxialAccelerationController = new SetpointController()
        { 
            MinimumValue = -PlatformDynamics.AxialAccelerationMax_ms2, 
            MaximumValue = PlatformDynamics.AxialAccelerationMax_ms2, 
            ControllerGain = PlatformDynamics.AxialAccelerationGain 
        };

        LateralAccelerationController = new SetpointController()
        {
            MinimumValue = -PlatformDynamics.LateralAccelerationMax_ms2,
            MaximumValue = PlatformDynamics.LateralAccelerationMax_ms2,
            ControllerGain = PlatformDynamics.LateralAccelerationGain
        };

        VerticalAccelerationController = new SetpointController()
        {
            MinimumValue = -PlatformDynamics.VerticalAccelerationMax_ms2,
            MaximumValue = PlatformDynamics.VerticalAccelerationMax_ms2,
            ControllerGain = PlatformDynamics.VerticalAccelerationGain
        };

        PitchAngleController = new SetpointController()
        {
            MinimumValue = -PlatformDynamics.PitchAngleMax_deg,
            MaximumValue = PlatformDynamics.PitchAngleMax_deg,
            ControllerGain = PlatformDynamics.PitchAngleGain
        };
    }

    public void Update()
    {
        AxialAccelerationController.SetpointValue = PlatformFlightpathDemand.TotalSpeedDemand_ms;
        AxialAccelerationController.ActualValue = PlatformState.VelocityNED.TotalSpeed_ms;

        LateralAccelerationController.SetpointValue = PlatformFlightpathDemand.HeadingAngleDemand_deg;
        LateralAccelerationController.ActualValue = PlatformState.Attitude.HeadingAngle_deg;

        PitchAngleController.SetpointValue = PlatformFlightpathDemand.AltitudeDemand_m;
        PitchAngleController.ActualValue = PlatformState.PositionLLA.Altitude_m;

        var axialAcceleration_ms2 = AxialAccelerationController.Update();
        var lateralAcceleration_ms2 = LateralAccelerationController.Update();
        
        PitchAngleDemand_deg = PitchAngleController.Update();

        VerticalAccelerationController.SetpointValue = PitchAngleDemand_deg;
        VerticalAccelerationController.ActualValue = PlatformState.Attitude.PitchAngle_deg;

        var verticalAcceleration_ms2 = VerticalAccelerationController.Update();

        AccelerationTBA = new AccelerationTBA(axialAcceleration_ms2, lateralAcceleration_ms2, verticalAcceleration_ms2);
    }

    public void Finalise()
    {
    }
}
