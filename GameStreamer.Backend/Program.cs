
using GameStreamer.Backend.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<RoomsHub>("/room");
app.Run();
