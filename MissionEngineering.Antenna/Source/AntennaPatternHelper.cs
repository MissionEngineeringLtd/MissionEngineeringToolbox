using MissionEngineering.Math;
using System.Text;

namespace MissionEngineering.Antenna;

public static class AntennaPatternHelper
{
    public static AntennaPattern LoadAntennaPatternAprf(string filePath)
    {
        var antennaPattern = new AntennaPattern();

        return antennaPattern;
    }

    public static void SaveAntennaPatternAprf(string filePath, AntennaPattern antennaPattern)
    {
        var lines = new StringBuilder();

        var nAz = antennaPattern.NumberOfAzimuthAngles;
        var nEl = antennaPattern.NumberOfAzimuthAngles;

        lines.AppendLine(@$"{nAz} {nEl} // #azim pts,  #elev pts");
        lines.AppendLine();
        lines.AppendLine(@$"// azimuth data: ang (deg), gain (relative dB off main beam)");
        lines.AppendLine();

        for (int i = 0; i < nAz; i++)
        {
            var azDeg = antennaPattern.AzimuthAngles_deg[i].ToDisplayFormat(2);
            var gaindB = antennaPattern.AntennaGainNormalised_dB[i].ToDisplayFormat(2);

            lines.AppendLine(@$"{azDeg} {gaindB}");
        }

        lines.AppendLine();
        lines.AppendLine(@$"// elevation data: ang (deg), gain (relative dB off main beam)");
        lines.AppendLine();

        for (int i = 0; i < nAz; i++)
        {
            var elDeg = antennaPattern.AzimuthAngles_deg[i].ToDisplayFormat(2);
            var gaindB = antennaPattern.AntennaGainNormalised_dB[i].ToDisplayFormat(2);
            
            lines.AppendLine(@$"{elDeg} {gaindB}");
        }

        File.WriteAllText(filePath, lines.ToString());
    }

    public static void SaveAntennaPatternApbf(string filePath, AntennaPattern antennaPattern, AntennaModelSettings antennaModelSettings)
    {
        var am = antennaModelSettings;
        var ap = antennaPattern;

        var lines = new StringBuilder();

        var nAz = ap.NumberOfAzimuthAngles;
        var nEl = ap.NumberOfAzimuthAngles;

        lines.AppendLine("bilinear");
        lines.AppendLine($@"{am.RfFrequency_MHz.ToDisplayFormat(3)} {am.RfFrequency_MHz.ToDisplayFormat(3)} 1");
        lines.AppendLine($@"{am.AzimuthAngleMin_deg.ToDisplayFormat(2)} {am.AzimuthAngleMax_deg.ToDisplayFormat(2)},  {am.AzimuthAngleStep_deg.ToDisplayFormat(2)}");
        lines.AppendLine($@"{am.AzimuthAngleMin_deg.ToDisplayFormat(2)} {am.AzimuthAngleMax_deg.ToDisplayFormat(2)},  {am.AzimuthAngleStep_deg.ToDisplayFormat(2)}");

        for (int i = 0; i < nAz; i++)
        {
            for (int j = 0; j < nEl; j++)
            {
                var gainAz_dB = ap.AntennaGain_dB[i];
                var gainEl_dB = ap.AntennaGainNormalised_dB[j];

                var gain_dB = gainAz_dB + gainEl_dB;

                var gain_dB_string = gain_dB.ToDisplayFormat(2);

                lines.AppendLine(gain_dB_string);
            }
        }

        File.WriteAllText(filePath, lines.ToString());
    }
}