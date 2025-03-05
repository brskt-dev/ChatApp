using chat_api.Infrastructure.Persistence;
using chat_api.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using chat_api.Presentation.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 🔹 Configuração do PostgreSQL com Entity Framework Core
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Configuração do Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
});

// 🔹 Adicionando SignalR
builder.Services.AddSignalR();

// 🔹 Injeção de Dependência dos Repositórios
builder.Services.AddScoped<MessageRepository, MessageRepository>();

builder.Services.AddControllers();

builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Configura o Swagger (OpenAPI)
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

var app = builder.Build();

// 🔹 Habilitar CORS (caso necessário para permitir conexão do frontend)
app.UseCors(policy =>
    policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)); // ⚠️ Ajuste para produção

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Ativar o filtro de pesquisa
    options.EnableFilter();
});

// 🔹 Configurar endpoints
app.UseRouting();
app.UseAuthorization();
app.MapHub<ChatHub>("/chathub"); // Endpoint do SignalR

app.UseHttpsRedirection();
app.MapControllers();

app.Run();