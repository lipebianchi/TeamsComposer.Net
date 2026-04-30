using System.Text.Json.Serialization;

namespace TeamsComposer.Net.Models.Common
{
    public class TeamsSimpleMessage
    {
        [JsonPropertyName("text")]
        public string Text { get; }
        public TeamsSimpleMessage(string text)
        {
            Text = text;
        }
    }
}
