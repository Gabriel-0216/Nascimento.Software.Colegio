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
        private string GetToken()
        {
            var token = Request.Cookies["JwtToken"];
            if (token == null) return string.Empty;

            return token;
        }

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
        public IActionResult Create()
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
                if (inserted.Success) return await Index();

                var errorMessage = new List<string>();
                errorMessage.Add("The server response wasn't succesful");
                return Error(new CourseErrorViewModel()
                {
                    ErrorMessage = errorMessage,
                    Success = false,
                });
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
                var errorMessage = new List<string>();
                errorMessage.Add("The data you sent wasn't ok");
                return Error(new CourseErrorViewModel()
                {
                    ErrorMessage = errorMessage,
                    Success = false,
                });
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
                if (updated.Success) return RedirectToAction("Index", "Course");

                var error = new CourseErrorViewModel();
                error.Success = false;
                foreach (var item in updated.Errors)
                {
                    error.ErrorMessage.Add(item.Message);
                }

                return Error(error);
            }
            var errorMessage = new List<string>();
            errorMessage.Add("The data you sent wasn't ok");
            return Error(new CourseErrorViewModel()
            {
                ErrorMessage = errorMessage,
                Success = false,
            });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");

            var course = await _courseService.GetById(Id, GetToken());

            if (course == null) 
            {
                var errorMessage = new List<string>();
                errorMessage.Add("This course don't exists");
                return Error(new CourseErrorViewModel()
                {
                    ErrorMessage = errorMessage,
                    Success = false,
                });
            }

            return View(course);


        }
        [HttpPost]
        public async Task<IActionResult> Delete(Course course)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");


                var deleted = await _courseService.Remove(course, GetToken());
                if (deleted.Success) return RedirectToAction("Index", "Course");

            var error = new CourseErrorViewModel();
            error.Success = false;
            foreach(var item in deleted.Errors)
            {
                error.ErrorMessage.Add(item.Message);
            }

            return RedirectToAction("Error", "Course", error);
     
        }


        public IActionResult Error(CourseErrorViewModel error)
        {
            return View(error);
        }


    }

    public class CourseErrorViewModel
    {
        public bool Success { get; set; }
        public List<string> ErrorMessage { get; set; } = new List<string>();
    }

}
