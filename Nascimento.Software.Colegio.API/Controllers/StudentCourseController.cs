using Domain.Entity;
using Infra.Infra.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Colegio.API.DTO;

namespace Nascimento.Software.Colegio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentCourseController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("get-all-students-course")]
        public async Task<ActionResult> GetAllStudentsCourse([FromServices] IStudentCourseRepository _studeCourseRepo)
        {
            return Ok(await _studeCourseRepo.GetAll());
        }
        [HttpPost]
        [Route("insert-new-student_course")]
        public async Task<ActionResult> InsertNewStudentCourse
            ([FromServices] IStudentCourseRepository _studeCourseRepo,
            [FromBody] Student_CourseDTO dto)
        {
            if (ModelState.IsValid)
            {
                var studentCourse = new Student_Course()
                {
                    Student_Id = dto.Student_Id,
                    Course_Id = dto.Course_Id,
                    Active = dto.Active,
                    Created_Date = DateTime.Now,
                    Updated_At = DateTime.Now,
                };
                var inserted = await _studeCourseRepo.Create(studentCourse);
                if (inserted) return Ok();

                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("update-student_course-status")]
        public async Task<ActionResult> UpdateStudentCourseStatus
            ([FromServices] IStudentCourseRepository _studeCourseRepo,
            [FromBody] Student_CourseDTO dto)
        {
            if (ModelState.IsValid)
            {
                var studentCourse = new Student_Course()
                {
                    Student_Id = dto.Student_Id,
                    Active = dto.Active,
                    Course_Id = dto.Course_Id,
                    Updated_At = DateTime.Now,
                };
                var updated = await _studeCourseRepo.Update(studentCourse);
                if (updated) return Ok();

                return BadRequest();
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("get-student_course-by-id")]
        public async Task<ActionResult> GetStudentCourseById
            ([FromServices] IStudentCourseRepository _studeCourseRepo,
            [FromHeader] string Course_Id, [FromHeader] string student_Id)
        {
            var get = await _studeCourseRepo.GetOne(Course_Id, student_Id);
            return Ok(get);
        }
    }
}
