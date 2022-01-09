using Colegio.WebApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Colegio.WebApp.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;

        public StudentCourseService(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }
        public async Task<ServiceReturn> Add(StudentCourseViewModel entity, string jwtToken)
        {
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");

            var studentCourseJson = new StringContent(
                JsonConvert.SerializeObject(entity),
                Encoding.UTF8,
                Application.Json);

            var response = await client.PostAsync("api/StudentCourse/insert-new-student_course", studentCourseJson);
            if (response.IsSuccessStatusCode)
            {
                return new ServiceReturn
                {
                    Success = true,
                };
            }
            var serviceReturn = new ServiceReturn();
            serviceReturn.Success = false;
            serviceReturn.Errors = new List<Error>();
            var message = await response.Content.ReadAsStringAsync();
            serviceReturn.Errors.Add(new Error
            {
                Code = response.StatusCode.ToString(),
                Message = message == null ? string.Empty : message
            });

            return serviceReturn;
        }

        public async Task<IEnumerable<StudentCourseViewModel>> GetAll(string jwtToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetSection("ApiColegio").Value}/api/StudentCourse/get-all-students-course");
            var client = _client.CreateClient();
            request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<StudentCourseViewModel>>();
                if (content == null) return Enumerable.Empty<StudentCourseViewModel>();

                return content;
            }

            throw new Exception("ocorreu um erro ao fazer a requisição no servidor");
        }

        public Task<StudentCourseViewModel> GetById(string Id, string jwtToken)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentCourseViewModel> GetOne(string Course_Id, string Student_Id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetSection("ApiColegio").Value}/api/StudentCourse/get-student_course-by-id");
            var client = _client.CreateClient();
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Headers.Add("Course_Id", Course_Id);
            request.Headers.Add("student_Id", Student_Id);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<StudentCourseViewModel>();
                if (content != null) return content;
            }
            throw new Exception();
        }

        public Task<ServiceReturn> Remove(StudentCourseViewModel entity, string jwtToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceReturn> Update(StudentCourseViewModel entity, string jwtToken)
        {
            var studentCourseJson = new StringContent(JsonConvert.SerializeObject(entity),
                                                        Encoding.UTF8,
                                                        Application.Json);
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");

            var response = await client.PostAsync("api/StudentCourse/update-student_course-status", studentCourseJson);
            if (response.IsSuccessStatusCode)
            {
                return new ServiceReturn
                {
                    Success = true,
                };
            }
            var serviceReturn = new ServiceReturn();
            serviceReturn.Success = false;
            serviceReturn.Errors = new List<Error>();
            var message = await response.Content.ReadAsStringAsync();
            serviceReturn.Errors.Add(new Error
            {
                Code = response.StatusCode.ToString(),
                Message = message == null ? string.Empty : message
            });

            return serviceReturn;
        }
    }
}
