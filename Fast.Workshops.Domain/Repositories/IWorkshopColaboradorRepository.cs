namespace Fast.Workshops.Domain.Repositories
{
    public interface IWorkshopColaboradorRepository
    {
        Task AddAsync(WorkshopColaborador workshopColaborador);
        Task RemoveAsync(Guid workshopId, Guid colaboradorId);
        Task<WorkshopColaborador?> GetAsync(Guid workshopId, Guid colaboradorId);
    }
}