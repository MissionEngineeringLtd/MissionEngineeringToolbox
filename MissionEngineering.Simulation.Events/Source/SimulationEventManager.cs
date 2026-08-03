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
                    ["PlatformCreate"] = typeof(PlatformCreateEvent),
                    ["PlatformDelete"] = typeof(PlatformDeleteEvent),
                    ["PlatformAutopilot"] = typeof(PlatformAutopilotEvent),
                    ["PlatformLaunchMissile"] = typeof(PlatformLaunchMissileEvent),
                    ["ZoneCreate"] = typeof(ZoneCreateEvent),
                    ["ZoneDelete"] = typeof(ZoneDeleteEvent)
                });
            })
            .Build();

        var events = deserializer.Deserialize<List<ISimulationEvent>>(yamlString);

        return events;
    }
}