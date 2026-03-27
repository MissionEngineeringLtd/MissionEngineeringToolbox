namespace MissionEngineering.Core;

public interface IExecutableModel
{
    void Initialise(double time_s);

    void Update(double time_s);

    void Finalise(double time_s);
}