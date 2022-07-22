using DiscgolfResults.Translators;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DiscgolfResults.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTranslators(this IServiceCollection services)
        {
            services.TryAddTransient<IEventResultTranslator, EventResultTranslator>();
            services.TryAddTransient<IPlayerTranslator, PlayerTranslator>();
            services.TryAddTransient<IPlayerDetailsTranslator, PlayerDetailsTranslator>();
            services.TryAddTransient<ISerieLeaderboardTranslator, SerieLeaderboardTranslator>();
            services.TryAddTransient<IPlayerHcpTranslator, PlayerHcpTranslator>();
            services.TryAddTransient<IHoleResultTranslator, HoleResultTranslator>();            

            return services;
        }
    }
}
