using Microsoft.UI.Reactor;
using Microsoft.UI.Reactor.Core;         // BackdropKind
using MissionEngineering.Radar;
using static Microsoft.UI.Reactor.Factories;

public class Shell : Component
{
    public override Element Render()
    {
        var titleBar = TitleBar("Radar Calculator");

        return ScrollView(
            HStack(24,
                Heading("Radar Calculator"),
                Component<SystemComponent>(),
                Component<WaveformComponent>().IsEnabled(false)
            ).Padding(24)
        );
    }
}