using TeamsComposer.Net.Abstractions.AdaptiveCards;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Common;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;

namespace TeamsComposer.Net.Builders.AdaptiveCards.Roots
{
    public class AdaptiveCardBuilder : IAdaptiveCardBuilder
    {
        private readonly AdaptiveCard _card = new();

        public IAdaptiveCardBuilder WithVersion(string version)
        {
            _card.Version = version;
            return this;
        }

        public IAdaptiveCardBuilder WithFallbackText(string text)
        {
            _card.FallbackText = text;
            return this;
        }

        public IAdaptiveCardBuilder AddTextBlock(string text, string? size = null, string? weight = null, string? color = null)
        {
            _card.Body.Add(new AdaptiveTextBlock(text)
            {
                Size = size,
                Weight = weight,
                Color = color
            });
            return this;
        }

        public IAdaptiveCardBuilder AddImage(string url, string? style = null, string? size = null)
        {
            _card.Body.Add(new AdaptiveImage(url)
            {
                Style = style,
                Size = size
            });
            return this;
        }

        public IAdaptiveCardBuilder AddFact(string title, string value)
        {
            var lastElement = _card.Body.LastOrDefault();
            if (lastElement is not AdaptiveFactSet factSet)
            {
                factSet = new AdaptiveFactSet();
                _card.Body.Add(factSet);
            }

            factSet.Facts.Add(new AdaptiveFact(title, value));
            return this;
        }

        public IAdaptiveCardBuilder AddMarkdownList<T>(IEnumerable<T> items, Func<T, string> itemFormatter)
        {
            if (items == null || !items.Any())
                return this;

            var formattedItems = items.Select(itemFormatter);

            var joinedText = "- " + string.Join("\n- ", formattedItems);

            return AddTextBlock(joinedText);
        }

        public IAdaptiveCardBuilder AddMarkdownList(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
                return this;

            var joinedText = "- " + string.Join("\n- ", items);
            return AddTextBlock(joinedText);
        }

        public IAdaptiveCardBuilder AddContainer(Action<IAdaptiveContainerBuilder> configure)
        {
            var containerBuilder = new AdaptiveContainerBuilder();
            configure(containerBuilder);
            _card.Body.Add(containerBuilder.Build());
            return this;
        }

        public IAdaptiveCardBuilder AddOpenUrlAction(string title, string url)
        {
            _card.Actions ??= new List<AdaptiveAction>();
            _card.Actions.Add(new AdaptiveOpenUrlAction(title, url));
            return this;
        }

        public IAdaptiveCardBuilder AddSubmitAction(string title, object? data = null)
        {
            _card.Actions ??= new List<AdaptiveAction>();
            _card.Actions.Add(new AdaptiveSubmitAction(title, data));
            return this;
        }

        public AdaptiveCard Build()
        {
            return _card;
        }
    }
}