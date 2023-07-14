namespace Results.Domain.Proxies.Contracts
{
    public class SeriesListExternal
    {
        public SerieExternal[] Series { get; set; } = new SerieExternal[0];
    }

    public class SerieExternal
    {
        public int SerieId { get; set; } = -1;
        public string Name { get; set; } = "";
        public string RoundsPath { get; set; } = "";
        public int RoundsToCount { get; set; } = 1;
        public LayoutExt[] Layouts { get; set; } = new LayoutExt[0];

        public int GetLayoutId(DateTime start)
        {
            if (Layouts.Count() == 0)
                throw new ArgumentException($"No Layouts were configured for the serie. Update json-file");

            if (Layouts.Length == 1)
                return Layouts.First().CourseLayoutId;


            foreach (LayoutExt layout in Layouts)
            {
                if (layout.Until == null || start <= layout.Until)
                    return layout.CourseLayoutId;
            }

            throw new ArgumentException($"No valid Layout were found for time {start}. Update json-file");
        }
    }

    public class LayoutExt
    {
        public int CourseLayoutId { get; set; } = 1;
        public DateTime? Until { get; set; }
    }

}
