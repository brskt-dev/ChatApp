using ChatApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAll() => await _context.Messages.ToListAsync();
        public async Task<Message> GetById(int id) => await _context.Messages.FindAsync(id);
        public async Task Add(Message message) { _context.Messages.Add(message); await _context.SaveChangesAsync(); }
        public async Task Update(Message message) { _context.Messages.Update(message); await _context.SaveChangesAsync(); }
        public async Task Delete(int id) { var message = await GetById(id); _context.Messages.Remove(message); await _context.SaveChangesAsync(); }
    }
}