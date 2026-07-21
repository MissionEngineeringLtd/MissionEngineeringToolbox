using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class SimulationCommandProcessor : ISimulationCommandProcessor
{
    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public IPlatformManager PlatformManager { get; set; }

    public IDataRecorder DataRecorder { get; set; }

    public List<ISimulationCommand> SimulationCommands { get; set; }

    public List<ISimulationCommand> SimulationCommandsForTimeStep { get; set; }

    public double LastProcessedTime { get; set; }

    public SimulationCommandProcessor(ISimulationClock simulationClock, ILLAOrigin llaOrigin, IPlatformManager platformManager, IDataRecorder dataRecorder)
    {
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
        PlatformManager = platformManager;
        DataRecorder = dataRecorder;

        LastProcessedTime = -1.0;
    }

    public void Initialise(double time)
    {
        SimulationCommands = SimulationCommands.OrderBy(s => s.CommandTime).ToList();
    }

    public void Update(double time)
    {
        GetSimulationCommandsForTimeStep(time);

        ProcessCommands(time);
    }

    public void ProcessCommands(double time)
    {
        foreach (var command in SimulationCommandsForTimeStep)
        {
            switch (command)
            {
                case SimulationSettingsCommand:
                    break;

                case MapOriginCommand:
                    break;

                case PlatformCreateCommand c:
                    var p = ConvertPlatformCommandToPlatform(c);
                    p.Initialise(time);
                    PlatformManager.AddPlatform(p);
                    DataRecorder.SimulationData.ScenarioSettings.PlatformSettingsList.Add(p.PlatformSettings);
                    break;

                default:
                    throw new NotImplementedException($"Command type {command.CommandType} is not implemented.");
            }
        }
    }

    public void Finalise(double time)
    {
    }

    public void GetSimulationCommandsForTimeStep(double time)
    {
        SimulationCommandsForTimeStep = SimulationCommands
            .Where(c => c.CommandTime > LastProcessedTime && c.CommandTime <= time)
            .ToList();

        LastProcessedTime = time;
    }

    public Platform.Platform ConvertPlatformCommandToPlatform(PlatformCreateCommand c)
    {
        var platformSettings = new PlatformSettings
        {
            PlatformHeader = new PlatformHeader()
            {
                PlatformId = c.PlatformId,
                PlatformName = c.PlatformName,
                PlatformDescription = c.PlatformDescription,
                PlatformCallsign = c.PlatformCallsign,
                PlatformType = c.PlatformType,
                PlatformAffiliation = c.PlatformAffiliation
            },
            PlatformHeaderSimdis = new PlatformHeaderSimdis()
            {
                PlatformAffiliationFHN = "F",
                PlatformColor = "GREEN",
                PlatformIcon = "f-35a_lightning",
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
}