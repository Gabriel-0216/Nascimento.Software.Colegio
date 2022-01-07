using System.ComponentModel.DataAnnotations;

namespace Nascimento.Software.Colegio.API.DTO
{
    public class StudentDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

    }
}
