using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Backend.IntegrationTests.Properties
{
    public static class EnvironmentVariables
    {
        public static readonly dynamic Variables = ReadEnvironmentVariablesFromFile();

        private static dynamic ReadEnvironmentVariablesFromFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\SEBDetectLanguageProperties.json");
            var json = JObject.Parse(File.ReadAllText(path));

            return json.ToObject<dynamic>()!;
        }
    }
}