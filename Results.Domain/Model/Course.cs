namespace Results.Domain.Model
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = "";
        public List<CourseLayout> Layouts { get; set; } = new List<CourseLayout>();
    }
}
