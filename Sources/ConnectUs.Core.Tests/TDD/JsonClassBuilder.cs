using System.Text.RegularExpressions;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonClassBuilder
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"(?<value>[^,^}]*)";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex PropertyRegex = new Regex(JsonRegex.Replace("'", "\""));
        private static readonly Regex ClassRegex = new Regex(@"(?<name>'[^,]*?'):(?<value>[{\[].*?[}\]])".Replace("'", "\""));

        public JsonClass Build(string json)
        {
            var jsonClass = new JsonClass();
            json = ParseSubObjects(jsonClass, json);
            ParseProperties(jsonClass, json);
            return jsonClass;
        }

        private void ParseProperties(JsonClass jsonClass, string json)
        {
            var matches = PropertyRegex.Matches(json);
            foreach (Match match in matches)
            {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value.Trim();
                jsonClass.Add(name, JsonObjectFactory.BuildJsonObject(value));
            }
        }
        private string ParseSubObjects(JsonClass jsonClass, string json)
        {
            var classMatches = ClassRegex.Matches(json);
            foreach (Match classMatch in classMatches)
            {
                var name = classMatch.Groups["name"].Value;
                var value = classMatch.Groups["value"].Value;
                jsonClass[name] = Build(value);
                json = json.Replace(classMatch.Value, "");
            }
            return json;
        }
    }
}