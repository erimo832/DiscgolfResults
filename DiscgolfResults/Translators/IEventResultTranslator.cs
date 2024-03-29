﻿using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface IEventResultTranslator
    {
        IList<EventResultResponse> Translate(IList<Event> events, IList<Player> players);
        IList<EventResponse> Translate(IList<Event> events);
    }
}
