namespace Results.Domain.Model
{
    public class HoleResult
    {
        public int HoleResultId { get; set; }
        public int RoundScoreId { get; set; }
        public int CourseHoleId { get; set; }
        public int Score { get; set; }
        public bool IsCtp { get; set; }
    }
}
