using Results.Domain.Proxies;

namespace Results.Domain.Service
{
    public class SyncDatabaseService : ISyncDatabaseService
    {
        private ISeriesProxy ExternalSeries { get; }
        private ICoursesProxy ExternalCourses { get; }
        private IEventsProxy ExternalEvents { get; }
        private IEventManager EventManager { get; }
        private IHcpManager HcpManager { get; }
        private ISerieManager SeriesManager { get; }
        private ICourseManager CoursesManager { get; }

        public SyncDatabaseService(ISeriesProxy externalSeries,
            ICoursesProxy coursesProxy,
            IEventsProxy eventsProxy,
            IEventManager eventManager,
            IHcpManager hcpManager,
            ISerieManager seriesManager,
            ICourseManager coursesManager)
        {
            ExternalSeries = externalSeries;
            ExternalCourses = coursesProxy;
            ExternalEvents = eventsProxy;
            EventManager = eventManager;
            HcpManager = hcpManager;
            SeriesManager = seriesManager;
            CoursesManager = coursesManager;
        }

        public Task Sync()
        {
            if (HasDataSourceChanged())
            {
                DropDatabase();
                RebuildDatabase();
            }

            return Task.CompletedTask;
        }

        private bool HasDataSourceChanged()
        {
            return true;
        }

        private void DropDatabase()
        {
            var files = Directory.GetFiles("c:/db");

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private void RebuildDatabase()
        {
            //Synch courses
            var courses = ExternalCourses.GetCourses();
            CoursesManager.Insert(courses);

            //Synch series
            var series = ExternalSeries.GetSeries();
            SeriesManager.Insert(series);

            //Synch rounds, and players
            foreach (var serie in series)
            {
                var events = ExternalEvents.GetEvents(serie);
                EventManager.Insert(events);
            }

            //Synch courselayout hcps
            HcpManager.UpdateHcp();

            //Synch player events
            EventManager.UpdateEventResults();
        }
    }
}
