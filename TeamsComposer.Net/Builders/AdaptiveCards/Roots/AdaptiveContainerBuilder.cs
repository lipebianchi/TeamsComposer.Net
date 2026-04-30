using TeamsComposer.Net.Abstractions.AdaptiveCards;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Common;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;

namespace TeamsComposer.Net.Builders.AdaptiveCards.Roots
{
    internal class AdaptiveContainerBuilder : IAdaptiveContainerBuilder
    {
        private readonly AdaptiveContainer _container = new();

        public IAdaptiveContainerBuilder WithStyle(string style)
        {
            _container.Style = style;
            return this;
        }

        public IAdaptiveContainerBuilder WithBleed(bool bleed = true)
        {
            _container.Bleed = bleed;
            return this;
        }

        public IAdaptiveContainerBuilder AddTextBlock(string text, string? size = null, string? weight = null, string? color = null)
        {
            _container.Items.Add(new AdaptiveTextBlock(text)
            {
                Size = size,
                Weight = weight,
                Color = color
            });
            return this;
        }

        public IAdaptiveContainerBuilder AddImage(string url, string? style = null, string? size = null)
        {
            _container.Items.Add(new AdaptiveImage(url)
            {
                Style = style,
                Size = size
            });
            return this;
        }

        public IAdaptiveContainerBuilder AddContainer(Action<IAdaptiveContainerBuilder> configure)
        {
            var childBuilder = new AdaptiveContainerBuilder();
            configure(childBuilder);
            _container.Items.Add(childBuilder.Build());
            return this;
        }

        public IAdaptiveContainerBuilder AddOpenUrlButton(string title, string url)
        {
            var lastElement = _container.Items.LastOrDefault();
            if (lastElement is not AdaptiveActionSet actionSet)
            {
                actionSet = new AdaptiveActionSet();
                _container.Items.Add(actionSet);
            }

            actionSet.Actions.Add(new AdaptiveOpenUrlAction(title, url));
            return this;
        }

        public IAdaptiveContainerBuilder AddSubmitButton(string title, object? data = null)
        {
            var lastElement = _container.Items.LastOrDefault();
            if (lastElement is not AdaptiveActionSet actionSet)
            {
                actionSet = new AdaptiveActionSet();
                _container.Items.Add(actionSet);
            }

            actionSet.Actions.Add(new AdaptiveSubmitAction(title, data));
            return this;
        }

        public IAdaptiveContainerBuilder AddFact(string title, string value)
        {
            var lastElement = _container.Items.LastOrDefault();
            if (lastElement is not AdaptiveFactSet factSet)
            {
                factSet = new AdaptiveFactSet();
                _container.Items.Add(factSet);
            }

            factSet.Facts.Add(new AdaptiveFact(title, value));
            return this;
        }

        public IAdaptiveContainerBuilder AddMarkdownList(IEnumerable<string> items)
        {
            if(items == null || !items.Any())
                return this;

            var joinedText = "- " + string.Join("\n- ", items);
            return AddTextBlock(joinedText);
        }

        public IAdaptiveContainerBuilder AddMarkdownList<T>(IEnumerable<T> items, Func<T, string> itemFormatter)
        {
            if (items == null || !items.Any())
                return this;

            var formattedItems = items.Select(itemFormatter);

            var joinedText = "- " + string.Join("\n- ", formattedItems);

            return AddTextBlock(joinedText);
        }

        public IAdaptiveContainerBuilder ForEach<T>(IEnumerable<T> items, Action<IAdaptiveContainerBuilder, T> configure)
        {
            if (items == null || !items.Any())
                return this;

            foreach(var item in items)
            {
                configure(this, item);
            }
            return this;
        }

        internal AdaptiveContainer Build()
        {
            return _container;
        }
    }
}