using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Sensor;

public interface ISensor
{
    List<DetectionReport> GenerateDetectionReports(double time_s);
}
