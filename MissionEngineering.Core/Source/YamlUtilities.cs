using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MissionEngineering.Core;

public static class YamlUtilities
{
    public static string ConvertToYamlString<T>(this T obj)
    {
        var serializer = new SerializerBuilder()
            .Build();

        var yamlString = serializer.Serialize(obj);

        return yamlString;
    }

    public static T ConvertFromYamlString<T>(string yamlString)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        var obj = deserializer.Deserialize<T>(yamlString);

        return obj;
    }

    public static void WriteToYamlFile<T>(this T obj, string fileName, int padding = 0)
    {
        string yamlString = obj.ConvertToYamlString();

        File.WriteAllText(fileName, yamlString);
    }

    public static T ReadFromYamlFile<T>(string fileName)
    {
        var yamlString = File.ReadAllText(fileName);

        T obj = ConvertFromYamlString<T>(yamlString);

        return obj;
    }
}