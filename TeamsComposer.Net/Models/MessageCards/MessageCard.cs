using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace TeamsComposer.Net.Models.MessageCards
{
    public class MessageCard
    {
        [JsonPropertyName("@type")]
        public string Type { get; } = "MessageCard";

        [JsonPropertyName("@context")]
        public string Context { get; } = "https://schema.org/extensions";

        [JsonPropertyName("themeColor")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ThemeColor { get; set; }

        [JsonPropertyName("summary")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Summary { get; set; }

        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        [JsonPropertyName("text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }

        [JsonPropertyName("sections")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Section>? Sections { get; set; }

        [JsonPropertyName("potentialAction")]
        public List<PotentialAction>? PotentialActions { get; set; }
    }
}
