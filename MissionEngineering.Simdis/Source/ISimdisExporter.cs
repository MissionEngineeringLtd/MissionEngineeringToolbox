using MissionEngineering.Simulation;

namespace MissionEngineering.Simdis;

public interface ISimdisExporter
{
    SimulationData SimulationData { get; set; }

    void GenerateSimdisData();

    void WriteSimdisData();
}