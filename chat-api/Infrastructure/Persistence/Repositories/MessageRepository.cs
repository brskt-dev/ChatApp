using chat_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace chat_api.Infrastructure.Persistence.Repositories;

public interface IMessageRepository : IRepository<Message>
{
    Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId);
}

public class MessageRepository : Repository<Message>, IMessageRepository
{
    private readonly ChatDbContext _context;

    public MessageRepository(ChatDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
    {
        return await _context.Messages.Where(m => m.UserId == userId).ToListAsync();
    }
}