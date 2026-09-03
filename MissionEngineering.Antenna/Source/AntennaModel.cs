using MissionEngineering.Core;
using MissionEngineering.Math;
using System.Runtime.CompilerServices;

namespace MissionEngineering.Antenna;

public class AntennaModel
{
    public AntennaModelSettings AntennaModelSettings { get; set; }

    public Vector AzimuthAngles_deg {  get; set; }

    public int NumberOfAzimuthAngles => AzimuthAngles_deg.NumberOfElements;

    public int NFFT { get; set; }

    public Vector AntennaWeights { get; set; }

    public Vector AntennaWeightsPadded { get; set; }

    public Vector ArrayFactor { get; set; }

    public Vector ElementFactor { get; set; }

    public Vector AntennaDirectivity { get; set; }

    public Vector AntennaGain { get; set; }

    public Vector ArrayFactor_dB { get; set; }

    public Vector ElementFactor_dB { get; set; }

    public Vector AntennaDirectivity_dB { get; set; }

    public Vector AntennaGain_dB { get; set; }

    public List<AntennaPatternDataPoint> AntennaPatternDataPoints { get; set; }

    public void GenerateAntenna()
    {
        GenerateAzimuthAngles();
        GenerateAntennaWeights();
        GenerateAntennaPattern();
    }

    public void GenerateAntennaWeights()
    {
        var a = AntennaModelSettings;

        AntennaWeights = Vector.Ones(a.NumberOfAntennaElements);

        NFFT = 1024;

        AntennaWeightsPadded = Vector.Ones(NFFT, a.NumberOfAntennaElements);
    }

    public void GenerateAntennaPattern()
    {
        GenerateArrayFactor();

        GenerateElementFactor();

        GenerateAntennaPatternDirectivity();

        GenerateAntennaPatternGain();

        GenerateAntennaPatternData_dB();

        GenerateAntennaPatternDataPoints();

        WriteAntennaPattern();
    }

    public void GenerateAzimuthAngles()
    {
        var s = AntennaModelSettings;

        AzimuthAngles_deg = Vector.LinearlySpacedVector(s.AzimuthAngleMin_deg, s.AzimuthAngleMax_deg, s.AzimuthAngleStep_deg);
    }

    public void GenerateArrayFactor()
    {
        ArrayFactor = AntennaWeightsPadded.FFT();
    }

    public void GenerateElementFactor()
    {
        ElementFactor = Vector.Ones(NFFT);
    }

    public void GenerateAntennaPatternDirectivity()
    {
        AntennaDirectivity = ArrayFactor * ElementFactor;
    }

    public void GenerateAntennaPatternGain()
    {
        AntennaGain = AntennaDirectivity * (1.0 / AntennaModelSettings.AntennaLosses);
    }

    public void GenerateAntennaPatternData_dB()
    {
        ArrayFactor_dB = ArrayFactor.PowerToDecibels();
        ElementFactor_dB = ElementFactor.PowerToDecibels();
        AntennaDirectivity_dB = AntennaDirectivity.PowerToDecibels();
        AntennaGain_dB = AntennaGain.PowerToDecibels();
    }

    public void GenerateAntennaPatternDataPoints()
    {
        AntennaPatternDataPoints = [];

        for (int i = 0; i < NFFT/2; i++)
        {
            var antennaPatternDataPoint = new AntennaPatternDataPoint
            {
                //AzimuthAngle_deg = AzimuthAngles_deg[i],
                ArrayFactor = ArrayFactor[i],
                ElementFactor = ElementFactor[i],
                AntennaDirectivity = AntennaDirectivity[i],
                AntennaGain = AntennaGain[i],
                ArrayFactor_dB = ArrayFactor_dB[i],
                ElementFactor_dB = ElementFactor_dB[i],
                AntennaDirectivity_dB = AntennaDirectivity_dB[i],
                AntennaGain_dB = AntennaGain_dB[i]
            };

            AntennaPatternDataPoints.Add(antennaPatternDataPoint);
        }
    }

    public void WriteAntennaPattern()
    {
        var folderPath = @"C:\Temp\MissionEngineeringToolbox\AntennaModel";
        var filePath = AntennaModelSettings.AntennaName + ".csv";

        var filePathFull = Path.Combine(folderPath, filePath);

        WriteAntennaPatternDataPointsToCsv(filePathFull);
    }

    public void WriteAntennaPatternDataPointsToCsv(string filePath)
    {
        AntennaPatternDataPoints.WriteToCsvFile(filePath);
    }
}