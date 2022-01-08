using System.ComponentModel.DataAnnotations;

namespace Colegio.WebApp.Models.User
{
    public class UserViewModel
    {
        [Required]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "É necessário informar o nome")]
        [Display(Name = "Nome de usuário")]
        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário informar o email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário informar o número de telefone")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Número de telefone")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "É necessário informar o ano de nascimento")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public DateTime Birthdate { get; set; }
    }
}
