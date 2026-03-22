namespace Fast.Workshops.Domain.Entities
{
    public class Colaborador
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<WorkshopColaborador>? WorkshopColaboradores { get; set; }
    }
}
