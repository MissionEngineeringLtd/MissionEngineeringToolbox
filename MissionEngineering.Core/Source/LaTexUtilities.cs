using System.Diagnostics;

namespace MissionEngineering.Core;

public static class LaTexUtilities
{
    public static void ConvertTexToPdf(string texFile, int padding = 0)
    {
        var path = Path.GetDirectoryName(texFile);
        var fileNameTex = Path.GetFileName(texFile);

        var pdfFile = texFile.Replace(".tex", ".pdf");

        LogUtilities.LogInformation($"Writing    Pdf  file : {pdfFile}", padding);

        Process process = new Process();

        process.StartInfo.FileName = "pdflatex.exe";
        process.StartInfo.WorkingDirectory = path;
        process.StartInfo.Arguments = fileNameTex;

        process.Start();

        process.WaitForExit();

        process.Start();

        process.WaitForExit();
    }
}