using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;

namespace MissionEngineering.Simulation;

public interface ISimulation
{
    IDataRecorder DataRecorder { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    ILLAOrigin LLAOrigin { get; set; }

    SimulationSettings SimulationSettings { get; set; }

    ScenarioSettings ScenarioSettings { get; set; }

    List<IExecutableModel> SimulationModels { get; set; }

    void Run();

    void Initialise(double time);

    void Update(double time);

    void Finalise(double time);
}