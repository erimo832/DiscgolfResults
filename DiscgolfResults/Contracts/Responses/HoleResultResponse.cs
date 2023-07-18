using DiscgolfResults.Extensions;

namespace DiscgolfResults.Contracts.Responses
{
    public class AverageHoleResultResponse //Wrong name
    {
        public int CourseLayoutId { get; set;}
        public string LayoutName { get; set; } = "";
        public ScoreDistribution[] ScoreDistributions { get; set; } = new ScoreDistribution[0];
        public HoleResultResponse[] HoleResults { get; set; } = new HoleResultResponse[0];
    }

    public class HoleResultResponse
    {
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Hios { get; set; }

        //From HoleResult
        public int CourseHoleId { get; set; }
        public double AverageScore { get; set; }

        public double DiffToPar => Math.Round(AverageScore - Par, 2);
        public IList<HoleScoreDistribution> ScoreDistibutions { get; set; } = new List<HoleScoreDistribution>();
    }

    public class HoleScoreDistribution
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

    public class ScoreDistribution
    {
        public int Score { get; set; }
        public int NumberOfTimes { get; set; }
    }
}
