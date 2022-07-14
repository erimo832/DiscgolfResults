using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Results.Domain.Common.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static bool TryAddTransient<TInterface, TImplementation>(this IServiceCollection self, bool isSingleImplementation = true)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            if (isSingleImplementation && self.Any(x => x.ServiceType == typeof(TInterface)))
                return false;

            self.AddTransient<TInterface, TImplementation>();

            return true;
        }

        public static bool TryAddTransientImplementation<TInterface, TImplementation>(this IServiceCollection self)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            if (self.Any(x => x.ServiceType == typeof(TImplementation)))
                return false;

            self.AddTransient<TInterface, TImplementation>();

            return true;
        }

        public static bool TryAddTransiantCached<TInterface, TImplementation, TCachedImplementation>(this IServiceCollection self, bool useCached)
            where TInterface : class
            where TImplementation : class, TInterface
            where TCachedImplementation : class, TInterface
        {
            if (self.Any(x => x.ServiceType == typeof(TInterface)))
                return false;

            if (useCached)
                self.AddTransient<TInterface, TCachedImplementation>();
            else
                self.AddTransient<TInterface, TImplementation>();

            return true;
        }

        public static bool TryAddSingleton<TInterface, TImplementation>(this IServiceCollection self)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            if (self.Any(x => x.ServiceType == typeof(TInterface)))
                return false;

            self.AddSingleton<TInterface, TImplementation>();

            return true;
        }

        public static bool TryAddConfiguration<TInterface, TConcrete>(this IServiceCollection self, IConfiguration configuration) where TConcrete : TInterface
        {
            if (self.Any(x => x.ServiceType == typeof(TInterface)))
                return false;

            //Microsoft.Extensions.Configuration.Binder
            var conf = (TInterface)Activator.CreateInstance(typeof(TConcrete));
            configuration.Bind(typeof(TConcrete).Name, conf);
            self.AddSingleton(typeof(TInterface), conf);

            return true;
        }

        public static bool IsAdded<TInterface>(this IServiceCollection self)
        {
            return self.Any(x => x.ServiceType == typeof(TInterface));
        }
    }
}
