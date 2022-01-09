using Colegio.WebApp.Models;
using Colegio.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Colegio.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Index([FromServices] IAuthService _servc)
        {
            if (!IsTokenValid()) return RedirectToAction("Login", "Home");
        
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            var token = Request.Cookies["JwtToken"];
            var tokenExpireDate = Request.Cookies["ExpireDate"];

            if(string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(tokenExpireDate) || DateTime.Now > Convert.ToDateTime(tokenExpireDate))
            {
                return View();

            }
            return RedirectToAction("UserPanel", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> UserPanel([FromServices] IAuthService _service)
        {
            var token = Request.Cookies["JwtToken"];
            var email = Request.Cookies["Email"];
            var tokenExpireDate = Request.Cookies["ExpireDate"];

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(email) || DateTime.Now > Convert.ToDateTime(tokenExpireDate))
            {
                return Unauthorized();
            }

            var user = await _service.GetUserByEmail(email, token);

            return View(user); // essa tela vai servir pra logoff, lembrar de apagar os cookies do navegador
        }

      

        [HttpPost]
        public async Task<IActionResult> Login([FromServices] IAuthService _service, LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.Login(login);
                if(result.Success == false)
                {
                    return Unauthorized();
                }

                if (string.IsNullOrWhiteSpace(result.Token) || string.IsNullOrWhiteSpace(result.Email)) return Unauthorized();
       

                if (!SetTokenInCookies(result.Token, result.Email)) return Unauthorized();

                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            Response.Cookies.Delete("JwtToken");

            if (!string.IsNullOrWhiteSpace(Request.Cookies["JwtToken"]))
            {
                RedirectToAction("UserPanel", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromServices] IAuthService _service, RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var insert = await _service.Register(register);
                if(insert.Success == false)
                {
                    return Error();
                }
                if (string.IsNullOrWhiteSpace(insert.Token) || string.IsNullOrWhiteSpace(insert.Email)) return Unauthorized();

                if (!SetTokenInCookies(insert.Token, insert.Email)) return Unauthorized();

                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool SetTokenInCookies(string token, string email)
        {
            Response.Cookies.Append("JwtToken", token);
            Response.Cookies.Append("ExpireDate", DateTime.Now.AddHours(6).ToString()) ;
            Response.Cookies.Append("Email", email);
            return true;

        }
    }
}