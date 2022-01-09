using Colegio.WebApp.Models;
using Colegio.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colegio.WebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly IService<Course> _courseService;

        public CourseController(IService<Course> courseService)
        {
            _courseService = courseService;
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

        public async Task<IActionResult> Index()
        {//get all
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var coursesList = await _courseService.GetAll(GetToken());
            return View(coursesList);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Course model)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                var inserted = await _courseService.Add(model, GetToken());
                if (inserted) return await Index();

                return await Error(
              new CourseErrorViewModel
              {
                  ErrorMessage = "The server response wasn't sucesfull!",
                  Success = false
              }
                 );
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var course = await _courseService.GetById(Id, GetToken());
            if (course == null)
            {
                return await Error(
              new CourseErrorViewModel
              {
                  ErrorMessage = "There's a problem with the data you sent!",
                  Success = false
              }
          );
            }

            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Course model)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                var updated = await _courseService.Update(model, GetToken());
                if (updated) return RedirectToAction("Index", "Course");

                return await Error(
              new CourseErrorViewModel
              {
                  ErrorMessage = "the server response wasn't sucesful!",
                  Success = false
              }
          );
            }
            return await Error(
              new CourseErrorViewModel
              {
                  ErrorMessage = "There's a problem with the data you sent!",
                  Success = false
              }
          );
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var course = await _courseService.GetById(Id, GetToken());

            if (course == null) 
            {
                return await Error(
                  new CourseErrorViewModel
                  {
                      ErrorMessage = "There's a problem with the data you sent!",
                      Success = false
                  }
              );
            }

            return View(course);


        }
        [HttpPost]
        public async Task<IActionResult> Delete(Course course)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");


                var deleted = await _courseService.Remove(course, GetToken());
                if (deleted) return RedirectToAction("Index", "Course");

                return await Error(
              new CourseErrorViewModel
              {
                  ErrorMessage = "There's a problem with the server!",
                  Success = false
              }
          );
     
        }


        public async Task<IActionResult> Error(CourseErrorViewModel error)
        {
            ViewBag["error"] = error;
            return View();
        }


    }

    public class CourseErrorViewModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } 
    }

}
