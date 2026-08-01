using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public static class SimulationEventExamples
{
    public static List<ISimulationEvent> Example_1()
    {
        var c0 = new SimulationSettingsEvent()
        {
            EventTime = 0.0,
            SimulationStartDateTime = "2024-11-21T21:20:30Z",
            SimulationStartTime_s = 10.0,
            SimulationEndTime_s = 100.0,
            SimulationTimeStep_s = 0.1
        };

        var c1 = new MapOriginEvent()
        {
            EventTime = 0.0,
            Latitude_deg = 56.0,
            Longitude_deg = 12.5,
        };

        var c2 = new PlatformCreateEvent()
        {
            EventTime = 30.0,
            PlatformName = "FF_1",
            PlatformCallsign = "FF_1",
            PlatformDescription = "FF_1 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = 1000.0,
            PositionEast_m = 500.0,
            Altitude_m = 3000.0,
            TotalSpeed_ms = 250.0,
            HeadingAngle_deg = 45,
            PitchAngle_deg = 5.0
        };

        var c3 = new PlatformCreateEvent()
        {
            EventTime = 40.0,
            PlatformName = "FF_2",
            PlatformCallsign = "FF_2",
            PlatformDescription = "FF_2 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = 20000.0,
            PositionEast_m = 40000.0,
            Altitude_m = 5000.0,
            TotalSpeed_ms = 450.0,
            HeadingAngle_deg = 135.0,
            PitchAngle_deg = 5.0
        };

        var c4 = new PlatformAutopilotEvent()
        {
            EventTime = 50.0,
            PlatformName = "FF_2",
            HeadingAngleDemand_deg = 45.0,
            AltitudeDemand_FL = 250.0,
            TotalSpeedDemand_ms = 310.0
        };

        var c5 = new PlatformAutopilotEvent()
        {
            EventTime = 70.0,
            PlatformName = "FF_2",
            HeadingAngleDemand_deg = 135.0,
            AltitudeDemand_FL = 300,
            TotalSpeedDemand_ms = 250.0
        };

        var c6 = new PlatformAutopilotEvent()
        {
            EventTime = 80.0,
            PlatformName = "FF_1",
            HeadingAngleDemand_deg = 60,
            AltitudeDemand_FL = 120.0,
            TotalSpeedDemand_ms = 300.0
        };

        var c7 = new PlatformDeleteEvent()
        {
            EventTime = 150.0,
            PlatformName = "FF_1",
        };

        var c8 = new PlatformDeleteEvent()
        {
            EventTime = 160.0,
            PlatformName = "FF_2",
        };

        var c9 = new PlatformLaunchMissileEvent()
        {
            EventTime = 100.0,
            MissileName = "AMRAAM_1",
            LaunchPlatformName = "FF_1",
            TargetPlatformName = "RED_1",
            PlatformIcon = "aim-120_amraam",
            PlatformColor = "Green"
        };

        var c10 = new PlatformCreateEvent()
        {
            EventTime = 30.0,
            PlatformName = "RED_1",
            PlatformCallsign = "RED_1",
            PlatformDescription = "RED_1 Description",
            PlatformAffiliation = PlatformAffiliationType.Hostile,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = 80000.0,
            PositionEast_m = 20000.0,
            Altitude_m = 8000.0,
            TotalSpeed_ms = 250.0,
            HeadingAngle_deg = -135,
            PitchAngle_deg = 0.0
        };

        return new List<ISimulationEvent> { c0, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10 };
    }
}