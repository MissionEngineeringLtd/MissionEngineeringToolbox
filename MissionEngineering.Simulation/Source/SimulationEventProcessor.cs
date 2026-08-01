using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class SimulationEventProcessor : ISimulationEventProcessor
{
    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public IPlatformManager PlatformManager { get; set; }

    public IDataRecorder DataRecorder { get; set; }

    public List<ISimulationEvent> SimulationEvents { get; set; }

    public List<ISimulationEvent> SimulationEventsForTimeStep { get; set; }

    public double LastProcessedTime { get; set; }

    public int NextPlatformId { get; set; }

    public int NextPlatformIdMissile { get; set; }

    public SimulationEventProcessor(ISimulationClock simulationClock, ILLAOrigin llaOrigin, IPlatformManager platformManager, IDataRecorder dataRecorder)
    {
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
        PlatformManager = platformManager;
        DataRecorder = dataRecorder;

        LastProcessedTime = -1.0;

        NextPlatformId = 100;
        NextPlatformIdMissile = 200;
    }

    public void Initialise(double time)
    {
        SimulationEvents = SimulationEvents.OrderBy(s => s.EventTime).ToList();
    }

    public void Update(double time)
    {
        GetSimulationEventsForTimeStep(time);

        ProcessEvents(time);
    }

    public void ProcessEvents(double time)
    {
        foreach (var Event in SimulationEventsForTimeStep)
        {
            switch (Event)
            {
                case SimulationSettingsEvent:
                    break;

                case MapOriginEvent:
                    break;

                case PlatformCreateEvent c:
                    NextPlatformId++;
                    var p = ConvertPlatformEventToPlatform(c);
                    p.Initialise(time);
                    PlatformManager.CreatePlatform(p);
                    DataRecorder.SimulationData.ScenarioSettings.PlatformSettingsList.Add(p.PlatformSettings);
                    break;

                case PlatformLaunchMissileEvent c:
                    NextPlatformIdMissile++;
                    var pm = ConvertPlatformLaunchMissileEventToPlatform(c);
                    pm.Initialise(time);
                    pm.PlatformModel.PlatformAutopilot.PlatformFlightpathDemand.TotalSpeedDemand_ms = 1000.0;
                    PlatformManager.CreatePlatform(pm);
                    DataRecorder.SimulationData.ScenarioSettings.PlatformSettingsList.Add(pm.PlatformSettings);
                    break;

                case PlatformDeleteEvent c:
                    PlatformManager.DeletePlatform(c.PlatformName);
                    break;

                case PlatformAutopilotEvent c:
                    var platform = PlatformManager.GetPlatformByName(c.PlatformName);

                    UpdatePlatformAutopilot(platform, c);

                    break;

                default:
                    throw new NotImplementedException($"Event type {Event.EventType} is not implemented.");
            }
        }
    }

    public void Finalise(double time)
    {
    }

    public void GetSimulationEventsForTimeStep(double time)
    {
        SimulationEventsForTimeStep = SimulationEvents
            .Where(c => c.EventTime > LastProcessedTime && c.EventTime <= time)
            .ToList();

        LastProcessedTime = time;
    }

    public Platform.Platform ConvertPlatformEventToPlatform(PlatformCreateEvent c)
    {
        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader()
            {
                PlatformId = NextPlatformId,
                PlatformName = c.PlatformName,
                PlatformDescription = c.PlatformDescription,
                PlatformCallsign = c.PlatformCallsign,
                PlatformType = c.PlatformType,
                PlatformAffiliation = c.PlatformAffiliation
            },
            PlatformHeaderSimdis = new PlatformHeaderSimdis()
            {
                PlatformAffiliationFHN = "F",
                PlatformColor = c.PlatformColor,
                PlatformIcon = c.PlatformIcon,
                PlatformInterpolate = "1",
                PlatformScaleLevel = 2.5,
                PlatformType = "Aircraft"
            },
            PlatformStateInitial = new PlatformStateInitial()
            {
                PositionNorth_m = c.PositionNorth_m,
                PositionEast_m = c.PositionEast_m,
                Altitude_m = c.Altitude_m,
                TotalSpeed_ms = c.TotalSpeed_ms,
                HeadingAngle_deg = c.HeadingAngle_deg,
                PitchAngle_deg = c.PitchAngle_deg
            }
        };

        var platform = new Platform.Platform(SimulationClock, LLAOrigin)
        {
            PlatformSettings = platformSettings
        };

        return platform;
    }

    public Platform.Platform ConvertPlatformLaunchMissileEventToPlatform(PlatformLaunchMissileEvent c)
    {
        var platformLaunch = PlatformManager.GetPlatformByName(c.LaunchPlatformName);

        var ps = platformLaunch.PlatformSettings;
        var ph = ps.PlatformHeader;
        var pd = ps.PlatformHeaderSimdis;
        var pi = platformLaunch.PlatformState;

        var platformTarget = PlatformManager.GetPlatformByName(c.TargetPlatformName);

        var platformType = PlatformType.Missile;

        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader()
            {
                PlatformId = NextPlatformIdMissile,
                PlatformName = c.MissileName,
                PlatformDescription = ph.PlatformDescription,
                PlatformCallsign = ph.PlatformCallsign,
                PlatformType = platformType,
                PlatformAffiliation = ph.PlatformAffiliation
            },
            PlatformHeaderSimdis = new PlatformHeaderSimdis()
            {
                PlatformAffiliationFHN = pd.PlatformAffiliationFHN,
                PlatformColor = c.PlatformColor,
                PlatformIcon = c.PlatformIcon,
                PlatformInterpolate = "1",
                PlatformScaleLevel = 2.5,
                PlatformType = "Aircraft"
            },
            PlatformStateInitial = new PlatformStateInitial()
            {
                PositionNorth_m = pi.PositionNED.PositionNorth_m,
                PositionEast_m = pi.PositionNED.PositionEast_m,
                Altitude_m = pi.PositionLLA.Altitude_m,
                TotalSpeed_ms = pi.VelocityNED.TotalSpeed_ms,
                HeadingAngle_deg = pi.Attitude.HeadingAngle_deg,
                PitchAngle_deg = pi.Attitude.PitchAngle_deg
            }
        };

        var platform = new Platform.Platform(SimulationClock, LLAOrigin)
        {
            PlatformSettings = platformSettings,
            PlatformTarget = platformTarget
        };

        return platform;
    }

    public void UpdatePlatformAutopilot(Platform.Platform p, PlatformAutopilotEvent c)
    {
        var pd = p.PlatformModel.PlatformAutopilot.PlatformFlightpathDemand;

        pd.HeadingAngleDemand_deg = c.HeadingAngleDemand_deg;
        pd.AltitudeDemand_m = c.AltitudeDemand_m;
        pd.TotalSpeedDemand_ms = c.TotalSpeedDemand_ms;
    }
}