using Microsoft.Extensions.DependencyInjection;
using TeamsComposer.Net.Abstractions;
using TeamsComposer.Net.Http;

namespace TeamsComposer.Net.IoC
{
    public static class TeamsComposerExtensions
    {
        /// <summary>
        /// Registers the TeamsComposer and its required HttpClient services in the provided service collection. This allows you to inject ITeamsSender into your classes and use it to send messages to Microsoft Teams.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The original IServiceCollection</returns>
        public static IServiceCollection AddTeamsComposer(this IServiceCollection services)
        {
            services.AddHttpClient<ITeamsSender, TeamsSender>();

            return services;
        }
    }
}
