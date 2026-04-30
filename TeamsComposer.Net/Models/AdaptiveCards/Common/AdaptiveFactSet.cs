using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.AdaptiveCards.Common
{
    public class AdaptiveFactSet : AdaptiveElement
    {
        public override string Type => "FactSet";

        [JsonPropertyName("facts")]
        public List<AdaptiveFact> Facts { get; set; } = new();
    }

    public class AdaptiveFact
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        public AdaptiveFact(string title, string value)
        {
            Title = title;
            Value = value;
        }
    }
}
