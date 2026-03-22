using Fast.Workshops.Domain.Entities;

public class WorkshopColaborador
{
    public Guid WorkshopId { get; set; }
    public Workshop? Workshop { get; set; }
    public Guid ColaboradorId { get; set; }
    public Colaborador? Colaborador { get; set; }
}