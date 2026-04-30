using System.Text.Json.Serialization;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Common;

namespace TeamsComposer.Net.Models.AdaptiveCards.Roots
{
    public class AdaptiveCard
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "AdaptiveCard";

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.4";

        [JsonPropertyName("$schema")]
        public string Schema { get; } = "http://adaptivecards.io/schemas/adaptive-card.json";

        [JsonPropertyName("fallbackText")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FallbackText { get; set; }

        [JsonPropertyName("body")]
        public List<AdaptiveElement> Body { get; set; } = new();

        [JsonPropertyName("actions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AdaptiveAction>? Actions { get; set; }
    }
}
