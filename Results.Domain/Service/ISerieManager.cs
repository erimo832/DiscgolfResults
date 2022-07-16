using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface ISerieManager
    {
        IList<Serie> GetBy(int seriesId = -1, bool includeEvents = false, bool includePlayerEvents = false, bool includePlayerhcp = false);

        void Insert(IList<Serie> items);
    }
}
