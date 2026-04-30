using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.Common
{
    public class Fact
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        public Fact(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
