using Newtonsoft.Json.Linq;

namespace API.IntegrationTests.Helpers
{
    public static class EnvironmentVariables
    {
        public static readonly dynamic Variables = ReadEnvironmentVariablesFromFile();

        private static dynamic ReadEnvironmentVariablesFromFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "SEBDetectLanguageProperties.json");
            var json = JObject.Parse(File.ReadAllText(path));

            return json.ToObject<dynamic>()!;
        }
    }
}