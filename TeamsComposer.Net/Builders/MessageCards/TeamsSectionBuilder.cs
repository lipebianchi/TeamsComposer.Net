using System.Text;
using TeamsComposer.Net.Abstractions.MessageCards;
using TeamsComposer.Net.Models.Common;
using TeamsComposer.Net.Models.MessageCards;

namespace TeamsComposer.Net.Builders.MessageCards
{
    internal class TeamsSectionBuilder : ITeamsSectionBuilder
    {
        private readonly Section _section = new();
        private readonly StringBuilder _textBuilder = new();

        public ITeamsSectionBuilder WithTitle(string title)
        {
            _section.Title = title;
            return this;
        }

        public ITeamsSectionBuilder WithStartGroup()
        {
            _section.StartGroup = true;
            return this;
        }

        public ITeamsSectionBuilder AddButton(string text, string url)
        {
            _section.PotentialActions ??= new List<PotentialAction>();
            _section.PotentialActions.Add(PotentialAction.CreateOpenUri(text, url));
            return this;
        }

        public ITeamsSectionBuilder AddPostButton(string text, string targetUrl, string body)
        {
            _section.PotentialActions ??= new List<PotentialAction>();
            _section.PotentialActions.Add(PotentialAction.CreateHttpPost(text, targetUrl, body));
            return this;
        }

        public ITeamsSectionBuilder WithActivity(string title, string? subtitle = null, string? imageUrl = null)
        {
            _section.ActivityTitle = title;
            _section.ActivitySubtitle = subtitle;
            _section.ActivityImage = imageUrl;
            return this;
        }

        public ITeamsSectionBuilder WithHeroImage(string imageUrl)
        {
            _section.HeroImage = imageUrl;
            return this;
        }

        public ITeamsSectionBuilder WithText(string text)
        {
            _section.Text = text;
            return this;
        }

        public ITeamsSectionBuilder AddFact(string name, string value)
        {
            _section.Facts ??= new List<Fact>();
            _section.Facts.Add(new Fact(name, value));
            return this;
        }

        public ITeamsSectionBuilder AddTextBlock(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return this;

            if (_textBuilder.Length > 0)
                _textBuilder.Append("\n\n");

            _textBuilder.Append(text);
            return this;
        }

        public ITeamsSectionBuilder AddMarkdownLink(string text, string url)
        { 
            return AddTextBlock($"[{text}]({url})");
        }

        public ITeamsSectionBuilder AddQuoteBlock(string text, string? author = null)
        {
            if (string.IsNullOrEmpty(text)) return this;

            var formattedText = "> " + text.Replace("\n", "\n> ");

            if (!string.IsNullOrWhiteSpace(author))
            {
                formattedText += $"\n>\n> — *{author}*";
            }

            return AddTextBlock(formattedText);
        }

        public ITeamsSectionBuilder AddMarkdownList(IEnumerable<string> items)
        {
            if (items == null || !items.Any()) return this;

            if (_textBuilder.Length > 0)
                _textBuilder.Append("\n\n");

            foreach (var item in items)
            {
                _textBuilder.AppendLine($"- {item}");
            }
            return this;
        }

        public ITeamsSectionBuilder AddMarkdownNumberedList(IEnumerable<string> items)
        {
            if (items == null || !items.Any()) return this;

            if (_textBuilder.Length > 0)
                _textBuilder.Append("\n\n");

            int index = 1;
            foreach (var item in items)
            {
                _textBuilder.AppendLine($"{index}. {item}");
                index++;
            }
            return this;
        }
        public ITeamsSectionBuilder AddCodeSnippet(string code, string? language = null)
        {
            if (string.IsNullOrWhiteSpace(code)) return this;

            if (_textBuilder.Length > 0)
                _textBuilder.Append("\n\n");

            _textBuilder.AppendLine($"```{language}");
            _textBuilder.AppendLine(code);
            _textBuilder.AppendLine("```");
            return this;
        }
        internal Section Build()
        {
            if(_textBuilder.Length > 0)
            {
                _section.Text = _textBuilder.ToString();
            }
            return _section;
        }
    }
}
