using Results.Domain.Model.ReadObjects;
using Results.Domain.Repository;

namespace Results.Domain.Service
{
    internal class HoleResultManager : IHoleResultManager
    {
        public HoleResultManager(IHoleResultRepository respository)
        {
            HoleRespository = respository;
        }

        private IHoleResultRepository HoleRespository { get; }

        public IList<AverageHoleResultRo> GetAverageRoBy(int playerId = -1, int fromEventId = -1, int toEventId = -1)
        {
            return HoleRespository.GetAverageRoBy(playerId, fromEventId, toEventId);
        }

        public IList<HoleResultRo> GetRoBy(int playerId = -1, int fromEventId = -1, int toEventId = -1, int courseHoleId = -1)
        {
            return HoleRespository.GetRoBy(playerId, fromEventId, toEventId, courseHoleId);
        }
    }
}
