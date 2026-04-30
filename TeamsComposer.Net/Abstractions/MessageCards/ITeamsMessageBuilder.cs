using TeamsComposer.Net.Models.MessageCards;

namespace TeamsComposer.Net.Abstractions.MessageCards
{
    public interface ITeamsMessageBuilder
    {
        ITeamsMessageBuilder WithThemeColor(string hexColor);
        ITeamsMessageBuilder WithSummary(string summary);
        ITeamsMessageBuilder WithTitle(string title);
        ITeamsMessageBuilder WithText(string text);
        ITeamsMessageBuilder AddPostButton(string text, string targetUrl, string body);
        ITeamsMessageBuilder AddSection(Action<ITeamsSectionBuilder> configureSection);

        ITeamsMessageBuilder AddButton(string text, string uri);

        MessageCard Build();
    }
}
