using MissionEngineering.Core;
using MissionEngineering.Math;
using static System.Math;
using Complex = System.Numerics.Complex;

namespace MissionEngineering.Antenna;

public class AntennaModel
{
    public string OutputFolder { get; set; }

    public AntennaModelSettings AntennaModelSettings { get; set; }

    public AntennaArrayPattern AntennaArrayPattern { get; set; }

    public AntennaPattern AntennaPattern { get; set; }

    public List<AntennaPatternDataPoint> AntennaPatternDataPoints { get; set; }

    public AntennaModel()
    {
        AntennaArrayPattern = new AntennaArrayPattern   ();
        AntennaPattern = new AntennaPattern();
    }

    public void GenerateAntenna()
    {
        GenerateAzimuthAngles();

        GenerateArrayPattern();

        GenerateAntennaPattern();
    }

    public void GenerateArrayPattern()
    {
        var am = AntennaModelSettings;
        var aa = AntennaArrayPattern;

        aa.NumberOfAntennaElements = am.NumberOfAntennaElements;

        GenerateAntennaWeights();

        GenerateArrayFactor();

        GenerateElementFactor();

        GenerateAntennaFactor();

        GenerateArrayPatternData_dB();
    }

    public void GenerateAntennaPattern()
    {
        var am = AntennaModelSettings;
        var aa = AntennaArrayPattern;
        var ap = AntennaPattern;

        ap.AntennaPatternName = am.AntennaName;

        ap.AzimuthAngles_deg = aa.AzimuthAngles_deg;

        GenerateAntennaPatternDirectivity();

        GenerateAntennaPatternGain();

        GenerateAntennaPatternData_dB();

        GenerateAntennaPatternDataPoints();
    }

    public void GenerateAzimuthAngles()
    {
        var am = AntennaModelSettings;
        var aa = AntennaArrayPattern;

        aa.AzimuthAngles_deg = Vector.LinearlySpacedVector(am.AzimuthAngleMin_deg, am.AzimuthAngleMax_deg, am.AzimuthAngleStep_deg);
    }

    public void GenerateAntennaWeights()
    {
        var am = AntennaModelSettings;
        var aa = AntennaArrayPattern;

        aa.AntennaWeights = VectorComplex.Ones(am.NumberOfAntennaElements);
    }

