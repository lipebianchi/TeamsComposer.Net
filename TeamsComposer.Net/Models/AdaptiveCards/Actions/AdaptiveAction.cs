using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Actions
{
    [JsonDerivedType(typeof(AdaptiveOpenUrlAction), "Action.OpenUrl")]
    [JsonDerivedType(typeof(AdaptiveSubmitAction), "Action.Submit")]
    public abstract class AdaptiveAction
    {
        [JsonPropertyName("type")]
        public abstract string Type { get; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("style")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Style { get; set; } 

        protected AdaptiveAction(string title)
        {
            Title = title;
        }
    }
}
