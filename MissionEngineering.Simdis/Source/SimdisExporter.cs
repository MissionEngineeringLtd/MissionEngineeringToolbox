using MathNet.Numerics.LinearAlgebra.Factorization;
using MissionEngineering.Core;
using MissionEngineering.Math;
using MissionEngineering.Platform;
using MissionEngineering.Simulation;
using System.Text;

namespace MissionEngineering.Simdis;

public class SimdisExporter : ISimdisExporter
{
    public ILogClass Log { get; set; }

    public SimulationData SimulationData { get; set; }

    private StringBuilder SimdisData { get; set; }

    private bool IsCreateZonesGOGFile { get; set; }

    private string ZonesGOGFile { get; set; }

    private string ZonesGOGFileFull { get; set; }

    public SimdisExporter(SimulationData simulationData, ILogClass log)
    {
        SimulationData = simulationData;

        SimdisData = new StringBuilder();

        Log = log;
    }

    public void GenerateSimdisData()
    {
        CreateZonesGOGFile();

        CreateSimdisHeader();

        CreateSimdisGOGFileReference();

        CreatePlatforms();
    }

    public void WriteSimdisData()
    {
        if (!SimulationData.SimulationRunSettings.IsWriteData)
        {
            return;
        }

        var fileName = $"{SimulationData.SimulationRunSettings.SimulationName}.asi";

        var fileNameFull = SimulationData.SimulationRunSettings.GetFileNameFull(fileName);

        Log.LogInformation($"Writing File : {fileNameFull}");

        var strings = SimdisData.ToString();

        File.WriteAllText(fileNameFull, strings);
    }

    public void CreateSimdisHeader()
    {
        var llaOrigin = new PositionLLA(SimulationData.SimulationSettings.Latitude_deg, SimulationData.SimulationSettings.Longitude_deg, 0.0);

        AddLine("Version          24");
        AddLine("""Classification   "Unclassified" 0x8000FF00""");
        AddLine(@$"ScenarioInfo     ""{SimulationData.SimulationSettings.SimulationName}"" ");
        AddLine("""VerticalDatum    "WGS84" """);
        AddLine("""CoordSystem      "LLA" """);
        AddLine($"RefLLA           {llaOrigin.Latitude_deg} {llaOrigin.Longitude_deg} {llaOrigin.Altitude_m}");
        AddLine($"ReferenceYear    {SimulationData.SimulationSettings.DateTimeOrigin.Substring(0, 4)}");
        //AddLine("""ReferenceTimeECI "0.0" """);
        AddLine("DegreeAngles     1");
        AddLine("");
    }

    public void CreateSimdisGOGFileReference()
    {
        if (!IsCreateZonesGOGFile)
        {
            return;
        }

        AddLine(@$"GOGFile ""{ZonesGOGFile}""");
        AddLine("");
    }

    public void CreatePlatforms()
    {
        var index = 0;

        foreach (var platformSettings in SimulationData.PlatformSettingsList)
        {
            var platformId = platformSettings.PlatformHeader.PlatformId;

            var platformIdSimdis = GetSimdisPlatformId(platformId);

            var platformDataList = SimulationData.PlatformDataPerPlatform[index];

            CreatePlatformInitialisation(platformIdSimdis, platformSettings);

            CreatePlatformData(platformIdSimdis, platformDataList);

            index++;
        }
    }

    public int GetSimdisPlatformId(int platformId)
    {
        return platformId;
    }

    public void CreatePlatformInitialisation(int platformId, PlatformSettings platformSettings)
    {
        var ph = platformSettings.PlatformHeader;
        var ps = platformSettings.PlatformHeaderSimdis;

        AddLine(@$"PlatformID          {platformId}");
        AddLine(@$"PlatformName        {platformId} ""{ph.PlatformName}""");
        AddLine(@$"PlatformType        {platformId} ""{ps.PlatformType}""");
        AddLine(@$"PlatformIcon        {platformId} ""{ps.PlatformIcon}""");
        AddLine(@$"PlatformFHN         {platformId} {ps.PlatformAffiliationFHN}");
        AddLine(@$"PlatformInterpolate {platformId} {ps.PlatformInterpolate}");
        AddLine(@$"PlatformCoordSystem {platformId} ""NED""");
        AddLine("");
        AddLine(@$"GenericData         {platformId} ""SIMDIS_DynamicScale"" ""1"" ""0"" ");
        AddLine(@$"GenericData         {platformId} ""SIMDIS_ScaleLevel"" ""{ps.PlatformScaleLevel}"" ""0"" ");
        AddLine("");
    }

    public void CreatePlatformData(int platformId, List<PlatformData> platformDataList)
    {
        foreach (var pd in platformDataList)
        {
            var ps = pd.PlatformState;

            var time = ps.TimeStamp.SimulationTime_s;
            var pos = ps.PositionNED;
            var vel = ps.VelocityNED;
            var att = ps.Attitude;

            string line = $"PlatformData {platformId} {time} {pos.PositionNorth_m} {pos.PositionEast_m} {pos.PositionDown_m} {att.HeadingAngle_deg} {att.PitchAngle_deg} {att.BankAngle_deg} {vel.VelocityNorth_ms} {vel.VelocityEast_ms} {vel.VelocityDown_ms}";

            AddLine(line);
        }

        AddLine("");
    }

    public void CreateZonesGOGFile()
    {
        IsCreateZonesGOGFile = SimulationData.SimulationZones.Count > 0;

        if (!IsCreateZonesGOGFile)
        {
            return;
        }

        var fileName = $"{SimulationData.SimulationRunSettings.SimulationName}.gog";

        var fileNameFull = SimulationData.SimulationRunSettings.GetFileNameFull(fileName);

        ZonesGOGFile = fileName;
        ZonesGOGFileFull = fileNameFull;

        Log.LogInformation($"Writing File : {fileNameFull}");
        
        var text = @"
start
annotation  : OCA Area of Interest: FL 660
LL 59.141667 9.236111 66000.000000
starttime ""2025-01-01T00:00:00Z""
endtime   ""2025-01-01T00:10:00Z""
end

start
line
linewidth 5
linecolor YELLOW
LL 60.977778 2.161111 66000.000000
LL 69.797222 15.013889 66000.000000
LL 65.605556 25.541667 66000.000000
LL 59.141667 9.236111 66000.000000
LL 60.977778 2.161111 66000.000000
starttime ""2025-01-01T00:00:00Z""
endtime   ""2025-01-01T00:10:00Z""
end";

    File.WriteAllText(fileNameFull, text.ToString());

    }

    public void AddLine(string line)
    {
        SimdisData.AppendLine(line);
    }
}