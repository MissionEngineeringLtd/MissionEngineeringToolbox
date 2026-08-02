using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public static class SimulationEventFactory
{
    public static List<ISimulationEvent> Example_1()
    {
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

        return new List<ISimulationEvent> { c2, c3, c4, c5, c6, c7, c8, c9, c10 };
    }

    public static List<ISimulationEvent> FF_1()
    { 
        var ff_1 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "FF_1",
            PlatformCallsign = "FF_1",
            PlatformDescription = "FF_1 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = -50000.0,
            PositionEast_m = 17000.0,
            Altitude_m = 12192.0,
            TotalSpeed_ms = 250.0,
            HeadingAngle_deg = 30,
            PitchAngle_deg = 5.0
        };

        var lw_1 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "LW_1",
            PlatformCallsign = "LW_1",
            PlatformDescription = "LW_1 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "rq-1b_predator",
            PlatformColor = "Blue",
            PositionNorth_m = -42000.0,
            PositionEast_m = 16000.0,
            Altitude_m = 11582.0,
            TotalSpeed_ms = 300.0,
            HeadingAngle_deg = 20.0,
            PitchAngle_deg = 0.0
        };

        var ff_2 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "FF_2",
            PlatformCallsign = "FF_2",
            PlatformDescription = "FF_2 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = -50000.0,
            PositionEast_m = 23000.0,
            Altitude_m = 12192.0,
            TotalSpeed_ms = 250.0,
            HeadingAngle_deg = 30.0,
            PitchAngle_deg = 0.0
        };

        var lw_2 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "LW_2",
            PlatformCallsign = "LW_2",
            PlatformDescription = "LW_2 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "rq-1b_predator",
            PlatformColor = "Blue",
            PositionNorth_m = -42000.0,
            PositionEast_m = 30000.0,
            Altitude_m = 11582.0,
            TotalSpeed_ms = 300.0,
            HeadingAngle_deg = 30.0,
            PitchAngle_deg = 0.0
        };

        var ff_3 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "FF_3",
            PlatformCallsign = "FF_3",
            PlatformDescription = "FF_3 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = -51000.0,
            PositionEast_m = 41000.0,
            Altitude_m = 12192.0,
            TotalSpeed_ms = 250.0,
            HeadingAngle_deg = 30.0,
            PitchAngle_deg = 0.0
        };

        var lw_3 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "LW_3",
            PlatformCallsign = "LW_3",
            PlatformDescription = "LW_3 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "rq-1b_predator",
            PlatformColor = "Blue",
            PositionNorth_m = -42000.0,
            PositionEast_m = 40000.0,
            Altitude_m = 11582.0,
            TotalSpeed_ms = 300.0,
            HeadingAngle_deg = 25.0,
            PitchAngle_deg = 0.0
        };

        var ff_4 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "FF_4",
            PlatformCallsign = "FF_4",
            PlatformDescription = "FF_4 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "f-35a_lightning",
            PlatformColor = "Blue",
            PositionNorth_m = -52000.0,
            PositionEast_m = 45000.0,
            Altitude_m = 12192.0,
            TotalSpeed_ms = 250.0,
            HeadingAngle_deg = 30.0,
            PitchAngle_deg = 0.0
        };

        var lw_4 = new PlatformCreateEvent()
        {
            EventTime = 0.0,
            PlatformName = "LW_4",
            PlatformCallsign = "LW_4",
            PlatformDescription = "LW_4 Description",
            PlatformAffiliation = PlatformAffiliationType.Friendly,
            PlatformType = PlatformType.Aircraft,
            PlatformIcon = "rq-1b_predator",
            PlatformColor = "Blue",
            PositionNorth_m = -43000.0,
            PositionEast_m = 52000.0,
            Altitude_m = 11582.0,
            TotalSpeed_ms = 350.0,
            HeadingAngle_deg = 25.0,
            PitchAngle_deg = 0.0
        };

        return new List<ISimulationEvent> { ff_1, lw_1, ff_2, lw_2, ff_3, lw_3, ff_4, lw_4 };
    }
}