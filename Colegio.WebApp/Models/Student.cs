using System.ComponentModel.DataAnnotations;

namespace Colegio.WebApp.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

    }
}
