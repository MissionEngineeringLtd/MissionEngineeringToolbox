using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Sensor;

public abstract class Sensor : ISensor
{
    public abstract List<DetectionReport> GenerateDetectionReports(double time_s);
}