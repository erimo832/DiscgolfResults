using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Repository
{
    public interface IHoleResultRepository
    {
        void Insert(IList<HoleResult> items);

        public IList<HoleResultRo> GetRoBy(int playerId = -1, int fromEventId = -1, int toEventId = -1, int courseHoleId = -1);
    }
}
