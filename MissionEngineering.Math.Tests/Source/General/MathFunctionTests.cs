namespace MissionEngineering.Math.Tests
{
    [TestClass]
    public class MathFunctionTests
    {
        [TestMethod]
        [DataRow(5.7, 1.0, 6.0)]
        [DataRow(5.2, 1.0, 5.0)]
        [DataRow(5.5, 0.5, 5.5)]
        [DataRow(5.3, 0.5, 5.5)]
        [DataRow(-2.3, 1.0, -2.0)]
        [DataRow(-2.7, 1.0, -3.0)]
        [DataRow(0.0, 0.1, 0.0)]
        public void RoundToStepSize_ReturnsExpectedResult(double value, double stepSize, double expected)
        {
            var result = MathFunctions.RoundToStepSize(value, stepSize);
            Assert.AreEqual(expected, result, 10e-10);
        }

        [TestMethod]
        [DataRow(1.0, 0.0)]
        [DataRow(1.0, -1.0)]
        public void RoundToStepSize_InvalidStepSize_Throws(double value, double stepSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => MathFunctions.RoundToStepSize(value, stepSize));
        }
    }
}