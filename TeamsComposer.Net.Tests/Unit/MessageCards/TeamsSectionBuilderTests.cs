using TeamsComposer.Net.Builders.MessageCards;

namespace TeamsComposer.Net.Tests.Unit.MessageCards
{
    public class TeamsSectionBuilderTests
    {
        private readonly TeamsSectionBuilder _builder = new();

        [Fact]
        public void WithActivity_ShouldSetOnlyTitle_WhenOthersAreNull()
        {
            _builder.WithActivity("User Action");
            var section = _builder.Build();

            Assert.Equal("User Action", section.ActivityTitle);
            Assert.Null(section.ActivitySubtitle);
        }

        [Fact]
        public void WithActivity_ShouldSetFullInfo_WhenAllParamsProvided()
        {
            _builder.WithActivity("Title", "Subtitle", "https://image.com/img.png");
            var section = _builder.Build();
            Assert.Equal("Title", section.ActivityTitle);
            Assert.Equal("Subtitle", section.ActivitySubtitle);
            Assert.Equal("https://image.com/img.png", section.ActivityImage);
        }
        [Fact]
        public void WithHeroImage_ShouldSetHeroImageCorrectly()
        {
            _builder.WithHeroImage("https://image.com/hero.png");
            var section = _builder.Build();
            Assert.Equal("https://image.com/hero.png", section.HeroImage);
        }
        [Fact]
        public void AddFact_ShouldAddItemsToFactsList()
        {
            _builder.AddFact("Key", "Value");
            var section = _builder.Build();
            Assert.Single(section.Facts!);
            Assert.Equal("Key", section.Facts![0].Name);
        }

        [Fact]
        public void AddTextBlock_ShouldAppendWithDoubleNewLines()
        {
            _builder.AddTextBlock("Line 1").AddTextBlock("Line 2");
            var section = _builder.Build();
            Assert.Equal("Line 1\n\nLine 2", section.Text);
        }

        [Fact]
        public void AddMarkdownList_ShouldFormatWithDashes()
        {
            _builder.AddMarkdownList(new[] { "Item 1", "Item 2" });
            var section = _builder.Build();
            Assert.Contains("- Item 1", section.Text);
            Assert.Contains("- Item 2", section.Text);
        }

        [Fact]
        public void AddMarkdownNumberedList_ShouldFormatWithNumbers()
        {
            _builder.AddMarkdownNumberedList(new[] { "First", "Second" });
            var section = _builder.Build();
            Assert.Contains("1. First", section.Text);
            Assert.Contains("2. Second", section.Text);
        }

        [Fact]
        public void AddQuoteBlock_ShouldFormatWithAuthorInItalic()
        {
            _builder.AddQuoteBlock("To be or not to be", "Shakespeare");
            var section = _builder.Build();
            Assert.Contains("> To be or not to be", section.Text);
            Assert.Contains("> — *Shakespeare*", section.Text);
        }

        [Fact]
        public void AddCodeSnippet_ShouldFormatWithTripleBackticks()
        {
            _builder.AddCodeSnippet("var x = 10;", "csharp");
            var section = _builder.Build();

            var expected = $"```csharp{Environment.NewLine}var x = 10;{Environment.NewLine}```";
            Assert.Contains(expected, section.Text);
        }

        [Fact]
        public void WithTitle_ShouldSetTitle()
        {
            _builder.WithTitle("My section");
            var section = _builder.Build();

            Assert.Equal("My section", section.Title);
        }

        [Fact]
        public void WithStartGroup_ShouldSetStartGroupToTrue()
        {
            _builder.WithStartGroup();
            var section = _builder.Build();

            Assert.True(section.StartGroup);
        }

        [Fact]
        public void AddButton_ShouldAddOpenUriAction()
        {
            _builder.AddButton("Access", "https://link.com");
            var section = _builder.Build();

            Assert.Single(section.PotentialActions!);
            Assert.Equal("Access", section.PotentialActions![0].Name);
        }

        [Fact]
        public void AddPostButton_ShouldAddHttpPostAction()
        {
            _builder.AddPostButton("Send", "https://api.com", "{}");
            var section = _builder.Build();

            Assert.Single(section.PotentialActions!);
            Assert.Equal("Send", section.PotentialActions![0].Name);
        }

        [Fact]
        public void WithText_ShouldSetTextDirectly()
        {
            _builder.WithText("Text without StringBuilder");
            var section = _builder.Build();

            Assert.Equal("Text without StringBuilder", section.Text);
        }

        [Fact]
        public void AddMarkdownLink_ShouldFormatAsMarkdownLink()
        {
            _builder.AddMarkdownLink("Google", "https://google.com");
            var section = _builder.Build();

            Assert.Equal("[Google](https://google.com)", section.Text);
        }

        [Fact]
        public void AddMethods_ShouldNotThrow_WhenInputsAreNull()
        {
            var exception = Record.Exception(() =>
            {
                _builder.AddTextBlock(null!)
                        .AddMarkdownList(null!)
                        .AddQuoteBlock(null!)
                        .AddCodeSnippet(null!);
            });

            Assert.Null(exception);
        }
    }
}
