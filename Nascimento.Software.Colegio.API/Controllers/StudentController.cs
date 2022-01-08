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
    public class StudentController : ControllerBase
    {

        [HttpGet]
        [Route("get-all-students")]
        public async Task<ActionResult> GetAllStudents
            ([FromServices] IRepository<Student> _studentRepo)
        {
            var p = await _studentRepo.GetAll();
            return Ok(p);
        }
        [HttpGet]
        [Route("get-one-student")]
        public async Task<ActionResult> GetOneStudent
            ([FromServices] IRepository<Student> _studentRepo,
            [FromHeader] string Id)
        {
            var p = await _studentRepo.GetOne(Id);
            return Ok(p);
        }
        [HttpPost]
        [Route("insert-new-student")]
        public async Task<ActionResult> InsertNewStudent
            ([FromServices] IRepository<Student> _studentRepo,
            [FromBody] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Name = studentDTO.Name,
                    Birthdate = studentDTO.Birthdate,
                    Email = studentDTO.Email,
                    Phone = studentDTO.Phone,
                    Create_Date = DateTime.Now,
                    Updated_Date = DateTime.Now,
                };
                var inserted = await _studentRepo.Create(student);
                if (inserted) return Ok();

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("delete-student")]
        public async Task<ActionResult> DeleteStudent
            ([FromServices] IRepository<Student> _studentRepo,
            [FromHeader] Guid Id)
        {
            var student = new Student() { Id = Id };
            var deleted = await _studentRepo.Delete(student);
            if (deleted) return Ok();

            return BadRequest();
        }
        [HttpPut]
        [Route("update-student")]
        public async Task<ActionResult> UpdateStudent
            ([FromServices] IRepository<Student> _studentRepo,
            [FromHeader] Guid Id,
            [FromBody] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    Id = Id,
                    Name = studentDTO.Name,
                    Email = studentDTO.Email,
                    Birthdate = studentDTO.Birthdate,
                    Phone = studentDTO.Phone,
                    Updated_Date = DateTime.Now,
                };

                var updated = await _studentRepo.Update(student);
                if (updated) return Ok();

                return BadRequest();
            }
            return BadRequest();
        }
    }
}
