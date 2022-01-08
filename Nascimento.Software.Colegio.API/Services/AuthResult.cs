namespace Nascimento.Software.Colegio.API.Services
{
    public class AuthResult
    {
        public string? Token { get; set; }
        public bool Success { get; set; } = false;
        public List<string>? Errors { get; set; }
    }
}
