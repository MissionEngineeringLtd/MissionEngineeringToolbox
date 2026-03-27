using System;
using System.Collections.Generic;
using System.Text;

namespace MissionEngineering.Platform;

public class PlatformSettings
{
    public required PlatformHeader PlatformHeader { get; set; }

    public required PlatformState PlatformState { get; set; }
}
