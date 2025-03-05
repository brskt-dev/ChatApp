using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using NBomber.CSharp;

class SignalRLoadTest
{
    static async Task Main()
    {
        // Cenário 1: 💥 Teste de RPS (100 mensagens por segundo)
        var scenario1 = Scenario.Create("SignalR RPS Test", async context =>
        {
            try
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("http://192.168.0.20:5000/chathub") // 🔥 Testando no Docker
                    .Build();

                await connection.StartAsync();
                await connection.InvokeAsync("SendMessage", "user_test", "Mensagem de Carga 🚀");
                await connection.StopAsync();

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail<string>();
            }
        })
        .WithLoadSimulations(
            // 💥 100 mensagens por segundo durante 30 segundos
            Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30))
        );

        // Cenário 2: 👥 Teste de 500 conexões simultâneas
        var scenario2 = Scenario.Create("SignalR Concurrent Users Test", async context =>
        {
            try
            {
                var connection = new HubConnectionBuilder()
                    .WithUrl("http://192.168.0.20:5000/chathub") // 🔥 Testando no Docker
                    .Build();

                await connection.StartAsync();
                await connection.InvokeAsync("SendMessage", "user_test", "Mensagem de Carga 🚀");
                await connection.StopAsync();

                return Response.Ok();
            }
            catch (Exception ex)
            {
                return Response.Fail<string>();
            }
        })
        .WithLoadSimulations(
            // 👥 500 conexões simultâneas por 30 segundos
            Simulation.KeepConstant(copies: 500, during: TimeSpan.FromSeconds(30))
        );

        // 🔥 Agora registramos os dois cenários
        NBomberRunner
            .RegisterScenarios(scenario1, scenario2)
            .Run();
    }
}
