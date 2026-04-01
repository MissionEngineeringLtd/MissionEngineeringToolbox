using System.IO.Compression;

namespace MissionEngineering.Core;

public static class ZipUtilities
{
    public static void ZipDirectory(string folderPath, string zipFileName)
    {
        var fileFull = Path.Combine(folderPath, zipFileName);

        File.Delete(fileFull);

        var tempPath = Path.GetTempPath();

        var tempFile = Path.GetFileName(zipFileName);

        var tempFileFull = Path.Combine(tempPath, tempFile);

        File.Delete(tempFileFull);

        ZipFile.CreateFromDirectory(folderPath, tempFileFull);

        File.Copy(tempFileFull, zipFileName, true);

        File.Delete(tempFileFull);
    }
}