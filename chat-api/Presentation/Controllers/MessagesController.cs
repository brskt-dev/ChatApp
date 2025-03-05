using chat_api.Domain.Entities;
using chat_api.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace chat_api.Presentation.Controllers;

[ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;

    public MessagesController(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    // 🔹 Retorna todas as mensagens armazenadas
    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var messages = await _messageRepository.GetAllAsync();
        return Ok(messages);
    }

    // 🔹 Retorna as mensagens de um usuário específico
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetMessagesByUser(int userId)
    {
        var messages = await _messageRepository.GetMessagesByUserIdAsync(userId);
        return Ok(messages);
    }

    // 🔹 Envia uma nova mensagem (caso queiramos enviar via API REST)
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] Message message)
    {
        if (message == null || string.IsNullOrEmpty(message.Content))
            return BadRequest("Mensagem inválida!");

        message.Timestamp = DateTime.UtcNow;
        await _messageRepository.AddAsync(message);

        return Ok(new { message = "Mensagem enviada com sucesso!" });
    }
}