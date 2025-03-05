using chat_api.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using chat_api.Infrastructure.Persistence.Repositories;

namespace chat_api.Presentation.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessageRepository _messageRepository;

        public ChatHub(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task SendMessage(string username, string messageContent)
        {
            var message = new Message
            {
                Content = messageContent,
                Timestamp = DateTime.UtcNow,
                User = new User { Username = username }
            };

            await _messageRepository.AddAsync(message);
            await Clients.All.SendAsync("ReceiveMessage", username, messageContent);
        }
    }
}