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

    public SetpointController BankAngleController { get; set; }

    public AccelerationTBA AccelerationTBA { get; set; }

    public double PitchAngleDemand_deg { get; set; }

    public double BankAngleDemand_deg { get; set; }

    public double BankAngleRate_degs { get; set; }


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
            ControllerGain = PlatformDynamics.LateralAccelerationGain,
            IsAngleController = true
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

        BankAngleController = new SetpointController()
        {
            MinimumValue = -PlatformDynamics.BankAngleRateMax_degs,
            MaximumValue = PlatformDynamics.BankAngleRateMax_degs,
            ControllerGain = PlatformDynamics.BankAngleRateGain
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

        BankAngleDemand_deg = CalculateBankAngleFromLateralAcceleration(lateralAcceleration_ms2);

        BankAngleController.SetpointValue = BankAngleDemand_deg;
        BankAngleController.ActualValue = PlatformState.Attitude.BankAngle_deg;

        BankAngleRate_degs = BankAngleController.Update();

        var verticalAcceleration_ms2 = VerticalAccelerationController.Update();

        AccelerationTBA = new AccelerationTBA(axialAcceleration_ms2, lateralAcceleration_ms2, verticalAcceleration_ms2);
    }

    public void Finalise()
    {
    }


    public static double CalculateBankAngleFromLateralAcceleration(double lateralAcceleration_ms2)
    {
        var lateralAcceleration_g = lateralAcceleration_ms2.MetersPerSecondSquaredToG();

        var bankAngle_deg = MathFunctions.CalculateBankAngleDegFromLateralAcceleration(lateralAcceleration_g);

        return bankAngle_deg;
    }
}
