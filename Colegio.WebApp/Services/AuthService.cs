using Colegio.WebApp.Models;
using Colegio.WebApp.Models.User;
using Colegio.WebApp.Services.LoginResult;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Colegio.WebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;
        public AuthService(IHttpClientFactory client, IConfiguration config)
        {
            _config = config;
            _client = client;
        }

        public async Task<UserViewModel> GetUserByEmail(string email, string token)
        {
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Add("Email", email);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{token}");
            var request = new HttpRequestMessage(HttpMethod.Get, "api/AuthManager/get-user-by-email");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<UserViewModel>();
                if(content !=null) return content;
            }
            throw new Exception("Not found");
        }

        public async Task<AuthResult> Login(LoginViewModel login)
        {
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");

            var loginJson = new StringContent(
        JsonConvert.SerializeObject(login),
        Encoding.UTF8,
        Application.Json);
            var response = await client.PostAsync("/api/AuthManager/user-login", loginJson);
            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
                if (authResult != null) return authResult;
            }
            return new AuthResult()
            {
                Success = false,
            };
        }

        public async Task<AuthResult> Register(RegisterViewModel register)
        {
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            var registerJson = new StringContent(
                JsonConvert.SerializeObject(register),
                Encoding.UTF8,
                Application.Json);
            var response = await client.PostAsync("api/AuthManager/user-register", registerJson);
            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();
                if (authResult != null) return authResult;
            }

            return new AuthResult()
            {
                Success = false,
            };

        }
    }
}
