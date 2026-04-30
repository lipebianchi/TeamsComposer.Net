using TeamsComposer.Net.Abstractions.AdaptiveCards;
using TeamsComposer.Net.Builders.AdaptiveCards.Roots;
using TeamsComposer.Net.Models.AdaptiveCards.Actions;
using TeamsComposer.Net.Models.AdaptiveCards.Common;
using TeamsComposer.Net.Models.AdaptiveCards.Roots;
using Xunit;

namespace TeamsComposer.Net.Tests.Unit.AdaptiveCards
{
    public class AdaptiveCardBuilderTests
    {
        private readonly IAdaptiveCardBuilder _builder = new AdaptiveCardBuilder();

        [Fact]
        public void WithVersion_ShouldSetVersionCorrectly()
        {
            _builder.WithVersion("1.5");
            var card = _builder.Build();

            Assert.Equal("1.5", card.Version);
        }

        [Fact]
        public void WithFallbackText_ShouldSetFallbackText()
        {
            _builder.WithFallbackText("Your client does not support Adaptive Cards.");
            var card = _builder.Build();

            Assert.Equal("Your client does not support Adaptive Cards.", card.FallbackText);
        }

        [Fact]
        public void AddTextBlock_ShouldAddElementToBody()
        {
            _builder.AddTextBlock("Root Text", size: "Medium", weight: "Bolder");
            var card = _builder.Build();

            Assert.Single(card.Body);
            var textBlock = Assert.IsType<AdaptiveTextBlock>(card.Body[0]);
            Assert.Equal("Root Text", textBlock.Text);
            Assert.Equal("Medium", textBlock.Size);
            Assert.Equal("Bolder", textBlock.Weight);
        }

        [Fact]
        public void AddImage_ShouldAddImageToBody()
        {
            _builder.AddImage("https://img.com/logo.png");
            var card = _builder.Build();

            Assert.Single(card.Body);
            var image = Assert.IsType<AdaptiveImage>(card.Body[0]);
            Assert.Equal("https://img.com/logo.png", image.Url);
        }

        [Fact]
        public void AddFact_ShouldCreateFactSetAndGroupFactsInBody()
        {
            _builder.AddFact("Environment", "Production")
                    .AddFact("Region", "us-east-1");

            var card = _builder.Build();

            Assert.Single(card.Body);
            var factSet = Assert.IsType<AdaptiveFactSet>(card.Body[0]);
            Assert.Equal(2, factSet.Facts.Count);
            Assert.Equal("Environment", factSet.Facts[0].Title);
            Assert.Equal("Production", factSet.Facts[0].Value);
        }

        [Fact]
        public void AddContainer_ShouldAddNestedContainerToBody()
        {
            _builder.AddContainer(c => c.WithStyle("emphasis"));
            var card = _builder.Build();

            Assert.Single(card.Body);
            var container = Assert.IsType<AdaptiveContainer>(card.Body[0]);
            Assert.Equal("emphasis", container.Style);
        }

        [Fact]
        public void AddOpenUrlAction_ShouldAddToActionListInRoot()
        {
            _builder.AddOpenUrlAction("View Logs", "https://aws.com/logs");
            var card = _builder.Build();

            Assert.NotNull(card.Actions);
            Assert.Single(card.Actions);
            var action = Assert.IsType<AdaptiveOpenUrlAction>(card.Actions[0]);
            Assert.Equal("View Logs", action.Title);
            Assert.Equal("https://aws.com/logs", action.Url);
        }

        [Fact]
        public void AddSubmitAction_ShouldAddToActionListInRoot()
        {
            var payloadData = new { command = "Retry" };
            _builder.AddSubmitAction("Try Again", payloadData);
            var card = _builder.Build();

            Assert.NotNull(card.Actions);
            Assert.Single(card.Actions);
            var action = Assert.IsType<AdaptiveSubmitAction>(card.Actions[0]);
            Assert.Equal("Try Again", action.Title);
            Assert.Equal(payloadData, action.Data);
        }
    }
}