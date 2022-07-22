using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Service
{
    public interface IHoleResultManager
    {
        public IList<HoleResultRo> GetRoBy(int playerId = -1, int eventId = -1, int courseHoleId = -1);
        public IList<AverageHoleResultRo> GetAverageRoBy(int playerId = -1, int fromEventId = -1, int toEventId = -1);
    }
}
