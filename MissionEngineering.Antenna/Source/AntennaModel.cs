using MissionEngineering.Core;
using MissionEngineering.Math;
using static System.Math;
using Complex = System.Numerics.Complex;

namespace MissionEngineering.Antenna;

public class AntennaModel
{
    public AntennaModelSettings AntennaModelSettings { get; set; }

    public Vector AzimuthAngles_deg {  get; set; }

    public int NumberOfAntennaElements => AntennaModelSettings.NumberOfAntennaElements;

    public int NumberOfAzimuthAngles => AzimuthAngles_deg.NumberOfElements;

    public VectorComplex AntennaWeights { get; set; }

    public VectorComplex ArrayFactor { get; set; }

    public VectorComplex ElementFactor { get; set; }

    public VectorComplex AntennaFactor { get; set; }

    public Vector AntennaDirectivity { get; set; }

    public Vector AntennaGain { get; set; }

    public Vector ArrayFactor_dB { get; set; }

    public Vector ElementFactor_dB { get; set; }

    public Vector AntennaDirectivity_dB { get; set; }

    public Vector AntennaGain_dB { get; set; }

    public Vector AntennaDirectivityNormalised_dB { get; set; }

    public Vector AntennaGainNormalised_dB { get; set; }

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

        AntennaWeights = VectorComplex.Ones(a.NumberOfAntennaElements);
    }

    public void GenerateAntennaPattern()
    {
        GenerateArrayFactor();

        GenerateElementFactor();

        GenerateAntennaFactor();

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
        ArrayFactor = new VectorComplex(NumberOfAzimuthAngles);

        for (int i = 0; i < NumberOfAzimuthAngles; i++)
        {
            var azimuthAngle_deg = AzimuthAngles_deg[i];

            var phaseShiftPerElement = ComputePhaseShiftPerElement(azimuthAngle_deg, AntennaModelSettings.AntennaElementSpacing_m, AntennaModelSettings.RfWavelength_m);

            var phaseShifts = ComputePhaseShifts(phaseShiftPerElement);

            var arrayFactor = ComputeArrayFactor(phaseShifts);

            ArrayFactor[i] = arrayFactor;
        };
    }

    public double ComputePhaseShiftPerElement(double azimuthAngle_deg, double antennaElementSpacing_m, double rfWavelength_m)
    {
        var azimuthAngle_rad = azimuthAngle_deg.DegreesToRadians();

        var phaseShiftPerElement = (2.0 * PI * antennaElementSpacing_m / rfWavelength_m) * Sin(azimuthAngle_rad);

        return phaseShiftPerElement;
    }

    public VectorComplex ComputePhaseShifts(double phaseShiftPerElement)
    {
        var start = 0.0;
        var step = phaseShiftPerElement;

        var phaseShifts = VectorComplex.LinearlySpacedVector(start, step, NumberOfAntennaElements);
    
        return phaseShifts;
    }

    public Complex ComputeArrayFactor(VectorComplex phaseShifts)
    {
        var arrayFactor = new VectorComplex(NumberOfAntennaElements);

        for (int i = 0; i < NumberOfAntennaElements; i++)
        {
            arrayFactor[i] = AntennaWeights[i] * Complex.Exp(-1.0 * Complex.ImaginaryOne * phaseShifts[i]);
        }

        var arrayFactorSum = arrayFactor.Sum();

        return arrayFactorSum;
    }

    public void GenerateElementFactor()
    {
        ElementFactor = VectorComplex.Ones(NumberOfAzimuthAngles);
    }

    public void GenerateAntennaFactor()
    {
        AntennaFactor = ArrayFactor * ElementFactor;
    }

    public void GenerateAntennaPatternDirectivity()
    {
        AntennaDirectivity = AntennaFactor.Magnitude();

        AntennaDirectivity = AntennaDirectivity * AntennaDirectivity;

        AntennaDirectivity = AntennaDirectivity / NumberOfAntennaElements;
    }

    public void GenerateAntennaPatternGain()
    {
        AntennaGain = AntennaDirectivity * (1.0 / AntennaModelSettings.AntennaLosses);
    }

    public void GenerateAntennaPatternData_dB()
    {
        ArrayFactor_dB = ArrayFactor.Magnitude().PowerToDecibels();
        ElementFactor_dB = ElementFactor.Magnitude().PowerToDecibels();
        AntennaDirectivity_dB = AntennaDirectivity.PowerToDecibels();
        AntennaGain_dB = AntennaGain.PowerToDecibels();

        AntennaDirectivityNormalised_dB = AntennaDirectivity_dB - AntennaDirectivity_dB.Max();
        AntennaGainNormalised_dB = AntennaGain_dB - AntennaGain_dB.Max();
    }

    public void GenerateAntennaPatternDataPoints()
    {
        AntennaPatternDataPoints = [];

        for (int i = 0; i < NumberOfAzimuthAngles; i++)
        {
            var antennaPatternDataPoint = new AntennaPatternDataPoint
            {
                AzimuthAngle_deg = AzimuthAngles_deg[i],
                ArrayFactor = ArrayFactor[i].Magnitude,
                ElementFactor = ElementFactor[i].Magnitude,
                AntennaDirectivity = AntennaDirectivity[i],
                AntennaGain = AntennaGain[i],
                ArrayFactor_dB = ArrayFactor_dB[i],
                ElementFactor_dB = ElementFactor_dB[i],
                AntennaDirectivity_dB = AntennaDirectivity_dB[i],
                AntennaGain_dB = AntennaGain_dB[i],
                AntennaDirectivityNormalised_dB = AntennaDirectivityNormalised_dB[i],
                AntennaGainNormalised_dB = AntennaGainNormalised_dB[i]
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