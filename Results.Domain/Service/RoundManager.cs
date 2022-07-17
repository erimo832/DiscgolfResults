using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;
using Results.Domain.Repository;

namespace Results.Domain.Service
{
    internal class RoundManager : IRoundManager
    {
        private IRoundRepository Repository { get; }
        private IRoundScoreRepository RoundScoreRepository { get; }

        public RoundManager(IRoundRepository repository, IRoundScoreRepository roundScoreRepository)
        {
            Repository = repository;
            RoundScoreRepository = roundScoreRepository;
        }

        public IList<Round> GetBy(int eventId)
        {
            return Repository.GetBy(eventId);
        }

        public IList<RoundScoreRo> GetScoresBy(int playerId = -1, int courseLayoutId = -1)
        {
            return RoundScoreRepository.GetScoresBy(playerId, courseLayoutId);
        }
    }
}
