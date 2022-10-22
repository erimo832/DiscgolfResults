using DiscgolfResults.Extensions;

namespace DiscgolfResults.Contracts.Responses
{
    public class HoleResultResponse
    {
    }

    public class AverageHoleResultResponse
    {
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Hios { get; set; }

        //From HoleResult
        public int CourseHoleId { get; set; }
        public double AverageScore { get; set; }

        public double DiffToPar => Math.Round(AverageScore - Par, 2);
        public IList<ScoreDistibution> ScoreDistibutions { get; set; } = new List<ScoreDistibution>();
    }

    public class ScoreDistibution
    {
        public int NumberOfScores { get; set; }
        public int TotalPlayed { get; set; }
        public RelativeScore RelativeScore { get; set; }

        public double Percentage => (NumberOfScores.ToDouble() / TotalPlayed.ToDouble()).ToPercent(2);
    }

    public enum RelativeScore
    {
        Eagle = -2,
        Birdie = -1,
        Par = 0,
        Bogey = 1,
        DoubleBogey = 2,
        PlusBogey = 3
    }
}
