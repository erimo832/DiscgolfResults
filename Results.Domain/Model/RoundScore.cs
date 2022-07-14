namespace Results.Domain.Model
{
    public class RoundScore
    {
        public int RoundScoreId { get; set; }
        public int RoundId { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
        //public int Placement { get; set; } //Is calculated
        public string Division { get; set; } = "";
        public int NumberOfCtps { get; set; } //For legacy
        public double RoundHcp { get; set; }
        public Round Round { get; set; }
        public List<HoleResult> HoleResults { get; set; } = new List<HoleResult>();


        /*
        RoundHcp
        ThrowPoints?
        HcpPoints ( (score - courseadjPar) * 0.8 )

         */

    }
}
