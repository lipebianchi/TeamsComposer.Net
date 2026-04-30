using TeamsComposer.Net.Models.AdaptiveCards.Roots;

namespace TeamsComposer.Net.Abstractions.AdaptiveCards
{
    public interface IAdaptiveCardBuilder
    {
        IAdaptiveCardBuilder WithVersion(string version);

        IAdaptiveCardBuilder WithFallbackText(string text);

        IAdaptiveCardBuilder AddTextBlock(string text, string? size = null, string? weight = null, string? color = null);

        IAdaptiveCardBuilder AddImage(string url, string? style = null, string? size = null);

        IAdaptiveCardBuilder AddFact(string title, string value);
        IAdaptiveCardBuilder AddMarkdownList<T>(IEnumerable<T> items, Func<T, string> itemFormatter);
        IAdaptiveCardBuilder AddMarkdownList<T>(IEnumerable<T> items);
        IAdaptiveCardBuilder AddContainer(Action<IAdaptiveContainerBuilder> configure);

        IAdaptiveCardBuilder AddOpenUrlAction(string title, string url);

        IAdaptiveCardBuilder AddSubmitAction(string title, object? data = null);

        AdaptiveCard Build();
    }
}
