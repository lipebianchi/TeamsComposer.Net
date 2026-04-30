using TeamsComposer.Net.Abstractions.MessageCards;
using TeamsComposer.Net.Models.MessageCards;

namespace TeamsComposer.Net.Builders.MessageCards
{
    public class TeamsMessageBuilder : ITeamsMessageBuilder
    {
        private readonly MessageCard _card = new();

        public ITeamsMessageBuilder WithThemeColor(string hexColor)
        {
            _card.ThemeColor = hexColor.TrimStart('#');
            return this;
        }

        public ITeamsMessageBuilder WithText(string text)
        {
            _card.Text = text;
            return this;
        }

        public ITeamsMessageBuilder WithTitle(string title)
        {
            _card.Title = title;
            return this;
        }

        public ITeamsMessageBuilder WithSummary(string summary)
        {
            _card.Summary = summary;
            return this;
        }

        public ITeamsMessageBuilder AddSection(Action<ITeamsSectionBuilder> configureSection)
        {
            var sectionBuilder = new TeamsSectionBuilder();
            configureSection(sectionBuilder);

            _card.Sections ??= new List<Section>();
            _card.Sections.Add(sectionBuilder.Build());

            return this;
        }

        public ITeamsMessageBuilder AddButton(string text, string url)
        {
            _card.PotentialActions ??= new List<PotentialAction>();
            _card.PotentialActions.Add(PotentialAction.CreateOpenUri(text, url));
            return this;
        }

        public ITeamsMessageBuilder AddPostButton(string text, string targetUrl, string body)
        {
            _card.PotentialActions ??= new List<PotentialAction>();
            _card.PotentialActions.Add(PotentialAction.CreateHttpPost(text, targetUrl, body));
            return this;
        }

        public MessageCard Build()
        {
            if (string.IsNullOrWhiteSpace(_card.Title) && string.IsNullOrWhiteSpace(_card.Summary))
            {
                throw new InvalidOperationException("A MessageCard must have at least a Title and a Summary.");
            }

            return _card;
        }
    }
}
