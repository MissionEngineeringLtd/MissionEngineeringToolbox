using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace MissionEngineering.Core;

public static class CsvUtilities
{
    public static void WriteToCsvFile<T>(this IEnumerable<T> records, string fileName, int padding = 0)
    {
        LogUtilities.LogInformation($"Writing     Csv  File : {fileName}", padding);

        using var writer = new StreamWriter(fileName);

        using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

        var options = new TypeConverterOptions { Formats = ["yyyy-MM-ddTHH:mm:ss.fffZ"] };
        csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

        csvWriter.WriteRecords(records);
    }

    public static void TransposeCsvFile(string inputFilePath, string outputFilePath, int padding = 0)
    {
        LogUtilities.LogInformation($"Transposing Csv  File : {outputFilePath}", padding);

        // Read all lines from the input CSV file
        var lines = File.ReadAllLines(inputFilePath);

        // Split each line into columns
        var data = lines.Select(line => line.Split(',')).ToArray();

        // Transpose the data
        var transposed = Enumerable.Range(0, data[0].Length)
                                   .Select(i => data.Select(row => row.ElementAtOrDefault(i) ?? "").ToArray())
                                   .ToArray();

        // Write the transposed data to the output CSV file
        var transposedLines = transposed.Select(row => string.Join(",", row));
        File.WriteAllLines(outputFilePath, transposedLines);
    }
}