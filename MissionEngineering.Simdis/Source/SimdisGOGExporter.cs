using MissionEngineering.Core;
using MissionEngineering.Math;
using System.Text;

namespace MissionEngineering.Simulation;

public class SimdisGOGExporter
{
    public ILogClass Log { get; set; }

    public SimulationData SimulationData { get; set; }

    public StringBuilder SimdisGOGData { get; set; }

    public List<SimulationZone> SimulationZones { get; set; }

    public string SimdisGOGFile { get; set; }

    public SimdisGOGExporter(SimulationData simulationData, ILogClass log)
    {
        SimulationData = simulationData;

        SimdisGOGData = new StringBuilder();

        Log = log;
    }

    public void ExportSimdisGOGData()
    {
        GenerateSimdisGOGData();
        WriteSimdisGOGData();
    }

    public void GenerateSimdisGOGData()
    {
        foreach (var simulationZone in SimulationData.SimulationZones)
        {
            CreateSimdisGOGZoneDataSingle(simulationZone);
        };
    }

    public void CreateSimdisGOGZoneDataSingle(SimulationZone simulationZone)
    {
        var numberOfPoints = simulationZone.ZonePointsLatitude_DMS.Length;

        var lat_deg = simulationZone.ZonePointsLatitude_DMS.Select(s => s.DMSToDegrees()).ToArray();
        var lon_deg = simulationZone.ZonePointsLongitude_DMS.Select(s => s.DMSToDegrees()).ToArray();

        var alt_ft = simulationZone.ZoneHeightMax_ft;
        var alt_FL = alt_ft.FeetToFlightLevel();

        var llStringFirst = GetPointString(lat_deg[0], lon_deg[0], alt_ft);
        var llStringLast = GetPointString(lat_deg[^1], lon_deg[^1], alt_ft);

        var simulationZoneName = $@"{simulationZone.ZoneName}: FL{alt_FL}";

        var startTimeString = "2025-01-01T00:00:00Z";
        var endTimeString = "2025-01-01T00:10:00Z";

        AddLine(@$"start");
        AddLine(@$"annotation  : {simulationZoneName}");
        AddLine(@$"{llStringLast}");
        AddLine(@$"starttime ""{ startTimeString}""");
        AddLine(@$"endtime   ""{endTimeString}""");
        AddLine(@$"end");
        AddLine("");
        AddLine(@$"start");
        AddLine($@"line");
        AddLine(@$"linewidth 5");
        AddLine(@$"linecolor {simulationZone.ZoneColor}");

        for (int i = 0; i < numberOfPoints; i++)
        {
            var lat = lat_deg[i];
            var lon = lon_deg[i];

            var llString = GetPointString(lat, lon, alt_ft);

            AddLine(@$"{llString}");
        }

        // Repeat the first point to close the polygon:
        AddLine(@$"{llStringFirst}");

        AddLine(@$"starttime ""{startTimeString}""");
        AddLine(@$"endtime   ""{endTimeString}""");
        AddLine(@$"end");
        AddLine("");
    }

    public string GetPointString(double lat_deg, double lon_deg, double alt_ft)
    {
        return @$"LL {lat_deg:F6} {lon_deg:F6} {alt_ft:F2}";
    }

    public void WriteSimdisGOGData()
    {
        if (!SimulationData.SimulationRunSettings.IsWriteData)
        {
            return;
        }

        var fileName = $"{SimulationData.SimulationRunSettings.SimulationName}.gog";

        var fileNameFull = SimulationData.SimulationRunSettings.GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        var strings = SimdisGOGData.ToString();

        File.WriteAllText(fileNameFull, strings);

        SimdisGOGFile = fileName;
    }

    public void AddLine(string line)
    {
        SimdisGOGData.AppendLine(line);
    }
}
