using Fast.Workshops.Domain.Entities;

namespace Fast.Workshops.Domain.Repositories
{
    public interface IStatsRepository
    {
        Task<List<Colaborador>> GetColaboradoresComWorkshopsAsync();
        Task<List<Workshop>> GetWorkshopsComColaboradoresAsync();
    }
}