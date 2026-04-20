using Serilog;
using Serilog.Core;

namespace MissionEngineering.Core;

public class LogClass : ILogClass
{
    public int RunNumber { get; set; }

    public Logger Logger { get; set; }

    public void CreateLogger(string fileName, bool isAddLogging)
    {
        var c = new LoggerConfiguration();

        if (isAddLogging)
        {
            var outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}";

            c.WriteTo.Console(outputTemplate: outputTemplate);
            c.WriteTo.File(fileName, outputTemplate: outputTemplate);
        }

        Logger = c.CreateLogger();
    }

    public void CloseLog()
    {
        Logger.Dispose();
    }

    public void LogInformation(string message, int padding = 0, params object?[]? propertyValues)
    {
        if (Logger is null) return;

        var paddingString = new string(' ', padding);

        var messageFull = paddingString + message;

        messageFull = $"[Run {RunNumber:D4}] {messageFull}";

        Logger.Information(messageFull, propertyValues);
    }

    public void LogError(string message, int padding = 0, params object?[]? propertyValues)
    {
        if (Logger is null) return;

        var paddingString = new string(' ', padding);

        var messageFull = paddingString + message;

        messageFull = $"[Run {RunNumber:D4}] {messageFull}";

        Logger.Error(messageFull, propertyValues);
    }
}
