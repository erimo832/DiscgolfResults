using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface ISerieRepository
    {
        void Insert(IList<Serie> items);
    }
}
