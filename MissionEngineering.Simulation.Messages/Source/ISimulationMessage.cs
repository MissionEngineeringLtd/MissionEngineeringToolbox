namespace MissionEngineering.Simulation.Messages;

public interface ISimulationMessage
{
    int MessageTypeId { get; }

    SimulationMessageType MessageType { get; set; }

    SimulationMessageHeader Header { get; set; }
}