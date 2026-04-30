using System.Text.Json.Serialization;
using TeamsComposer.Net.Models.AdaptiveCards.Common;

namespace TeamsComposer.Net.Models.AdaptiveCards.Roots
{
    public class AdaptiveContainer : AdaptiveElement
    {
        public override string Type => "Container";

        [JsonPropertyName("style")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Style { get; set; } 

        [JsonPropertyName("bleed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Bleed { get; set; }

        [JsonPropertyName("verticalContentAlignment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? VerticalContentAlignment { get; set; } 

        [JsonPropertyName("items")]
        public List<AdaptiveElement> Items { get; set; } = new();
    }
}
