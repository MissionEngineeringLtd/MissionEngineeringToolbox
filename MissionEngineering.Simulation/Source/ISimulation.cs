using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public interface ISimulation
{
    IDataRecorder DataRecorder { get; set; }

    ISimulationClock SimulationClock { get; set; }

    ILLAOrigin LLAOrigin { get; set; }

    SimulationSettings SimulationSettings { get; set; }

    ScenarioSettings ScenarioSettings { get; set; }

    ISimulationEventProcessor SimulationEventProcessor { get; set; }

    IPlatformManager PlatformManager { get; set; }

    List<IExecutableModel> SimulationModels { get; set; }

    ISimulation Run();

    void Initialise(double time);

    void Update(double time);

    void Finalise(double time);
}