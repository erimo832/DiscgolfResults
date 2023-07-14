using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Configuration.External;
using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;
using Results.Domain.Proxies.Transformers;

namespace Results.Domain.Proxies
{
    internal class EventsProxy : IEventsProxy
    {
        private XlsxTransformer XlsxTransformer { get; }
        private CsvTransformer CsvTransformer { get; }
        private IDbSyncConfiguration Config { get; }

        public EventsProxy(XlsxTransformer xlsxTransformer, CsvTransformer csvTransformer, IDbSyncConfiguration syncConfiguration)
        {
            XlsxTransformer = xlsxTransformer;
            CsvTransformer = csvTransformer;
            Config = syncConfiguration;
        }

        public IList<Event> GetEvents(SerieExternal serie)
        {
            var files = new DirectoryInfo(serie.RoundsPath).GetFiles();
            var duplicatePlayers = File.ReadAllText(Config.DuplicatePlayersSettingsPath).FromJson<DuplicatePlayerConfiguration>();

            var result = new List<Event>();

            foreach (var file in files)
            {
                if (file.Name.EndsWith(".csv"))
                    result.Add(CsvTransformer.ParseCsv(file, serie, duplicatePlayers));
                else if (file.Name.EndsWith(".xlsx"))
                    result.Add(XlsxTransformer.ParseXlsx(file, serie, duplicatePlayers));
            }

            return result;
        }
    }
}
