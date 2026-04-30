using System.Text.Json.Serialization;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;

namespace TeamsComposer.Net.Models.AdaptiveCards.Common
{
    [JsonDerivedType(typeof(AdaptiveTextBlock), "TextBlock")]
    [JsonDerivedType(typeof(AdaptiveContainer), "Container")]
    [JsonDerivedType(typeof(AdaptiveImage), "Image")]
    [JsonDerivedType(typeof(AdaptiveFactSet), "FactSet")]
    [JsonDerivedType(typeof(AdaptiveActionSet), "ActionSet")]
    public abstract class AdaptiveElement
    {
        [JsonPropertyName("type")]
        public abstract string Type { get; }

        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        [JsonPropertyName("spacing")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Spacing { get; set; }

        [JsonPropertyName("separator")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Separator { get; set; }
    }
}
