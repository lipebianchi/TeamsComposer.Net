using System.Text.Json.Serialization;
using TeamsComposer.Net.Models.Common;

namespace TeamsComposer.Net.Models.MessageCards
{
    public class Section
    {
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        [JsonPropertyName("activityTitle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ActivityTitle { get; set; }

        [JsonPropertyName("activitySubtitle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ActivitySubtitle { get; set; }

        [JsonPropertyName("text")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }

        [JsonPropertyName("facts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Fact>? Facts { get; set; }

        [JsonPropertyName("activityImage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ActivityImage { get; set; }

        [JsonPropertyName("heroImage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? HeroImage { get; set; }

        [JsonPropertyName("startGroup")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? StartGroup { get; set; }

        [JsonPropertyName("potentialAction")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<PotentialAction>? PotentialActions { get; set; }

        [JsonPropertyName("markdown")]
        public bool Markdown { get; set; } = true;
    }
}
