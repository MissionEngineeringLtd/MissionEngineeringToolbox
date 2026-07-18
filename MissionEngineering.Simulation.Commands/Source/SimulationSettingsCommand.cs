using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Simulation;

public class SimulationSettingsCommand : ISimulationCommand
{
    public SimulationCommandType CommandType { get; set; }

    public double CommandTime { get; set; }

    public DateTimeOffset SimulationStartDateTime { get; set; }

    public double SimulationStartTime_s { get; set; }

    public double SimulationEndTime_s { get; set; }

    public double SimulationTimeStep_s { get; set; }

    public SimulationSettingsCommand()
    {
        CommandType = SimulationCommandType.SimulationSettings;
    }
}