using Fast.Workshops.Domain.Entities;
using Fast.Workshops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fast.Workshops.Infrastructure.Repositories
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly Context _context;
        public WorkshopRepository(Context context)
        {
            _context = context;
        }
        public async Task<Workshop?> GetByIdAsync(Guid id)
        {
            return await _context.Workshops
                .Include(w => w.WorkshopColaboradores!)
                    .ThenInclude(wc => wc.Colaborador)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Workshop>> GetAllAsync()
        {
            return await _context.Workshops.ToListAsync();
        }
        public async Task<Workshop?> GetByDateAsync(DateTime date)
        {
            return await _context.Workshops
                .FirstOrDefaultAsync(w => w.Date == date);
        }

        public async Task CreateAsync(Workshop workshop)
        {
            _context.Workshops.Add(workshop);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                _context.Workshops.Remove(workshop);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Workshop workshop)
        {
            _context.Workshops.Update(workshop);
            await _context.SaveChangesAsync();
        }
    }
}
