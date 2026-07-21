using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public interface ISimulationCommandProcessor : IExecutableModel
{
    ISimulationClock SimulationClock { get; set; }

    ILLAOrigin LLAOrigin { get; set; }

    IPlatformManager PlatformManager { get; set; }

    List<ISimulationCommand> SimulationCommands { get; set; }

    void Initialise(double time);

    void Update(double time);

    void Finalise(double time);
}