using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Common
{
    public class AdaptiveTextBlock : AdaptiveElement
    {
        public override string Type => "TextBlock";

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("size")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Size { get; set; }

        [JsonPropertyName("weight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Weight { get; set; }

        [JsonPropertyName("color")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Color { get; set; }

        [JsonPropertyName("isSubtle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsSubtle { get; set; }

        [JsonPropertyName("wrap")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool Wrap { get; set; }

        public AdaptiveTextBlock(string text)
        {
            Text = text;
            Wrap = true;
        }
    }
}
