using Fast.Workshops.Domain.Entities;
using Fast.Workshops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fast.Workshops.Infrastructure.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly Context _context;

        public ColaboradorRepository(Context context)
        {
            _context = context;
        }

        public async Task<Colaborador?> GetByIdAsync(Guid id)
        {
            return await _context.Colaboradores
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Colaborador>> GetAllAsync()
        {
            return await _context.Colaboradores.ToListAsync();
        }

        public async Task CreateAsync(Colaborador colaborador)
        {
            await _context.Colaboradores.AddAsync(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Colaborador colaborador)
        {
            _context.Colaboradores.Update(colaborador);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var colaborador = await GetByIdAsync(id);
            if (colaborador is not null)
            {
                _context.Colaboradores.Remove(colaborador);
                await _context.SaveChangesAsync();
            }
        }
    }
}