using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Simulation;

public interface ISimulation
{
    IDataRecorder DataRecorder { get; set; }

    ILLAOrigin LLAOrigin { get; set; }

    SimulationSettings SimulationSettings { get; set; }

    ScenarioSettings ScenarioSettings { get; set; }

    List<IExecutableModel> SimulationModels { get; set; }

    ISimulationClock SimulationClock { get; set; }

    void Run();

    void Initialise(double time);

    void Update(double time);

    void Finalise(double time);
}