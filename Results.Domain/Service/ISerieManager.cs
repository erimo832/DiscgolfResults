using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface ISerieManager
    {
        void Insert(IList<Serie> items);
    }
}
