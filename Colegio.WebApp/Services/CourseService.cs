using Colegio.WebApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Colegio.WebApp.Services
{
    public class CourseService : IService<Course>
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public CourseService(IHttpClientFactory _clientFactory, IConfiguration config)
        {
            _client = _clientFactory;
            _config = config;
        }
        public async Task<bool> Add(Course entity, string jwtToken)
        {
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");

            var courseJson = new StringContent(
                    JsonConvert.SerializeObject(entity),
                    Encoding.UTF8,
                    Application.Json);

            var response = await client.PostAsync("/api/Course/insert-new-course", courseJson);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Course>> GetAll(string jwtToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetSection("ApiColegio").Value}/api/Course/get-all-courses");
            var client = _client.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<Course>>();
                if (content != null) return content;

                return Enumerable.Empty<Course>();
            }
            throw new Exception("Ocorreu um erro ao fazer a requisição no servidor");
        }

        public async Task<Course> GetById(string Id, string jwtToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetSection("ApiColegio").Value}/api/Course/get-course-by-id");
            var client = _client.CreateClient();
            request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            request.Headers.Add("courseId", Id.ToString());

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Course>();
                return content;
            }
            return null;

        }
        public async Task<bool> Remove(Course entity, string jwtToken)
        {
            var client = _client.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_config.GetSection("ApiColegio").Value}/api/Course/delete-course");
            request.Headers.Add("courseId", entity.Id.ToString());
            request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Update(Course entity, string jwtToken)
        {
            var p = new
            {
                Title = entity.Title,
                Resume = entity.Resume,
            };

            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");
            client.DefaultRequestHeaders.Add("courseId", entity.Id.ToString());
            var courseJson = new StringContent(
                    JsonConvert.SerializeObject(p),
                    Encoding.UTF8,
                    Application.Json);

            var response = await client.PutAsync("/api/Course/update-course", courseJson);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
    }
}
