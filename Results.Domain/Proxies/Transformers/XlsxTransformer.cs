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
                        NumberOfCtps = 0, 
                        HoleResults = new List<HoleResult>(),
                        Score = Convert.ToInt32(roundScore?.Totalscore ?? "100"),
                        PlayerId = player?.PlayerId ?? 0,
                        Division = roundScore?.DivCode ?? "MPO"
                    };

                    score.RoundHcp = HcpManager.RoundHcp(score.Score, courseLayout);

                    //Todo refactor parse more dynamically?
                    AddHoleResult(score.HoleResults, roundScore?.H1, 1, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H2, 2, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H3, 3, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H4, 4, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H5, 5, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H6, 6, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H7, 7, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H8, 8, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H9, 9, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H10, 10, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H11, 11, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H12, 12, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H13, 13, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H14, 14, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H15, 15, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H16, 16, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H17, 17, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H18, 18, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H19, 19, courseLayout);
                    AddHoleResult(score.HoleResults, roundScore?.H20, 20, courseLayout);

                    //Update number of ctps
                    score.NumberOfCtps = score.HoleResults.Count(x => x.IsCtp);

                    r.RoundScores.Add(score);
                }
            }

            e.Rounds.Add(r);

            return e;
        }

        private void AddHoleResult(List<HoleResult> results, string value, int holeNumber, CourseLayout? courseLayout)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            var vals = value.Split(',');

            results.Add(new HoleResult
            {
                Score = Convert.ToInt32(vals[0] ?? "0"),
                IsCtp = vals.Length == 1 ? false : true,
                CourseHoleId = courseLayout.Holes.First(x => x.Number == 1).CourseHoleId
            });
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
