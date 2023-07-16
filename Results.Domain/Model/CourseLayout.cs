namespace Results.Domain.Model
{
    public class CourseLayout
    {
        public int CourseLayoutId { get; set; }
        public int NumberOfHoles { get; set; }
        public string Name { get; set; } = "";
        public int Par { get; set; }
        public int HcpAdjustedPar { get; set; }
        public double HcpSlopeFactor { get; set; }
        public List<CourseHole> Holes { get; set; } = new List<CourseHole>();
        public List<PlayerCourseLayoutHcp> PlayerCourseLayoutHcps { get; set; } = new List<PlayerCourseLayoutHcp>();
    }
}
