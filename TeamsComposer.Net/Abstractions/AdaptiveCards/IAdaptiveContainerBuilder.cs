namespace TeamsComposer.Net.Abstractions.AdaptiveCards
{
    public interface IAdaptiveContainerBuilder
    {
        IAdaptiveContainerBuilder WithStyle(string style);
        
        IAdaptiveContainerBuilder WithBleed(bool bleed = true);
        
        IAdaptiveContainerBuilder AddTextBlock(string text, string? size = null, string? weight = null, string? color = null);
        
        IAdaptiveContainerBuilder AddImage(string url, string? style = null, string? size = null);
        
        IAdaptiveContainerBuilder AddContainer(Action<IAdaptiveContainerBuilder> configure);
        
        IAdaptiveContainerBuilder AddOpenUrlButton(string title, string url);
        
        IAdaptiveContainerBuilder AddSubmitButton(string title, object? data = null);
        
        IAdaptiveContainerBuilder AddFact(string title, string value);

        IAdaptiveContainerBuilder AddMarkdownList(IEnumerable<string> items);
        IAdaptiveContainerBuilder AddMarkdownList<T>(IEnumerable<T> items, Func<T, string> itemFormatter);
        IAdaptiveContainerBuilder ForEach<T>(IEnumerable<T> items, Action<IAdaptiveContainerBuilder, T> configure);
    }
}
