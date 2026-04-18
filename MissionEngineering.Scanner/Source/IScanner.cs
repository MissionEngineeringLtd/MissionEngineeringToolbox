using MissionEngineering.Core;

namespace MissionEngineering.Scanner;

public interface IScanner : IExecutableModel
{
    ScanSettings ScanSettings { get; set; }

    ScanData ScanData { get; set; }
}