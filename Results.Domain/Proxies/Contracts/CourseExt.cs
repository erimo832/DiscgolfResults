namespace Results.Domain.Proxies.Contracts
{
    public class CourseListExternal
    {
        public CourseExternal[] Courses { get; set; } = new CourseExternal[0];
    }

    public class CourseExternal
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = "";
        public CourseLayoutExternal[] Layouts { get; set; } = new CourseLayoutExternal[0];
    }

    public class CourseLayoutExternal
    {
        public int CourseLayoutId { get; set; } = 1;
        public string Name { get; set; } = "";
        public int NumberOfHoles { get; set; }
        public int CourseAdjustedPar { get; set; } = 0;
        public CourseHoleExternal[] Holes { get; set; } = new CourseHoleExternal[0];
    }

    public class CourseHoleExternal
    {
        public int Number { get; set; }
        public int Par { get; set; }
    }
}
