namespace MissionEngineering.Core;

public static class StringExtensions
{
    extension(string s)
    {
        public string ConvertToDisplayString()
        {
            var result = s.Replace("_", "-");

            return result;
        }
    }
}