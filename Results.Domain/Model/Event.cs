namespace Results.Domain.Model
{
    public class Event
    {
        public int EventId { get; set; }
        public int SerieId { get; set; } //fk to series
        public string EventName { get; set; } = "";
        public DateTime StartTime { get; set; }

        public Serie Serie { get; set; } = null;
        public List<Round> Rounds { get; set; } = new List<Round>();
        public List<EventScore> PlayerEvents { get; set; } = new List<EventScore>();
        public List<PlayerCourseLayoutHcp> PlayerCourseLayoutHcp { get; set; } = new List<PlayerCourseLayoutHcp>();
    }
}
