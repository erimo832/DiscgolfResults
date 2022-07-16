using DiscgolfResults.Transformers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DiscgolfResults.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTranslators(this IServiceCollection services)
        {
            services.TryAddTransient<IEventResultTranslator, EventResultTranslator>();

            return services;
        }
    }
}
