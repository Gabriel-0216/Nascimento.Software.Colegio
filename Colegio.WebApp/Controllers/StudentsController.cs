using Colegio.WebApp.Models;
using Colegio.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.WebApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IService<Student> _studentService;
        public StudentsController(IService<Student> service)
        {
            _studentService = service;
        }
        private string GetToken() => Request.Cookies["JwtToken"];
        [HttpGet]
        public async Task<IActionResult> StudentsList()
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Login", "Home");
            }
            var studentList = await _studentService.GetAll(token);

            return View(studentList);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Home");


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var token = GetToken();
            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                var inserted = await _studentService.Add(student, token);
                if (inserted) return RedirectToAction("StudentsList", "Students");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            return Unauthorized();

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            return Unauthorized();

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            return Unauthorized();

        }

    }
}
