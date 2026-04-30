using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Actions
{
    public class AdaptiveSubmitAction : AdaptiveAction
    {
        public override string Type => "Action.Submit";

        [JsonPropertyName("data")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }

        public AdaptiveSubmitAction(string title, object? data = null) : base(title)
        {
            Data = data;
        }
    }
}
