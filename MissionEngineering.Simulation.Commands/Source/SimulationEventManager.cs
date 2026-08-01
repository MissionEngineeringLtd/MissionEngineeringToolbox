using YamlDotNet.Serialization;

namespace MissionEngineering.Simulation;

public static class SimulationEventManager
{
    public static List<ISimulationEvent> ReadEventsFromFile(string fileName)
    {
        var yamlString = File.ReadAllText(fileName);

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithTypeDiscriminatingNodeDeserializer(options =>
            {
                options.AddKeyValueTypeDiscriminator<ISimulationEvent>("EventType", new Dictionary<string, Type>(StringComparer.Ordinal)
                {
                    ["SimulationSettings"] = typeof(SimulationSettingsEvent),
                    ["MapOrigin"] = typeof(MapOriginEvent),
                    ["PlatformCreate"] = typeof(PlatformCreateEvent),
                    ["PlatformDelete"] = typeof(PlatformDeleteEvent),
                    ["PlatformAutopilot"] = typeof(PlatformAutopilotEvent),
                    ["PlatformLaunchMissile"] = typeof(PlatformLaunchMissileEvent),
                });
            })
            .Build();

        var events = deserializer.Deserialize<List<ISimulationEvent>>(yamlString);

        return events;
    }
}