using Fast.Workshops.Domain.Entities;

namespace Fast.Workshops.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid Id);
        Task CreateUserAsync(User user);
    }
    
}
