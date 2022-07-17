using Npoi.Mapper;
using Npoi.Mapper.Attributes;
using Results.Domain.Configuration.External;
using Results.Domain.Model;
using Results.Domain.Service;

namespace Results.Domain.Proxies.Transformers
{
    internal class XlsxTransformer
    {
        private IPlayerManager PlayerManager { get; }
        private ICourseManager CourseManager { get; }
        private IHcpManager HcpManager { get; }
        private IEventManager EventManager { get; }

        public XlsxTransformer(IPlayerManager playerManager, ICourseManager courseManager, IHcpManager hcpManager, IEventManager eventManager)
        {
            PlayerManager = playerManager;
            CourseManager = courseManager;
            HcpManager = hcpManager;
            EventManager = eventManager;
        }

        public Event ParseXlsx(FileInfo file, Serie serie, IDuplicatePlayerConfiguration duplicatePlayers)
        {
            var mapper = new Mapper(file.FullName);

            var courseInfo = mapper.Take<PoolInformation>(1).FirstOrDefault();

            var courseLayout = CourseManager.GetLayout(serie.DefaultCourseLayout);


            //Add support to have multiple rounds for one event
            var e = EventManager.Get($"{CommonHelper.GetRoundNumber(file.Name)} - {serie.Name}", serie.SerieId, courseInfo?.Value?.Date ?? CommonHelper.GetRoundTime(file.Name));

            var r = new Round
            {
                CourseLayoutId = serie.DefaultCourseLayout,
                RoundName = "R1",
                StartTime = courseInfo?.Value?.Date ?? CommonHelper.GetRoundTime(file.Name)
            };

            var results = mapper.Take<ResultExcel>(0);

            foreach (var result in results)
            {
                var roundScore = result.Value;

                if (roundScore != null && !roundScore.Totalscore.Contains("DNF") && !roundScore.Totalscore.Contains("DNS"))
                {
                    //Check if missing hole scores
                    if (roundScore.IsDnfScore())
                        continue;

                    var player = PlayerManager.GetPlayer(roundScore?.PDGAnumber ?? "", roundScore?.Firstname ?? "", roundScore?.Lastname ?? "", duplicatePlayers);

                    var score = new RoundScore
                    {
                        NumberOfCtps = 0, //TODO: fix ctp
                        HoleResults = new List<HoleResult>(),
                        Score = Convert.ToInt32(roundScore?.Totalscore ?? "100"),
                        PlayerId = player?.PlayerId ?? 0,
                        Division = roundScore?.DivCode ?? "MPO"
                    };

                    score.RoundHcp = HcpManager.RoundHcp(score.Score, courseLayout);

                    //Todo refactor parse more dynamically?
                    if (!string.IsNullOrWhiteSpace(roundScore?.H1))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H1 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 1).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H2))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H2 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 2).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H3))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H3 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 3).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H4))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H4 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 4).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H5))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H5 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 5).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H6))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H6 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 6).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H7))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H7 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 7).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H8))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H8 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 8).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H9))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H9 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 9).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H10))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H10 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 10).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H11))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H11 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 11).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H12))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H12 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 12).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H13))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H13 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 13).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H14))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H14 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 14).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H15))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H15 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 15).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H16))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H16 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 16).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H17))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H17 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 17).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H18))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H18 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 18).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H19))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H19 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 19).CourseHoleId });
                    if (!string.IsNullOrWhiteSpace(roundScore?.H20))
                        score.HoleResults.Add(new HoleResult { Score = Convert.ToInt32(roundScore?.H20 ?? "0"), IsCtp = false, CourseHoleId = courseLayout.Holes.First(x => x.Number == 20).CourseHoleId });

                    r.RoundScores.Add(score);

                }
            }

            e.Rounds.Add(r);

            return e;
        }
    }

    public class ResultExcel
    {
        [Column("Place")]
        public string? Place { get; set; }
        [Column("DivCode")]
        public string? DivCode { get; set; }
        [Column("PDGA number")]
        public string? PDGAnumber { get; set; }
        [Column("First name")]
        public string? Firstname { get; set; }
        [Column("Last name")]
        public string? Lastname { get; set; }
        public string H1 { get; set; } = "";
        public string H2 { get; set; } = "";
        public string H3 { get; set; } = "";
        public string H4 { get; set; } = "";
        public string H5 { get; set; } = "";
        public string H6 { get; set; } = "";
        public string H7 { get; set; } = "";
        public string H8 { get; set; } = "";
        public string H9 { get; set; } = "";
        public string H10 { get; set; } = "";
        public string H11 { get; set; } = "";
        public string H12 { get; set; } = "";
        public string H13 { get; set; } = "";
        public string H14 { get; set; } = "";
        public string H15 { get; set; } = "";
        public string H16 { get; set; } = "";
        public string H17 { get; set; } = "";
        public string H18 { get; set; } = "";
        public string H19 { get; set; } = "";
        public string H20 { get; set; } = "";

        [Column("Total score")]
        public string Totalscore { get; set; } = "";

        public bool IsDnfScore()
        {
            if (H1 == null || 
                H2 == null ||
                H3 == null ||
                H4 == null ||
                H5 == null ||
                H6 == null ||
                H7 == null ||
                H8 == null ||
                H9 == null ||
                H10 == null ||
                H11 == null ||
                H12 == null ||
                H13 == null ||
                H14 == null ||
                H15 == null ||
                H16 == null ||
                H17 == null ||
                H18 == null ||
                H19 == null ||
                H20 == null)
                return true;

            return false;
        }
    }

    public class PoolInformation
    {
        [Column("Layout name")]
        public string? LayoutName { get; set; }
        [Column("Course name")]
        public string? CourseName { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }

    }
}
