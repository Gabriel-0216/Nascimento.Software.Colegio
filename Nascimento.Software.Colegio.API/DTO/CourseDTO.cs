using System.ComponentModel.DataAnnotations;

namespace Nascimento.Software.Colegio.API.DTO
{
    public class CourseDTO
    {
        [DataType(DataType.Text)]
        public string Title { get; set; } = string.Empty;
        [DataType(DataType.Text)]
        public string Resume { get; set; } = string.Empty;
    }
}
