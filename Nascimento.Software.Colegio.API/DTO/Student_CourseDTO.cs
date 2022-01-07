using System.ComponentModel.DataAnnotations;

namespace Nascimento.Software.Colegio.API.DTO
{
    public class Student_CourseDTO
    {
        public Guid Course_Id { get; set; }
        public Guid Student_Id { get; set; }
    }
}
