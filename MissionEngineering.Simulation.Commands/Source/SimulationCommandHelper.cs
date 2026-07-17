using System.Net;
using YamlDotNet.Serialization;

namespace MissionEngineering.Simulation;

public static class SimulationCommandHelper
{
    public static List<SimulationCommand> ReadCommandsFromFile(string fileName)
    {
        var yamlString = File.ReadAllText(fileName);

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithTypeDiscriminatingNodeDeserializer(options =>
            {
                options.AddKeyValueTypeDiscriminator<SimulationCommand>("CommandTypeString", new Dictionary<string, Type>(StringComparer.Ordinal)
                {
                    ["PlatformCreate"] = typeof(PlatformCreateCommand),
                });
                options.AddKeyValueTypeDiscriminator<SimulationCommandData>("CommandTypeString", new Dictionary<string, Type>(StringComparer.Ordinal)
                {
                    ["PlatformCreate"] = typeof(PlatformCreateCommandData),
                });
            })
            .Build();

        var commands = deserializer.Deserialize<List<SimulationCommand>>(yamlString);

        return commands;
    }
}