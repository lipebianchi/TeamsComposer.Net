namespace TeamsComposer.Net.Abstractions.MessageCards
{
    public interface ITeamsSectionBuilder
    {
        ITeamsSectionBuilder WithTitle(string title);
        ITeamsSectionBuilder WithActivity(string title, string? subtitle = null, string? imageUrl = null);
        ITeamsSectionBuilder WithText(string text);
        ITeamsSectionBuilder AddFact(string name, string value);

        ITeamsSectionBuilder WithStartGroup();
        ITeamsSectionBuilder AddButton(string text, string url);
        ITeamsSectionBuilder AddPostButton(string text, string targetUrl, string body);

        ITeamsSectionBuilder AddTextBlock(string text);
        ITeamsSectionBuilder AddMarkdownList(IEnumerable<string> items);
        ITeamsSectionBuilder AddMarkdownNumberedList(IEnumerable<string> items);
        ITeamsSectionBuilder AddCodeSnippet(string code, string? language = null);
        ITeamsSectionBuilder WithHeroImage(string imageUrl);

        ITeamsSectionBuilder AddMarkdownLink(string text, string url);
        ITeamsSectionBuilder AddQuoteBlock(string text, string? author = null);
    }
}
