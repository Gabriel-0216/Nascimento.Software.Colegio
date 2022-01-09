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
        public async Task<IActionResult> Index()
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var studentList = await _studentService.GetAll(GetToken());
            return View(studentList.Take(5));
        }
        private string GetToken() => Request.Cookies["JwtToken"];
        private bool IsTokenValid()
        {
            var token = GetToken();
            if (token == null || string.IsNullOrWhiteSpace(token)) return false;

            var tokenExpireDate = Request.Cookies["ExpireDate"];
            if (tokenExpireDate == null || string.IsNullOrWhiteSpace(tokenExpireDate) 
                || DateTime.Now > Convert.ToDateTime(tokenExpireDate)) return false;

            return true;

        } 

        [HttpGet]
        public async Task<IActionResult> StudentsList()
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var studentList = await _studentService.GetAll(GetToken());

            return View(studentList);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                var inserted = await _studentService.Add(student, GetToken());
                if (inserted) return RedirectToAction("StudentsList", "Students");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var student = await _studentService.GetById(Id, GetToken());
            if (student == null) return RedirectToAction("Index", "Students");

            return View(student);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var updated = await _studentService.Update(student, GetToken());
            if (updated) return RedirectToAction("StudentsList", "Students");

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var student = await _studentService.GetById(Id, GetToken());
            if (student == null) return RedirectToAction("Index", "Students");

            return View(student);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var deleted = await _studentService.Remove(student, GetToken());
            if (deleted) return RedirectToAction("StudentsList", "Students");

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string Id)
        {
            return View();
        }

    }
}
