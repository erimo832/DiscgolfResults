using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IRoundRepository
    {
        void Insert(IList<Round> items);
        IList<Round> GetBy(int eventId);
    }
}
