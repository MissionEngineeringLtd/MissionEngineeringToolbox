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
        var simulationHarness = new SimulationHarness();

        simulationHarness.SimulationHarnessSettings = new SimulationHarnessSettings();

        return simulationHarness;
    }

    public static ISimulation CreateSimulation()
    {
        var serviceProvider = CreateServices();

        var simulation = serviceProvider.GetRequiredService<ISimulation>();

        return simulation;
    }

    public static ServiceProvider CreateServices()
    {
        var services = new ServiceCollection();

        services.AddScoped<ISimulation, Simulation>();
        services.AddScoped<ISimulationClock, SimulationClock>();
        services.AddScoped<IDateTimeOrigin, DateTimeOrigin>();
        services.AddScoped<ILLAOrigin, LLAOrigin>();
        services.AddScoped<ScenarioSettings, ScenarioSettings>();
        services.AddScoped<IDataRecorder, DataRecorder.DataRecorder>();
        services.AddScoped<SimulationSettings, SimulationSettings>();
        services.AddScoped<SimulationData, SimulationData>();
        services.AddScoped<ISimdisExporter, SimdisExporter>();
        services.AddScoped<ILogClass, LogClass>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}