using System.ComponentModel.DataAnnotations;

namespace Nascimento.Software.Colegio.API.DTO
{
    public class Student_CourseDTO
    {
        [Required]

        public Guid Course_Id { get; set; }
        [Required]

        public Guid Student_Id { get; set; }
        [Required]

        public bool Active { get; set; }
    }
}
