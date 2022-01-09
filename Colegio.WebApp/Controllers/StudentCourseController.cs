using Colegio.WebApp.Models;
using Colegio.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.WebApp.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly IStudentCourseService _studentCourseSvc;
        private readonly IService<Student> _studentService;
        private readonly IService<Course> _courseService;

        public StudentCourseController
            (IStudentCourseService service,
            IService<Student> student,
            IService<Course> course)
        {
            _studentCourseSvc = service;
            _courseService = course;
            _studentService = student;
        }
        public async Task<IActionResult> Index()
        {// JOIN 
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var students = await _studentService.GetAll(GetToken());
            var courses = await _courseService.GetAll(GetToken());
            var studentCourse = await _studentCourseSvc.GetAll(GetToken());

            
            var list = new List<StudentCourseIndex>();
            foreach(var item in studentCourse)
            {
                list.Add(new StudentCourseIndex
                {
                    Course = courses.FirstOrDefault(p => p.Id == item.Course_Id),
                    Student = students.FirstOrDefault(p => p.Id == item.Student_Id),
                });
            }


            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var students = await _studentService.GetAll(GetToken());
            var courses = await _courseService.GetAll(GetToken());

            var studentCourse = new StudentCourseViewModel
            {
                Students = students.ToList(),
                Courses = courses.ToList(),
            };

            return View(studentCourse);

        }
        [HttpPost]
        public async Task<IActionResult> Registration(StudentCourseViewModel model)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            
            if(!string.IsNullOrWhiteSpace(model.Course_Id.ToString()) || !string.IsNullOrWhiteSpace(model.Student_Id.ToString()))
            {
                var inserted = await _studentCourseSvc.Add(model, GetToken());
                if (inserted) return RedirectToAction("Index", "StudentCourse");
            }      

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(string courseId, string studentId)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");
            var student = await _studentService.GetById(studentId, GetToken());
            var course = await _courseService.GetById(courseId, GetToken());
            var studentCourse = await _studentCourseSvc.GetOne(courseId, studentId, GetToken());

            var std = new StudentCourseVm
            {
                Student_Id = student.Id,
                Course_Id = course.Id,
                student = student,
                course = course,
                Active = studentCourse.Active,
            };

            return View(std);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(StudentCourseVm model)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var updated = await _studentCourseSvc.Update(new StudentCourseViewModel
            {
                Active = model.Active,
                Student_Id = model.Student_Id,
                Course_Id = model.Course_Id,
            }, GetToken());
            if (updated) return RedirectToAction("Index", "StudentCourse");


            return RedirectToAction("Error", "Home");
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
    }
}
