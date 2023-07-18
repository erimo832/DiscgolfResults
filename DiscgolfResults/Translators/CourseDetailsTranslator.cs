using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Extensions;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class CourseDetailsTranslator : ICourseDetailsTranslator
    {
        public CourseDetailsTranslator(IEventResultTranslator eventTranslator)
        {
            EventTranslator = eventTranslator;
        }

        private IEventResultTranslator EventTranslator { get; }

        public CourseDetailsResponse Translate(Course course, IList<Event> events)
        {
            return new CourseDetailsResponse
            {
                CourseId = course.CourseId,
                Name = course.Name,
                NumberOfEvents = events.Count,
                NumberOfPlayedRounds = events.SelectMany(x => x.PlayerEvents).Count(),
                AverageNumberOfPlayers = Math.Round(events.SelectMany(x => x.PlayerEvents).Count().ToDouble() / events.Count.ToDouble(), 2),
                UniqueNumberOfPlayers = events.SelectMany(x => x.PlayerEvents).Select(x => x.PlayerId).Distinct().Count(),
                Events = EventTranslator.Translate(events),
            };
        }
    }
}
