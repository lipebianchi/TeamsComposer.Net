using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Actions
{
    public class AdaptiveOpenUrlAction : AdaptiveAction
    {
        public override string Type => "Action.OpenUrl";

        [JsonPropertyName("url")]
        public string Url { get; set; }

        public AdaptiveOpenUrlAction(string title, string url) : base(title)
        {
            Url = url;
        }
    }
}
