namespace Colegio.WebApp.Services
{
    public class ServiceReturn
    {
        public bool Success { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
    public class Error
    {
        public string Message { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
