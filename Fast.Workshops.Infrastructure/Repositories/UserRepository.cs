using Fast.Workshops.Domain.Entities;
using Fast.Workshops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fast.Workshops.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByIdAsync(Guid Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
  
}
