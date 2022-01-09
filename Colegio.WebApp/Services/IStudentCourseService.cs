using Colegio.WebApp.Models;

namespace Colegio.WebApp.Services
{
    public interface IStudentCourseService : IService<StudentCourseViewModel>
    {

        Task<StudentCourseViewModel> GetOne(string Course_Id, string Student_Id, string token);
    }
}
