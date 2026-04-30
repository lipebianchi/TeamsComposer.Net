using System.Text.Json.Serialization;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Common;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;
using TeamsComposer.Net.Models.Common;
using TeamsComposer.Net.Models.MessageCards;

namespace TeamsComposer.Net.Models
{
    [JsonSourceGenerationOptions(
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(MessageCard))]
    [JsonSerializable(typeof(TeamsSimpleMessage))]

    [JsonSerializable(typeof(AdaptiveCard))]
    [JsonSerializable(typeof(AdaptiveElement))]
    [JsonSerializable(typeof(AdaptiveAction))]
    [JsonSerializable(typeof(TeamsAdaptiveMessagePayload))]
    public partial class TeamsComposerJsonContext : JsonSerializerContext
    {
    }
}
