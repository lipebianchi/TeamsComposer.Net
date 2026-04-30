using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.MessageCards
{
    public class PotentialAction
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("targets")]
        public List<ActionTarget>? Targets { get; set; }

        [JsonPropertyName("target")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; set; }

        [JsonPropertyName("body")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Body { get; set; }

        public static PotentialAction CreateOpenUri(string name, string uri)
        {
            return new PotentialAction
            {
                Type = "OpenUri",
                Name = name,
                Targets = new List<ActionTarget>
                {
                    new ActionTarget { Uri = uri }
                }
            };
        }

        public static PotentialAction CreateHttpPost(string name, string target, string body)
        {
            return new PotentialAction
            {
                Type = "HttpPOST",
                Name = name,
                Target = target,
                Body = body
            };
        }
    }

    public class ActionTarget
    {
        [JsonPropertyName("os")]
        public string Os { get; } = "default";

        [JsonPropertyName("uri")]
        public required string Uri { get; set; }
    }
}
