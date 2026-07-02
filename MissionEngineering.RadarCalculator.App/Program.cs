using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Core;
using System;

namespace MissionEngineering.RadarCalculator;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        ReactorApp.Run<Shell>("Radar Calculator", width: 1024, height: 768, fullScreen: true);
    }
}