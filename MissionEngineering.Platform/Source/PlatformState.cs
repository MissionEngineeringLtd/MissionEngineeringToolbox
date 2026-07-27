using MissionEngineering.Core;
using MissionEngineering.Math;

namespace MissionEngineering.Platform;

public record PlatformState
{
    public SimulationTimeStamp TimeStamp { get; set; }

    public int PlatformId { get; set; }

    public string PlatformName { get; set; }

    public PositionLLA PositionLLA { get; set; }

    public double Altitude_ft => PositionLLA.Altitude_m.MetersToFeet();

    public double Altitude_FL => Altitude_ft.FeetToFlightLevel();

    public PositionNED PositionNED { get; set; }

    public VelocityNED VelocityNED { get; set; }

    public double TotalSpeed_kph => VelocityNED.TotalSpeed_ms.MetersPerSecondToKilometersPerHour();

    public double TotalSpeed_kts => VelocityNED.TotalSpeed_ms.MetersPerSecondToKnots();

    public AccelerationNED AccelerationNED { get; set; }

    public AccelerationTBA AccelerationTBA { get; set; }

    public Attitude Attitude { get; set; }

    public AttitudeRate AttitudeRate { get; set; }

    public double RangeToGo_m { get; set; }

    public double TimeToGo_s { get; set; }

    public bool IsActive { get; set; }

    public bool IsDestroyed { get; set; }

    public double HeadingAngleDemand_deg { get; set; }

    public double AltitudeDemand_m { get; set; }

    public double AltitudeDemand_ft => AltitudeDemand_m.MetersToFeet();

    public double AltitudeDemand_FL => AltitudeDemand_ft.FeetToFlightLevel();

    public double TotalSpeedDemand_ms { get; set; }

    public double TotalSpeedDemand_kph => TotalSpeedDemand_ms.MetersPerSecondToKilometersPerHour();

    public double TotalSpeedDemand_kts => TotalSpeedDemand_ms.MetersPerSecondToKnots();

    public double PitchAngleDemand_deg { get; set; }

    public double BankAngleDemand_deg { get; set; }
}