namespace MissionEngineering.Simulation.Messages;

public record PlatformStateMessage : SimulationMessage
{
    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public bool IsPrediction { get; set; }

    public double LastUpdateTime_s { get; set; }

    public double PredictionTime_s { get; set; }

    public double PredictionTimeDelta_s { get; set; }

    public double Latitude_deg { get; set; }

    public double Longitude_deg { get; set; }

    public double Altitude_m { get; set; }

    public double PositionNorth_m { get; set; }

    public double PositionEast_m { get; set; }

    public double PositionDown_m { get; set; }

    public double VelocityNorth_ms { get; set; }

    public double VelocityEast_ms { get; set; }

    public double VelocityDown_ms { get; set; }

    public double AccelerationNorth_ms2 { get; set; }

    public double AccelerationEast_ms2 { get; set; }

    public double AccelerationDown_ms2 { get; set; }

    public double AccelerationAxial_ms2 { get; set; }

    public double AccelerationLateral_ms2 { get; set; }

    public double AccelerationVertical_ms2 { get; set; }

    public double HeadingAngle_deg { get; set; }

    public double PitchAngle_deg { get; set; }

    public double BankAngle_deg { get; set; }

    public double HeadingRate_deg { get; set; }

    public double PitchRate_deg { get; set; }

    public double BankRate_deg { get; set; }

    public bool IsActive { get; set; }

    public bool IsDestroyed { get; set; }

    public PlatformStateMessage()
    {
        MessageType = SimulationMessageType.PlatformState;
    }
}