using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class PlatformCreateEvent : ISimulationEvent
{
    public SimulationEventType EventType { get; set; }

    public double EventTime { get; set; }

    public string PlatformName { get; set; }

    public string PlatformCallsign { get; set; }

    public string PlatformDescription { get; set; }

    public PlatformType PlatformType { get; set; }

    public PlatformAffiliationType PlatformAffiliation { get; set; }

    public string PlatformIcon { get; set; }

    public string PlatformColor { get; set; }

    public double PositionNorth_m { get; set; }

    public double PositionEast_m { get; set; }

    public double Altitude_m { get; set; }

    public double Altitude_ft { get => Altitude_m.MetersToFeet(); set => Altitude_m = value.FeetToMeters(); }

    public double Altitude_FL { get => Altitude_ft.FeetToFlightLevel(); set => Altitude_ft = value.FlightLevelToFeet(); }

    public double TotalSpeed_ms { get; set; }

    public double HeadingAngle_deg { get; set; }

    public double PitchAngle_deg { get; set; }

    public PlatformCreateEvent()
    {
        EventType = SimulationEventType.PlatformCreate;
    }
}