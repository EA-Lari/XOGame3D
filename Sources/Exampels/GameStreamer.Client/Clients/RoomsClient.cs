using Microsoft.AspNetCore.SignalR.Client;

namespace GameStreamer.Client.Clients
{
    public class RoomsClient
    {
        public static async Task ConnectAsync()
        {

            var uri = "http://localhost:5000/xo-rooms";

            await using var connection = new HubConnectionBuilder().WithUrl(uri).Build();

            connection.On<string>("SendHelloWorld", msg => { Console.WriteLine(msg); } );

            await connection.StartAsync();

            await foreach (var date in connection.StreamAsync<DateTime>("Streaming"))
            {
                Console.WriteLine(date);
            }

            await Task.Run(() =>
            {
                connection.InvokeAsync("SendHelloWorld", $"Pidor - {new Random().Next(4, 200)}");
                Task.Delay(5000);
            });

        }
    }
}