using Fast.Workshops.Domain.Entities;

namespace Fast.Workshops.Domain.Repositories
{
    public interface IColaboradorRepository
    {
        Task<Colaborador?> GetByIdAsync(Guid id);
        Task<List<Colaborador>> GetAllAsync();
        Task CreateAsync(Colaborador colaborador);
        Task UpdateAsync(Colaborador colaborador);
        Task DeleteAsync(Guid id);
    }
}