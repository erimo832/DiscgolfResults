using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Proxies;
using Results.Domain.Proxies.Transformers;
using Results.Domain.Repository;
using Results.Domain.Service;

namespace Results.Domain
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddResultBackend(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISyncDatabaseService, SyncDatabaseService>();

            services.AddTransient<CsvTransformer, CsvTransformer>();
            services.AddTransient<XlsxTransformer, XlsxTransformer>();

            services.AddTransient<ISeriesProxy, SeriesProxy>();
            services.AddTransient<ICoursesProxy, CoursesProxy>();
            services.AddTransient<IEventsProxy, EventsProxy>();

            services.AddTransient<ICourseManager, CourseManager>();
            services.AddTransient<ISerieManager, SerieManager>();
            services.AddTransient<IEventManager, EventManager>();
            services.AddTransient<IHcpManager, HcpManager>();
            services.AddTransient<IPlayerManager, PlayerManager>();
            services.AddTransient<IRoundManager, RoundManager>();
            services.AddTransient<IHoleResultManager, HoleResultManager>();

            services.AddTransient<ICourseHoleRepository, CourseHoleRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IHoleResultRepository, HoleResultRepository>();
            services.AddTransient<ICourseLayoutRepository, CourseLayoutRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IPlayerEventRepository, PlayerEventRepository>();
            services.AddTransient<IRoundRepository, RoundRepository>();
            services.AddTransient<IRoundScoreRepository, RoundScoreRepository>();
            services.AddTransient<ISerieRepository, SerieRepository>();
            services.AddTransient<IPlayerCourseLayoutHcpRepository, PlayerCourseLayoutHcpRepository>();

            services.TryAddConfiguration<IDbSyncConfiguration, DbSyncConfiguration>(configuration);
            services.TryAddConfiguration<IDatabaseConfiguration, DatabaseConfiguration>(configuration);
            services.TryAddConfiguration<IHcpConfiguration, HcpConfiguration>(configuration);

            return services;
        }
    }
}
