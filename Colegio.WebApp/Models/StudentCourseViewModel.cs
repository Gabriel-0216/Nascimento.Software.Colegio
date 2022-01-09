using System.ComponentModel.DataAnnotations;

namespace Colegio.WebApp.Models
{
    public class StudentCourseViewModel
    {
        [Display(Name = "Curso")]
        public Guid Course_Id { get; set; }

        [Display(Name = "Estudante")]
        public Guid Student_Id { get; set; }
        public bool Active { get; set; }
        public List<Course>? Courses { get; set; }
        public List<Student>? Students { get; set; }

    }
}
