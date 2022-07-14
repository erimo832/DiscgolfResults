using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;
using Results.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IList<RoundScoreRo> GetScoresBy(int playerId, int courseLayoutId)
        {
            return RoundScoreRepository.GetScoresBy(playerId, courseLayoutId);
        }
    }
}
