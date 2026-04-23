namespace MissionEngineering.Simulation;

public record SimulationSettings
{
    public string SimulationName { get; set; }

    public int RunNumber { get; set; }

    public DateTime DateTime { get; set; }

    public bool IsAddConsoleLogging { get; set; }

    public bool IsAddFileLogging { get; set; }

    public bool IsWriteData { get; set; }

    public bool IsAddTimeStamp { get; set; }

    public bool IsAddRunNumber { get; set; }

    public bool IsCreateZipFile { get; set; }

    public string OutputFolderBase { get; set; }

    public string OutputFolder => GetOutputFolder();

    public string LogFileName => GetLogFileName();

    public SimulationSettings()
    {
    }

    public string GetOutputFolder()
    {
        var outputFolder = OutputFolderBase;

        outputFolder = Path.Combine(outputFolder, SimulationName);

        if (IsAddTimeStamp)
        {
            var dateTimeString = DateTime.ToString("yyyy-MM-dd HH-mm-ss");

            outputFolder = Path.Combine(outputFolder, dateTimeString);
        }

        if (IsAddRunNumber)
        {
            var runNumberString = $"Run_{RunNumber:D4}";

            outputFolder = Path.Combine(outputFolder, runNumberString);
        }

        return outputFolder;
    }

    public string GetLogFileName()
    {
        var logFileName = Path.Combine(OutputFolder, SimulationName + ".log");

        return logFileName;
    }

    public string GetFileNameFull(string fileName)
    {
        var fileNameFull = Path.Combine(OutputFolder, fileName);

        return fileNameFull;
    }
}