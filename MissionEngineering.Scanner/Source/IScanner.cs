using MissionEngineering.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Scanner;

public interface IScanner : IExecutableModel
{
    ScanSettings ScanSettings { get; set; }
    
    ScanData ScanData { get; set; }
}
