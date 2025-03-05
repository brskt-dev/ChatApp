using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ChatDbContext _context;

        public UserRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll() => await _context.Users.ToListAsync();
        public async Task<User> GetById(int id) => await _context.Users.FindAsync(id);
        public async Task Add(User user) { _context.Users.Add(user); await _context.SaveChangesAsync(); }
        public async Task Update(User user) { _context.Users.Update(user); await _context.SaveChangesAsync(); }
        public async Task Delete(int id) { var user = await GetById(id); _context.Users.Remove(user); await _context.SaveChangesAsync(); }
    }
}