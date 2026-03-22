using Fast.Workshops.Domain.Entities;

namespace Fast.Workshops.Domain.Repositories
{
    public interface IWorkshopRepository
    {
        Task<Workshop?> GetByIdAsync(Guid id);
        Task<List<Workshop>> GetAllAsync();
        Task<Workshop?> GetByDateAsync(DateTime date);
        Task CreateAsync(Workshop workshop);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Workshop workshop);

    }
}
