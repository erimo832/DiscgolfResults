namespace Results.Domain.Proxies.Contracts
{
    public class SeriesListExternal
    {
        public SerieExternal[] Series { get; set; } = new SerieExternal[0];
    }

    public class SerieExternal
    {
        public string Name { get; set; } = "";
        public string RoundsPath { get; set; } = "";
        public int RoundsToCount { get; set; } = 1;
        public int CourseLayoutId { get; set; } = 1;
    }
}
