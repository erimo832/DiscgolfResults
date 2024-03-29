﻿using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IEventManager
    {
        Event Get(string eventName, int serieId, DateTime time);
        void UpdateEventResults();
        void Insert(IList<Event> items);

        IList<Event> GetBy(int seriesId = -1, int playerId = -1, int fromEventId = -1, int toEventId = -1, bool includeRounds = false, bool includePlayerEvents = false, bool includePlayerhcp = false);

        IList<EventScore> GetPlayerEvents(int playerId);
    }
}
