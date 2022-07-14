namespace Results.Domain.Model
{
    public class Round
    {
        public int RoundId { get; set; }
        public int EventId { get; set; }
        public int CourseLayoutId { get; set; }
        public string RoundName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public List<RoundScore> RoundScores { get; set; } = new List<RoundScore>();
    }
}
