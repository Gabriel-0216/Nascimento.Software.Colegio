using Domain.Entity;
using Infra.Infra.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Colegio.API.DTO;

namespace Nascimento.Software.Colegio.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        [HttpPut]
        [Route("update-course")]
        public async Task<ActionResult> UpdateCourse
            ([FromServices] IRepository<Course> _courseRepo,
            [FromHeader] Guid CourseId,
            [FromBody] CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var course = new Course()
                {
                    Id = CourseId,
                    Resume = courseDTO.Resume,
                    Title = courseDTO.Title,
                    Updated_At = DateTime.Now,
                };
                var updated = await _courseRepo.Update(course);
                if (updated) return Ok();

                return BadRequest();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("delete-course")]
        public async Task<ActionResult> DeleteCourse
            ([FromServices] IRepository<Course> _courseRepo,
            [FromHeader] Guid courseId)
        {
            var course = new Course()
            {
                Id = courseId,
            };

            var deleted = await _courseRepo.Delete(course);
            if (deleted) return Ok();

            return BadRequest();
        }

        [HttpGet]
        [Route("get-course-by-id")]
        public async Task<ActionResult> GetCourseById
            ([FromServices] IRepository<Course> _courseRepo,
            [FromHeader] Guid courseId)
        {
            var course = await _courseRepo.GetOne(courseId.ToString());
            return Ok(course);
        }

    }
}
