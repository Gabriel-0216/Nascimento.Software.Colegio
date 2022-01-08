using Colegio.WebApp.Models.User;

namespace Colegio.WebApp.Services.LoginResult
{
    public sealed class AuthResult
    {
        public string? Token { get; set; }
        public bool Success { get; set; } = false;
        public string Email { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
    }
}
