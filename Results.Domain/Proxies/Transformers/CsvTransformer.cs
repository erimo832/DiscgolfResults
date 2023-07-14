using Results.Domain.Configuration.External;
using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;
using Results.Domain.Service;

namespace Results.Domain.Proxies.Transformers
{
    internal class CsvTransformer
    {
        private IPlayerManager PlayerManager { get; }
        private ICourseManager CourseManager { get; }
        private IHcpManager HcpManager { get; }
        private IEventManager EventManager { get; }

        public CsvTransformer(IPlayerManager playerManager, ICourseManager courseManager, IHcpManager hcpManager, IEventManager eventManager)
        {
            PlayerManager = playerManager;
            CourseManager = courseManager;
            HcpManager = hcpManager;
            EventManager = eventManager;
        }

        private const char Separator = ',';

        private const int MaxColumns = 8;
        private const int Col_DivCode = 0;
        private const int Col_Place = 1;
        private const int Col_FirstName = 2;
        private const int Col_LastName = 3;
        private const int Col_PdgaNumber = 4;
        private const int Col_Score = 5;
        private const int Col_Total = 6;
        private const int Col_Ctp = 7;

        public Event ParseCsv(FileInfo file, SerieExternal serie, IDuplicatePlayerConfiguration duplicatePlayers)
        {
            var e = EventManager.Get($"{CommonHelper.GetRoundNumber(file.Name)} - {serie.Name}", serie.SerieId, CommonHelper.GetRoundTime(file.Name));
            var startTime = CommonHelper.GetRoundTime(file.Name);

            var r = new Round
            {
                CourseLayoutId = serie.GetLayoutId(startTime),
                RoundName = "R1",
                StartTime = CommonHelper.GetRoundTime(file.Name)
            };

            var courseLayout = CourseManager.GetLayout(r.CourseLayoutId);

            var lines = File.ReadAllLines(file.FullName);

            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(Separator);

                if (!columns[Col_Score].Contains("DNF"))
                {
                    var player = PlayerManager.GetPlayer(columns[Col_PdgaNumber], columns[Col_FirstName].Replace("=", "").Replace("\"", ""), columns[Col_LastName].Replace("=", "").Replace("\"", ""), duplicatePlayers);

                    var score = new RoundScore
                    {
                        NumberOfCtps = columns.Length == MaxColumns ? Convert.ToInt32(columns[Col_Ctp].Trim()) : 0,
                        HoleResults = new List<HoleResult>(),
                        Score = Convert.ToInt32(columns[Col_Score]),
                        PlayerId = player?.PlayerId ?? 0,
                        Division = columns[Col_DivCode].Replace("=", "").Replace("\"", "")
                    };

                    score.RoundHcp = HcpManager.RoundHcp(score.Score, courseLayout);

                    r.RoundScores.Add(score);
                }
            }

            e.Rounds.Add(r);

            return e;
        }
    }
}
