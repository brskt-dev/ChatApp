using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Repositories;

namespace ChatApp.Application.Services
{
    public class MessageService
    {
        private readonly IRepository<Message> _messageRepository;

        public MessageService(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<Message>> GetMessages() => await _messageRepository.GetAll();
    }
}