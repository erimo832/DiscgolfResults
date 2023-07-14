namespace Results.Domain.Model.ReadObjects
{
    public class HoleResultRo
    {
        public int SerieId { get; set; }
        public int PlayerId { get; set; }
        public int EventId { get; set; }
        public DateTime EventStartTime { get; set; }
        public string LayoutName { get; set; } = "";
        public int CourseLayoutId { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }

        //From HoleResult
        public int HoleResultId { get; set; }
        public int RoundScoreId { get; set; }
        public int CourseHoleId { get; set; }
        public int Score { get; set; }
        public bool IsCtp { get; set; }
    }
}
