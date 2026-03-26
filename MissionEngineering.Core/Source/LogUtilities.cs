using Serilog;

namespace MissionEngineering.Core;

public class LogUtilities
{
    public static void CreateLogger(string fileName)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(fileName, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }

    public static void CloseLog()
    {
        Log.CloseAndFlush();
    }

    public static void LogInformation(string message, int padding = 0, params object?[]? propertyValues)
    {
        var paddingString = new string(' ', padding);

        var messageFull = paddingString + message;

        Log.Information(messageFull, propertyValues);
    }
}