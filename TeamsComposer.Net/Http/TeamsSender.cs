using System.Net.Http.Json;
using TeamsComposer.Net.Abstractions;
using TeamsComposer.Net.Models.Common;
using TeamsComposer.Net.Models;
using TeamsComposer.Net.Models.MessageCards;
using TeamsComposer.Net.Abstractions.MessageCards;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;

namespace TeamsComposer.Net.Http
{
    public class TeamsSender : ITeamsSender
    {
        private readonly HttpClient _httpClient;

        public TeamsSender(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task SendAsync(string webhookUrl, MessageCard card, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(webhookUrl))
                throw new ArgumentException("Webhook URL cannot be null or empty.", nameof(webhookUrl));

            ArgumentNullException.ThrowIfNull(card);


            var response = await _httpClient.PostAsJsonAsync(
                webhookUrl,
                card,
                TeamsComposerJsonContext.Default.MessageCard,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task SendAsync(string webhookUrl, string messageText, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(webhookUrl))
                throw new ArgumentException("Webhook URL cannot be null or empty.", nameof(webhookUrl));
            if (string.IsNullOrWhiteSpace(messageText))
                throw new ArgumentException("Message text cannot be null or empty.", nameof(messageText));

            var payload = new TeamsSimpleMessage(messageText);

            var response = await _httpClient.PostAsJsonAsync(
                webhookUrl,
                payload,
                TeamsComposerJsonContext.Default.TeamsSimpleMessage,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        public async Task SendAsync(string webhookUrl, AdaptiveCard card, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrWhiteSpace(webhookUrl))
                throw new ArgumentException("Webhook URL cannot be null or empty.", nameof(webhookUrl));
            
            ArgumentNullException.ThrowIfNull(card);

            var payload = new TeamsAdaptiveMessagePayload(card);

            var response = await _httpClient.PostAsJsonAsync(
                webhookUrl,
                payload,
                TeamsComposerJsonContext.Default.TeamsAdaptiveMessagePayload,
                cancellationToken);

            response.EnsureSuccessStatusCode();
        }
    }
}
