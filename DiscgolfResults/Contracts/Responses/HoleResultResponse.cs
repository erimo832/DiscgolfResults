namespace DiscgolfResults.Contracts.Responses
{
    public class HoleResultResponse
    {
    }

    public class AverageHoleResultResponse
    {
        public int PlayerId { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }

        //From HoleResult
        public int CourseHoleId { get; set; }
        public double AverageScore { get; set; }

        public double DiffToPar => Math.Round(AverageScore - Par, 2);
    }
}
