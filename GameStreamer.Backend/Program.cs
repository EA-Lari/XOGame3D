using Autofac;
using GameStreamer.Backend.Hubs;
using Autofac.Extensions.DependencyInjection;
using GameStreamer.Backend.Services;
using GameStreamer.Backend.Storage.GameStreamerDbase;
using Microsoft.EntityFrameworkCore;
using GameStreamer.Backend.Storage;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using GameStreamer.Backend.Jobs;
using GameStreamer.Backend.Consumers;
using GameStreamer.Backend.Consumers.Definitions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(ConfigureGameStreamerHost)
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

                services.AddSingleton<IRoomsManager, RoomsManager>();
                services.AddSingleton<IRoomRepository, RoomRepository>();

                services.AddScoped<ICustomJobService, CustomJobService>();

                services.AddHangfire(config =>
                    config.UsePostgreSqlStorage("Host=localhost;Database=xo_game_gamestreamerservice;User Id=local;Password=local"));

                services.AddHangfireServer();

                services.AddMassTransit(x =>
                {

                    x.AddConsumer<TurnAcceptedConsumer>(typeof(TurnAcceptedConsumerDefinition));
                    x.AddConsumer<TurnNotAcceptedConsumer>(typeof(TurnNotAcceptedConsumerDefinition));

                    x.SetKebabCaseEndpointNameFormatter();

                    x.UsingRabbitMq((rmqContext, cfg) =>
                    {
                        cfg.Host("localhost", "xo_game", h =>
                        {
                            h.Username("xo_admin");
                            h.Password("xo_admin");
                        });

                        cfg.ConfigureEndpoints(rmqContext);
                    });

                });

            });

var app = builder.Build();

app.UseCors();
app.MapHub<RoomsHub>("/lobbies");

app.MapHub<GameHub>("/game");

app.UseHangfireDashboard();

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

void ConfigureGameStreamerHost(HostBuilderContext builderContext, ContainerBuilder containerBuilder)
{

    #region Persistence Setup

    containerBuilder.Register(context =>
                    new DbContextOptionsBuilder<GameStreamerContext>()
                        .UseNpgsql("Host=localhost;Database=xo_game_gamestreamerservice;User Id=local;Password=local")
                        .Options)
                .As<DbContextOptions<GameStreamerContext>>().SingleInstance();

    containerBuilder.RegisterType<GameStreamerContext>().InstancePerDependency();

    #endregion
}

void InitializeDatabase(IApplicationBuilder application)
{
    //using (var scope = application.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    //{
    //    scope.ServiceProvider.GetRequiredService<GameStreamerContext>().Database.Migrate();
    //}
}