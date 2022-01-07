using Domain.Entity;
using Infra.Infra.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Colegio.API.DTO;

namespace Nascimento.Software.Colegio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        [HttpGet]
        [Route("get-all-students-course")]
        public async Task<ActionResult> GetAllStudentsCourse([FromServices] IRepository<Student_Course> _studeCourseRepo)
        {
            return Ok(await _studeCourseRepo.GetAll());
        }
        [HttpPost]
        [Route("insert-new-student_course")]
        public async Task<ActionResult> InsertNewStudentCourse
            ([FromServices] IRepository<Student_Course> _studeCourseRepo,
            [FromBody] Student_CourseDTO dto)
        {
            if (ModelState.IsValid)
            {
                var studentCourse = new Student_Course()
                {
                    Student_Id = dto.Student_Id,
                    Course_Id = dto.Course_Id,
                    Created_Date = DateTime.Now,
                    Updated_At = DateTime.Now,
                };
                var inserted = await _studeCourseRepo.Create(studentCourse);
                if (inserted) return Ok();

                return BadRequest();
            }
            return BadRequest();
        }
    }
}
