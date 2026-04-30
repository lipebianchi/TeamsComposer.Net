using TeamsComposer.Net.Builders.MessageCards;

namespace TeamsComposer.Net.Tests.Unit.MessageCards
{
    public class TeamsMessageBuilderTests
    {
        private readonly TeamsMessageBuilder _builder = new();

        [Fact]
        public void WithThemeColor_ShouldRemoveHash_WhenProvided()
        {
            
            var card = _builder.WithThemeColor("#FF0000").WithTitle("Test").WithSummary("Test").Build();

            Assert.Equal("FF0000", card.ThemeColor);
        }

        [Fact]
        public void WithTitle_ShouldSetTitleCorrectly()
        {
            var card = _builder.WithTitle("Alert Title").WithSummary("Test").Build();
            Assert.Equal("Alert Title", card.Title);
        }

        [Fact]
        public void AddButton_ShouldAddPotentialActionWithCorrectData()
        {
            var card = _builder.WithTitle("Test")
                    .WithSummary("Test")
                    .AddButton("Click Here", "https://google.com")
                    .Build();

            Assert.Single(card.PotentialActions!);
            Assert.Equal("Click Here", card.PotentialActions![0].Name);
            Assert.Equal("https://google.com", card.PotentialActions[0].Targets![0].Uri);
        }

        [Fact]
        public void WithText_ShouldSetTextCorrectly()
        {
            var card = _builder.WithText("Card text").WithTitle("Test").WithSummary("S").Build();

            Assert.Equal("Card text", card.Text);
        }

        [Fact]
        public void AddSection_ShouldAddSectionToCard()
        {
            var card = _builder.WithTitle("T").WithSummary("S")
                               .AddSection(s => s.WithTitle("Section Title"))
                               .Build();

            Assert.Single(card.Sections!);
            Assert.Equal("Section Title", card.Sections![0].Title);
        }

        [Fact]
        public void Build_ShouldThrowException_WhenTitleAndSummaryAreMissing()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _builder.Build());

            Assert.Contains("must have at least a Title and a Summary", exception.Message);
        }
    }
}
