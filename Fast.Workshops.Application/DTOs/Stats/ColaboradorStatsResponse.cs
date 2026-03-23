namespace Fast.Workshops.Application.DTOs.Stats
{
    public class ColaboradorStatsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalWorkshops { get; set; }
    }
}