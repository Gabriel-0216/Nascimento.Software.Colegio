using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Student_Course
    {
        [Required]
        public Guid Course_Id { get; set; }
        [Required]
        public Guid Student_Id { get; set; }
        [Required]
        public bool Active { get; set; }

        public DateTime Created_Date { get; set; }
        public DateTime Updated_At { get; set; }

    }
}