    public void GenerateArrayFactor()
    {
        var am = AntennaModelSettings;
        var aa = AntennaArrayPattern;

        aa.ArrayFactor = new VectorComplex(aa.NumberOfAzimuthAngles);

        for (int i = 0; i < aa.NumberOfAzimuthAngles; i++)
        {
            var azimuthAngle_deg = aa.AzimuthAngles_deg[i];

            var phaseShiftPerElement = ComputePhaseShiftPerElement(azimuthAngle_deg, am.AntennaElementSpacing_m, am.RfWavelength_m);

            var phaseShifts = ComputePhaseShifts(phaseShiftPerElement);

            var arrayFactor = ComputeArrayFactor(phaseShifts);

            aa.ArrayFactor[i] = arrayFactor;
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
        var aa = AntennaArrayPattern;

        var start = 0.0;
        var step = phaseShiftPerElement;

        var phaseShifts = VectorComplex.LinearlySpacedVector(start, step, aa.NumberOfAntennaElements);
    
        return phaseShifts;
    }

    public Complex ComputeArrayFactor(VectorComplex phaseShifts)
    {
        var aa = AntennaArrayPattern;

        var arrayFactor = new VectorComplex(aa.NumberOfAntennaElements);

        for (int i = 0; i < aa.NumberOfAntennaElements; i++)
        {
            arrayFactor[i] = aa.AntennaWeights[i] * Complex.Exp(-1.0 * Complex.ImaginaryOne * phaseShifts[i]);
        }

        var arrayFactorSum = arrayFactor.Sum();

        return arrayFactorSum;
    }

    public void GenerateElementFactor()
    {
        var aa = AntennaArrayPattern;

        aa.ElementFactor = VectorComplex.Ones(aa.NumberOfAzimuthAngles);
    }

    public void GenerateAntennaFactor()
    {
        var aa = AntennaArrayPattern;

        aa.AntennaFactor = aa.ArrayFactor * aa.ElementFactor;
    }

    public void GenerateArrayPatternData_dB()
    {
        var aa = AntennaArrayPattern;

        aa.ArrayFactor_dB = aa.ArrayFactor.Magnitude().PowerToDecibels();
        aa.ElementFactor_dB = aa.ElementFactor.Magnitude().PowerToDecibels();
    }

    public void GenerateAntennaPatternDirectivity()
    {
        var aa = AntennaArrayPattern;
        var ap = AntennaPattern;

        ap.AntennaDirectivity = aa.AntennaFactor.Magnitude();

        ap.AntennaDirectivity = ap.AntennaDirectivity * ap.AntennaDirectivity;

        ap.AntennaDirectivity = ap.AntennaDirectivity / aa.NumberOfAntennaElements;
    }

    public void GenerateAntennaPatternGain()
    {
        var ap = AntennaPattern;

        ap.AntennaGain = ap.AntennaDirectivity * (1.0 / AntennaModelSettings.AntennaLosses);
    }

    public void GenerateAntennaPatternData_dB()
    {
        var aa = AntennaArrayPattern;
        var ap = AntennaPattern;
    
        ap.AntennaDirectivity_dB = ap.AntennaDirectivity.PowerToDecibels();
        ap.AntennaGain_dB = ap.AntennaGain.PowerToDecibels();

        ap.AntennaDirectivityNormalised_dB = ap.AntennaDirectivity_dB - ap.AntennaDirectivity_dB.Max();
        ap.AntennaGainNormalised_dB = ap.AntennaGain_dB - ap.AntennaGain_dB.Max();
    }

    public void GenerateAntennaPatternDataPoints()
    {
        var aa = AntennaArrayPattern;
        var ap = AntennaPattern;

        AntennaPatternDataPoints = [];

        for (int i = 0; i < aa.NumberOfAzimuthAngles; i++)
        {
            var antennaPatternDataPoint = new AntennaPatternDataPoint
            {
                AzimuthAngle_deg = aa.AzimuthAngles_deg[i],
                ArrayFactor = aa.ArrayFactor[i].Magnitude,
                ElementFactor = aa.ElementFactor[i].Magnitude,
                AntennaDirectivity = ap.AntennaDirectivity[i],
                AntennaGain = ap.AntennaGain[i],
                ArrayFactor_dB = aa.ArrayFactor_dB[i],
                ElementFactor_dB = aa.ElementFactor_dB[i],
                AntennaDirectivity_dB = ap.AntennaDirectivity_dB[i],
                AntennaGain_dB = ap.AntennaGain_dB[i],
                AntennaDirectivityNormalised_dB = ap.AntennaDirectivityNormalised_dB[i],
                AntennaGainNormalised_dB = ap.AntennaGainNormalised_dB[i]
            };

            AntennaPatternDataPoints.Add(antennaPatternDataPoint);
        }
    }

    public void WriteAntennaPatternDataCsv()
    {
        if (!Directory.Exists(OutputFolder))
        {
            Directory.CreateDirectory(OutputFolder);
        }

        var filePath = AntennaModelSettings.AntennaName + ".csv";

        var filePathFull = Path.Combine(OutputFolder, filePath);

        WriteAntennaPatternDataPointsToCsv(filePathFull);
    }

    public void WriteAntennaPatternAprf()
    {
        if (!Directory.Exists(OutputFolder))
        {
            Directory.CreateDirectory(OutputFolder);
        }

        var filePath = AntennaModelSettings.AntennaName + ".aprf";

        var filePathFull = Path.Combine(OutputFolder, filePath);

        AntennaPatternHelper.SaveAntennaPatternAprf(filePathFull, AntennaPattern);
    }

    public void WriteAntennaPatternApbf()
    {
        if (!Directory.Exists(OutputFolder))
        {
            Directory.CreateDirectory(OutputFolder);
        }

        var filePath = AntennaModelSettings.AntennaName + ".apbf";

        var filePathFull = Path.Combine(OutputFolder, filePath);

        AntennaPatternHelper.SaveAntennaPatternApbf(filePathFull, AntennaPattern, AntennaModelSettings);
    }

    public void WriteAntennaPatternDataPointsToCsv(string filePath)
    {
        AntennaPatternDataPoints.WriteToCsvFile(filePath);
    }
}