namespace MissionEngineering.Math.Tests;

[TestClass]
public sealed class SetpointControllerTests
{
    [TestMethod]
    public void Update_WithValidValues_ExpectSuccess()
    {
        // Arrange
        var controller = new SetpointController
        {
            SetpointValue = 10.0,
            ActualValue = 5.0,
            MinimumValue = -20.0,
            MaximumValue = 20.0,
            ControllerGain = 2.0
        };

        // Act
        controller.Update();

        // Assert
        var expectedControlOutput = 10.0;

        Assert.AreEqual(expectedControlOutput, controller.ControlOutput, 1.0e-6);
    }
}