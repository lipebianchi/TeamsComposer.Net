using System.Text.Json.Serialization;
using TeamsComposer.Net.Models.AdaptiveCards.Common;

namespace TeamsComposer.Net.Models.AdaptiveCards.Actions
{
    public class AdaptiveActionSet : AdaptiveElement
    {
        public override string Type => "ActionSet";

        [JsonPropertyName("actions")]
        public List<AdaptiveAction> Actions { get; set; } = new();
    }
}
