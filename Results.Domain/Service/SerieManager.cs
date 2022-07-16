using Results.Domain.Model;
using Results.Domain.Repository;

namespace Results.Domain.Service
{
    internal class SerieManager : ISerieManager
    {
        public SerieManager(ISerieRepository repository)
        {
            Repository = repository;
        }

        private ISerieRepository Repository { get; }

        public IList<Serie> GetBy(int seriesId = -1, bool includeEvents = false, bool includePlayerEvents = false, bool includePlayerhcp = false)
        {
            return Repository.GetBy(seriesId, includeEvents, includePlayerEvents, includePlayerhcp);
        }

        public void Insert(IList<Serie> items)
        {
            Repository.Insert(items);
        }
    }
}
