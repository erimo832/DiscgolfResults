﻿using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Service
{
    internal interface IRoundManager
    {
        IList<Round> GetBy(int eventId);
        IList<RoundScoreRo> GetScoresBy(int playerId, int courseLayoutId);
    }
}
