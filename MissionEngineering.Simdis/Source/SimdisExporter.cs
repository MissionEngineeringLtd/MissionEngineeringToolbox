using MissionEngineering.Core;
using MissionEngineering.Platform;
using MissionEngineering.Simulation;
using System.Text;

namespace MissionEngineering.Simdis;

public class SimdisExporter : ISimdisExporter
{
    public SimulationData SimulationData { get; set; }

    private StringBuilder SimdisData { get; set; }

    public SimdisExporter(SimulationData simulationData)
    {
        SimulationData = simulationData;

        SimdisData = new StringBuilder();
    }

    public void GenerateSimdisData()
    {
        CreateSimdisHeader();

        CreatePlatforms();
    }

    public void WriteSimdisData()
    {
        if (!SimulationData.SimulationSettings.IsWriteData)
        {
            return;
        }

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}.asi";

        var fileNameFull = SimulationData.SimulationSettings.GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing File : {fileNameFull}");

        var strings = SimdisData.ToString();

        File.WriteAllText(fileNameFull, strings);
    }

    public void CreateSimdisHeader()
    {
        var llaOrigin = SimulationData.ScenarioSettings.LLAOrigin;

        AddLine("Version          24");
        AddLine("""Classification   "Unclassified" 0x8000FF00""");
        AddLine(@$"ScenarioInfo     ""{SimulationData.SimulationSettings.SimulationName}"" ");
        AddLine("""VerticalDatum    "WGS84" """);
        AddLine("""CoordSystem      "LLA" """);
        AddLine($"RefLLA           {llaOrigin.Latitude_deg} {llaOrigin.Longitude_deg} {llaOrigin.Altitude_m}");
        AddLine("""ReferenceTimeECI "0.0" """);
        AddLine("DegreeAngles     1");
        AddLine("");
    }

    public void CreatePlatforms()
    {
        var index = 0;

        foreach (var platformSettings in SimulationData.ScenarioSettings.PlatformSettingsList)
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

    public void AddLine(string line)
    {
        SimdisData.AppendLine(line);
    }
}