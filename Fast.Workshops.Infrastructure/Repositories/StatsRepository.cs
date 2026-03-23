using Fast.Workshops.Domain.Entities;
using Fast.Workshops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fast.Workshops.Infrastructure.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly Context _context;

        public StatsRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Colaborador>> GetColaboradoresComWorkshopsAsync()
        {
            return await _context.Colaboradores
                .Include(c => c.WorkshopColaboradores)
                .ToListAsync();
        }

        public async Task<List<Workshop>> GetWorkshopsComColaboradoresAsync()
        {
            return await _context.Workshops
                .Include(w => w.WorkshopColaboradores)
                .ToListAsync();
        }
    }
}