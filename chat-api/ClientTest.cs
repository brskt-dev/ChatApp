using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace chat_api;

class Program
{
    static async Task Main()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/chathub")
            .Build();

        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        await connection.StartAsync();
        Console.WriteLine("Conectado ao SignalR!");

        while (true)
        {
            Console.Write("Digite sua mensagem: ");
            string msg = Console.ReadLine();
            await connection.InvokeAsync("SendMessage", "TesteUser", msg);
        }
    }
}
