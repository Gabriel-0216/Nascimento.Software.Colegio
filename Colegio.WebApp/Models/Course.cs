using System.ComponentModel.DataAnnotations;

namespace Colegio.WebApp.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.MultilineText)]
        public string Resume { get; set; } = string.Empty;
    }
}
