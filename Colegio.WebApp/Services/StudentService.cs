using Colegio.WebApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Colegio.WebApp.Services
{
    public class StudentService : IService<Student>
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;
        public StudentService(IHttpClientFactory _clientFactory, IConfiguration config)
        {
            _client = _clientFactory;
            _config = config;
        }
        public async Task<ServiceReturn> Add(Student entity, string jwtToken)
        {
            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");

            var studentJson = new StringContent(
                    JsonConvert.SerializeObject(entity),
                    Encoding.UTF8,
                    Application.Json);
            var response = await client.PostAsync("/api/student/insert-new-student", studentJson);

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

        public async Task<IEnumerable<Student>> GetAll(string jwtToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetSection("ApiColegio").Value}/api/Student/get-all-students");
            var client = _client.CreateClient();
            request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<IEnumerable<Student>>();
                if (content != null) return content;

                return Enumerable.Empty<Student>();

            }
            throw new Exception("Ocorreu um erro ao fazer a requisição no servidor");
        }

        public async Task<Student> GetById(string Id, string jwtToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetSection("ApiColegio").Value}/api/Student/get-one-student");
            var client = _client.CreateClient();
            request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            request.Headers.Add("Id", Id.ToString());
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Student>();
                if (content != null) return content;
            }
            throw new Exception();
        }

        public async Task<ServiceReturn> Remove(Student entity, string jwtToken)
        {
            var client = _client.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_config.GetSection("ApiColegio").Value}/api/Student/delete-student");
            request.Headers.Add("Id", entity.Id.ToString());
            request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var response = await client.SendAsync(request);
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

        public async Task<ServiceReturn> Update(Student entity, string jwtToken)
        {
            var p = new
            {
                Name = entity.Name,
                Phone = entity.Phone,
                Email = entity.Email,
                Birthdate = entity.Birthdate,
            };
            var studentJson = new StringContent(
                    JsonConvert.SerializeObject(p),
                    Encoding.UTF8,
                    Application.Json);

            var client = _client.CreateClient();
            client.BaseAddress = new Uri($"{_config.GetSection("ApiColegio").Value}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{jwtToken}");
            client.DefaultRequestHeaders.Add("Id", entity.Id.ToString());

            var response = await client.PutAsync("/api/Student/update-student", studentJson);
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
