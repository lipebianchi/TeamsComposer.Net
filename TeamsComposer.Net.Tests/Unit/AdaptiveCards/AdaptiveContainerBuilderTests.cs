using TeamsComposer.Net.Abstractions.AdaptiveCards;
using TeamsComposer.Net.Builders.AdaptiveCards.Roots;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Common;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;
using Xunit;

namespace TeamsComposer.Net.Tests.Unit.AdaptiveCards
{
    public class AdaptiveContainerBuilderTests
    {
        private readonly AdaptiveContainerBuilder _builder = new();

        [Fact]
        public void WithStyle_ShouldSetStyleCorrectly()
        {
            _builder.WithStyle("attention");
            var container = _builder.Build();

            Assert.Equal("attention", container.Style);
        }

        [Fact]
        public void WithBleed_ShouldSetBleedToTrue()
        {
            _builder.WithBleed();
            var container = _builder.Build();

            Assert.True(container.Bleed);
        }

        [Fact]
        public void AddTextBlock_ShouldAddElementWithCorrectProperties()
        {
            _builder.AddTextBlock("Hello", size: "Large", weight: "Bolder", color: "Good");
            var container = _builder.Build();

            Assert.Single(container.Items);
            var textBlock = Assert.IsType<AdaptiveTextBlock>(container.Items[0]);

            Assert.Equal("Hello", textBlock.Text);
            Assert.Equal("Large", textBlock.Size);
            Assert.Equal("Bolder", textBlock.Weight);
            Assert.Equal("Good", textBlock.Color);
        }

        [Fact]
        public void AddImage_ShouldAddElementWithCorrectProperties()
        {
            _builder.AddImage("https://img.com", style: "Person", size: "Small");
            var container = _builder.Build();

            Assert.Single(container.Items);
            var image = Assert.IsType<AdaptiveImage>(container.Items[0]);

            Assert.Equal("https://img.com", image.Url);
            Assert.Equal("Person", image.Style);
            Assert.Equal("Small", image.Size);
        }

        [Fact]
        public void AddContainer_ShouldAddNestedContainer()
        {
            _builder.AddContainer(c => c.WithStyle("good").AddTextBlock("Nested"));
            var container = _builder.Build();

            Assert.Single(container.Items);
            var nestedContainer = Assert.IsType<AdaptiveContainer>(container.Items[0]);

            Assert.Equal("good", nestedContainer.Style);
            Assert.Single(nestedContainer.Items);
        }

        [Fact]
        public void AddOpenUrlButton_ShouldCreateActionSetAndGroupActions()
        {
            _builder.AddOpenUrlButton("Google", "https://google.com")
                    .AddOpenUrlButton("Bing", "https://bing.com");

            var container = _builder.Build();

            Assert.Single(container.Items);
            var actionSet = Assert.IsType<AdaptiveActionSet>(container.Items[0]);

            Assert.Equal(2, actionSet.Actions.Count);
            var btn1 = Assert.IsType<AdaptiveOpenUrlAction>(actionSet.Actions[0]);
            Assert.Equal("Google", btn1.Title);
            Assert.Equal("https://google.com", btn1.Url);
        }

        [Fact]
        public void AddSubmitButton_ShouldCreateActionSetAndAssignData()
        {
            var mockData = new { action = "restart" };
            _builder.AddSubmitButton("Restart", mockData);
            var container = _builder.Build();

            Assert.Single(container.Items);
            var actionSet = Assert.IsType<AdaptiveActionSet>(container.Items[0]);

            Assert.Single(actionSet.Actions);
            var submitBtn = Assert.IsType<AdaptiveSubmitAction>(actionSet.Actions[0]);
            Assert.Equal("Restart", submitBtn.Title);
            Assert.Equal(mockData, submitBtn.Data);
        }

        [Fact]
        public void AddFact_ShouldCreateFactSetAndGroupFacts()
        {
            _builder.AddFact("Server", "AWS")
                    .AddFact("Status", "Online");

            var container = _builder.Build();

            Assert.Single(container.Items);
            var factSet = Assert.IsType<AdaptiveFactSet>(container.Items[0]);

            Assert.Equal(2, factSet.Facts.Count);
            Assert.Equal("Server", factSet.Facts[0].Title);
            Assert.Equal("Online", factSet.Facts[1].Value);
        }
    }
}