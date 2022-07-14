using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;

namespace Results.Domain.Proxies
{
    internal class SeriesProxy : ISeriesProxy
    {
        private IDbSyncConfiguration Config { get; }

        public SeriesProxy(IDbSyncConfiguration syncConfiguration)
        {
            Config = syncConfiguration;
        }

        public IList<Serie> GetSeries()
        {
            var result = new List<Serie>();

            var series = File.ReadAllText(Config.SeriesSettingsPath).FromJson<SeriesListExternal>();

            foreach (var serie in series.Series)
            {
                result.Add(new Serie
                {
                    //SerieId = serie.
                    Name = serie.Name,
                    RoundsToCount = serie.RoundsToCount,
                    DefaultCourseLayout = serie.CourseLayoutId,
                    SourcePath = serie.RoundsPath
                });
            }

            return result;
        }
    }
}
