using Autofac;
using GameStreamer.Backend.Hubs;
using Autofac.Extensions.DependencyInjection;
using GameStreamer.Backend.Services;
using GameStreamer.Backend.Storage.GameStreamerDbase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(ConfigueGameStreamerHost)
            .ConfigureServices((hostContext, services) =>
            {

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

                services.AddSignalR();

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                        });
                });

                services.AddHostedService<TestScheduleService>();

                services.AddSingleton<IRoomsManager>(new RoomsManager());

                //services.AddMassTransit(x =>
                //{
                //    x.UsingRabbitMq((rmqContext, cfg) =>
                //    {
                //        cfg.Host("localhost", "xo_game", h =>
                //        {
                //            h.Username("xo_admin");
                //            h.Password("xo_admin");
                //        });
                //        cfg.ConfigureEndpoints(rmqContext);

                //    });

                //});

            });

var app = builder.Build();

app.UseCors();
app.MapHub<RoomsHub>("/lobbies");

app.MapHub<GameHub>("/game");

var logger = app.Logger;
var lifetime = app.Lifetime;
var env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} is started.")
);

app.Logger.LogInformation("GameStreamer is Running!");

InitializeDatabase(app);

app.Run();


void ConfigueGameStreamerHost(HostBuilderContext builderContext, ContainerBuilder containerBuilder)
{

    //#region Persistance Setup

    containerBuilder.Register(context =>
                    new DbContextOptionsBuilder<GameStreamerContext>()
                        .UseNpgsql("Host=localhost;Database=xo_game_gamestreamerservice;User Id=local;Password=local")
                        .Options)
                .As<DbContextOptions<GameStreamerContext>>().SingleInstance();

    containerBuilder.RegisterType<GameStreamerContext>().InstancePerDependency();

    //#endregion
}

void InitializeDatabase(IApplicationBuilder application)
{
    //using (var scope = application.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    //{
    //    scope.ServiceProvider.GetRequiredService<GameStreamerContext>().Database.Migrate();
    //}
}