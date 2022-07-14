using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IHoleResultRepository
    {
        void Insert(IList<HoleResult> items);
    }
}
