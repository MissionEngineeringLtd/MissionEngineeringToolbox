using Microsoft.Extensions.DependencyInjection;
using MissionEngineering.Core;
using MissionEngineering.DataRecorder;
using MissionEngineering.Math;
using MissionEngineering.Simdis;

namespace MissionEngineering.Simulation;

public static class SimulationBuilder
{
    public static ISimulationHarness CreateSimulationHarness()
    {
        var services = new ServiceCollection();

        services.AddScoped<ISimulationHarness, SimulationHarness>();
        services.AddScoped<SimulationHarnessSettings, SimulationHarnessSettings>();
        services.AddScoped<ISimulation, Simulation>();
        services.AddScoped<IDateTimeOrigin, DateTimeOrigin>();
        services.AddScoped<ILLAOrigin, LLAOrigin>();
        services.AddScoped<ScenarioSettings, ScenarioSettings>();
        services.AddScoped<IDataRecorder, DataRecorder.DataRecorder>();
        services.AddScoped<SimulationSettings, SimulationSettings>();
        services.AddScoped<SimulationData, SimulationData>();
        services.AddScoped<ISimdisExporter, SimdisExporter>();

        using var serviceProvider = services.BuildServiceProvider();

        var simulationHarness = serviceProvider.GetRequiredService<ISimulationHarness>();

        return simulationHarness;
    }

    public static ISimulation CreateSimulation()
    {
        var services = new ServiceCollection();

        services.AddScoped<ISimulation, Simulation>();
        services.AddScoped<IDateTimeOrigin, DateTimeOrigin>();
        services.AddScoped<ILLAOrigin, LLAOrigin>();
        services.AddScoped<ScenarioSettings, ScenarioSettings>();
        services.AddScoped<IDataRecorder, DataRecorder.DataRecorder>();
        services.AddScoped<SimulationSettings, SimulationSettings>();
        services.AddScoped<SimulationData, SimulationData>();
        services.AddScoped<ISimdisExporter, SimdisExporter>();

        using var serviceProvider = services.BuildServiceProvider();

        var simulation = serviceProvider.GetRequiredService<ISimulation>();

        return simulation;
    }
}