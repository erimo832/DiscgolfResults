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

        public void Insert(IList<Serie> items)
        {
            Repository.Insert(items);
        }
    }
}
