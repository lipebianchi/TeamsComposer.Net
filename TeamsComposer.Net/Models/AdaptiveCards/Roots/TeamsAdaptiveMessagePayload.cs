using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Roots
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class TeamsAdaptiveMessagePayload
    {
        [JsonPropertyName("type")]
        public string Type { get; } = "message";

        [JsonPropertyName("attachments")]
        public List<TeamsAdaptiveAttachment> Attachments { get; set; } = new();

        public TeamsAdaptiveMessagePayload(AdaptiveCard card)
        {
            Attachments.Add(new TeamsAdaptiveAttachment { Content = card });
        }
    }
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class TeamsAdaptiveAttachment
    {
        [JsonPropertyName("contentType")]
        public string ContentType { get; } = "application/vnd.microsoft.card.adaptive";

        [JsonPropertyName("contentUrl")]
        public string? ContentUrl { get; set; } = null;

        [JsonPropertyName("content")]
        public AdaptiveCard Content { get; set; } = default!;
    }
}
