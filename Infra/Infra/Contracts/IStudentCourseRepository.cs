using Domain.Entity;

namespace Infra.Infra.Contracts
{
    public interface IStudentCourseRepository : IRepository<Student_Course>
    {
        Task<Student_Course> GetOne(string Course_Id, string Student_Id);
    }
}
