using System.ComponentModel.DataAnnotations;

namespace Fast.Workshops.Application.DTOs.Colaboradores
{
    public class ColaboradorRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
    }
}