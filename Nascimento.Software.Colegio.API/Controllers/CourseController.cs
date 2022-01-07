using Domain.Entity;
using Infra.Infra.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Colegio.API.DTO;

namespace Nascimento.Software.Colegio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        [Route("get-all-courses")]
        public async Task<ActionResult> GetAllCourses([FromServices] IRepository<Course> _courseRepo)
        {
            return Ok(await _courseRepo.GetAll());
        }
        [HttpPost]
        [Route("insert-new-course")]
        public async Task<ActionResult> InsertNewCourse
            ([FromServices] IRepository<Course> _courseRepo,
            [FromBody] CourseDTO CourseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = new Course()
                {
                    Created_Date = DateTime.Now,
                    Resume = CourseDTO.Resume,
                    Title = CourseDTO.Title,
                    Updated_At = DateTime.Now
                };
                var inserted = await _courseRepo.Create(course);
                if (inserted) return Ok();

                return BadRequest();
            }

            return BadRequest();
        }
    }
}
