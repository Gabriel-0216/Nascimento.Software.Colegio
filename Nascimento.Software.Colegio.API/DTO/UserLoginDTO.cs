using System.ComponentModel.DataAnnotations;

namespace Nascimento.Software.Colegio.API.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;
    }
}
