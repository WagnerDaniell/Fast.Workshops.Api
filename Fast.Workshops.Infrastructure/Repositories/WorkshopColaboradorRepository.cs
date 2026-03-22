using Fast.Workshops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fast.Workshops.Infrastructure.Repositories
{
    public class WorkshopColaboradorRepository : IWorkshopColaboradorRepository
    {
        private readonly Context _context;

        public WorkshopColaboradorRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(WorkshopColaborador workshopColaborador)
        {
            await _context.WorkshopColaboradores.AddAsync(workshopColaborador);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid workshopId, Guid colaboradorId)
        {
            var workshopColaborador = await GetAsync(workshopId, colaboradorId);
            if (workshopColaborador is not null)
            {
                _context.WorkshopColaboradores.Remove(workshopColaborador);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WorkshopColaborador?> GetAsync(Guid workshopId, Guid colaboradorId)
        {
            return await _context.WorkshopColaboradores
                .FirstOrDefaultAsync(wc => wc.WorkshopId == workshopId && wc.ColaboradorId == colaboradorId);
        }
    }
}