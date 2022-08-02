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
                AverageNumerOfPlayers = Math.Round(events.SelectMany(x => x.PlayerEvents).Count().ToDouble() / events.Count.ToDouble(), 2),
                Events = EventTranslator.Translate(events)
            };
        }
    }
}
