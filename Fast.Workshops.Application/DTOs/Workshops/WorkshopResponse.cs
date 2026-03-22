using Fast.Workshops.Application.DTOs.Colaboradores;

namespace Fast.Workshops.Application.DTOs.Workshops
{
    public class WorkshopResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<ColaboradorResponse> Colaboradores { get; set; } = new();
    }
}