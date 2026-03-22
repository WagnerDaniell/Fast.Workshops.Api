namespace Fast.Workshops.Domain.Entities
{
    public class Workshop
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<WorkshopColaborador>? WorkshopColaboradores { get; set; }
    }
}
