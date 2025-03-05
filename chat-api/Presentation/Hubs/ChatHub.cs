using Microsoft.AspNetCore.SignalR;
using chat_api.Infrastructure.Persistence.Repositories;
using chat_api.Domain.Entities;
using chat_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace chat_api.Presentation.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageRepository _messageRepository;
    private readonly ChatDbContext _context;

    public ChatHub(IMessageRepository messageRepository, ChatDbContext context)
    {
        _messageRepository = messageRepository;
        _context = context;
    }

    public async Task SendMessage(string username, string messageContent)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(messageContent))
        {
            throw new HubException("Nome de usuário e mensagem não podem estar vazios.");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        // Se o usuário não existir, cria um novo e salva no banco
        if (user == null)
        {
            user = new User
            {
                Username = username,
                Email = $"{username}@example.com" // Pode ser ajustado conforme necessário
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(); // Salva para gerar o ID corretamente
        }

        // Agora o user.Id sempre estará preenchido corretamente
        var message = new Message
        {
            Content = messageContent,
            Timestamp = DateTime.UtcNow,
            UserId = user.Id, // Agora garantimos que o UserId está correto
        };

        await _messageRepository.AddAsync(message);

        // Notificar todos os clientes que uma nova mensagem foi enviada
        await Clients.All.SendAsync("ReceiveMessage", username, messageContent);
    }
}