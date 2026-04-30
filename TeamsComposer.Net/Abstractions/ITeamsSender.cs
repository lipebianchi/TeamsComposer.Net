using TeamsComposer.Net.Models.AdaptiveCards.Roots;
using TeamsComposer.Net.Models.MessageCards;

namespace TeamsComposer.Net.Abstractions
{
    public interface ITeamsSender
    {
        Task SendAsync(string webhookUrl, MessageCard message, CancellationToken cancellationToken = default);
        Task SendAsync(string webhookUrl, string messageText, CancellationToken cancellationToken = default);
        Task SendAsync(string webhookUrl, AdaptiveCard card, CancellationToken cancellationToken = default);
    }
}
