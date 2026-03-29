using MissionEngineering.Simdis;
using MissionEngineering.Simulation;

namespace MissionEngineering.DataRecorder;

public interface IDataRecorder
{
    SimulationData SimulationData { get; set; }

    public ISimdisExporter SimdisExporter { get; set; }

    void Initialise(double time);

    void Finalise(double time);

    string GetFileNameFull(string fileName);
}