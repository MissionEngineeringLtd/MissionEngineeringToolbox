using Serilog.Core;

namespace MissionEngineering.Core;

public interface ILogClass
{
    int RunNumber { get; set; }

    Logger Logger { get; set; }

    void CreateLogger(string fileName, bool isAddConsoleLogging, bool isAddFileLogging);

    void LogInformation(string message, int padding = 0, params object?[]? propertyValues);

    void LogError(string message, int padding = 0, params object?[]? propertyValues);

    void CloseLog();
}
