using System.ComponentModel.DataAnnotations;

namespace Fast.Workshops.Application.DTOs.Workshops
{
    public class WorkshopRequest
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; }
        [Required] public string Description { get; set; } = string.Empty;
    }
}
