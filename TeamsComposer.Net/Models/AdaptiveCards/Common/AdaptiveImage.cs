using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Common
{
    public class AdaptiveImage : AdaptiveElement
    {
        public override string Type => "Image";

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("size")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Size { get; set; }

        [JsonPropertyName("style")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Style { get; set; }

        public AdaptiveImage(string url)
        {
            Url = url;
        }
    }
}
