namespace Results.Domain.Model.ReadObjects
{
    public class RoundScoreRo : RoundScore
    {
        public int CourseLayoutId { get; set; }
        public int EventId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
