using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Fast.Workshops.Application.DTOs.Auth
{
    public class AuthResponse
    {
        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string AccessToken { get; set; } = string.Empty;
    }
}
