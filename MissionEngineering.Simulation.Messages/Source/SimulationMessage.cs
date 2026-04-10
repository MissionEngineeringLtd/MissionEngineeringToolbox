namespace MissionEngineering.Simulation.Messages;

public record SimulationMessage
{
    public int MessageTypeId => (int)MessageType;

    public SimulationMessageType MessageType { get; set; }

    public SimulationMessageHeader Header { get; set; }
}