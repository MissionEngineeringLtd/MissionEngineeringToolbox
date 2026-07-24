using YamlDotNet.Serialization;

namespace MissionEngineering.Simulation;

public static class SimulationCommandManager
{
    public static List<ISimulationCommand> ReadCommandsFromFile(string fileName)
    {
        var yamlString = File.ReadAllText(fileName);

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithTypeDiscriminatingNodeDeserializer(options =>
            {
                options.AddKeyValueTypeDiscriminator<ISimulationCommand>("CommandType", new Dictionary<string, Type>(StringComparer.Ordinal)
                {
                    ["SimulationSettings"] = typeof(SimulationSettingsCommand),
                    ["MapOrigin"] = typeof(MapOriginCommand),
                    ["PlatformCreate"] = typeof(PlatformCreateCommand),
                    ["PlatformAutopilot"] = typeof(PlatformAutopilotCommand)
                });
            })
            .Build();

        var commands = deserializer.Deserialize<List<ISimulationCommand>>(yamlString);

        return commands;
    }
}