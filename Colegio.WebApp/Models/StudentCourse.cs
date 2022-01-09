namespace Colegio.WebApp.Models
{
    public class StudentCourse
    {
        public List<StudentCourseViewModel>? StudentCourses { get; set; }
        public List<Student>? Students { get; set; }
        public List<Course>? Courses { get; set; }

    }

    public class StudentCourseVm
    {
        public Student? student { get; set; }
        public Course? course { get; set; }
        public bool Active { get; set; }
        public Guid Course_Id { get; set; }
        public Guid Student_Id { get; set; }
    }
}
