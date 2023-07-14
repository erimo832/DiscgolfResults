using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
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

        public SeriesListExternal GetSeries()
        {
            return File.ReadAllText(Config.SeriesSettingsPath).FromJson<SeriesListExternal>();
        }
    }
}
