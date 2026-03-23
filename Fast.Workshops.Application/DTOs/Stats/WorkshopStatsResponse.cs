namespace Fast.Workshops.Application.DTOs.Stats
{
    public class WorkshopStatsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalColaboradores { get; set; }
    }
}