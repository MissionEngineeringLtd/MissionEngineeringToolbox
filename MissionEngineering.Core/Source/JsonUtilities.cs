using System.Text.Json;

namespace MissionEngineering.Core;

public static class JsonUtilities
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };

    public static string ConvertToJsonString<T>(this T obj)
    {
        string jsonString = JsonSerializer.Serialize(obj, JsonSerializerOptions);

        return jsonString;
    }

    public static T ConvertFromJsonString<T>(string jsonString)
    {
        T obj = JsonSerializer.Deserialize<T>(jsonString, JsonSerializerOptions);

        return obj;
    }

    public static void WriteToJsonFile<T>(this T obj, string fileName, int padding = 0)
    {
        LogUtilities.LogInformation($"Writing     Json File : {fileName}", padding);

        string jsonString = obj.ConvertToJsonString();

        File.WriteAllText(fileName, jsonString);
    }

    public static T ReadFromJsonFile<T>(string fileName)
    {
        LogUtilities.LogInformation($"Reading     Json File : {fileName}");

        var jsonString = File.ReadAllText(fileName);

        T obj = ConvertFromJsonString<T>(jsonString);

        return obj;
    }
}