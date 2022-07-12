using Autofac;
using Hangfire;
using Hangfire.PostgreSql;
using MatchMake.Backend.Contracts;
using MatchMake.Backend.Processes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MatchMake.Backend.Domain.Processes.UserNotification_TestHangfire;
using MatchMake.Backend.Setup;
using MassTransit;
using System.Reflection;
using MatchMake.Backend.MessageBus.Publishers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(ConfigueMatchMakerHost)
            .ConfigureServices((hostContext, services) => {
                
                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                
                services.AddHangfire(configuration =>
                {
                    configuration.UsePostgreSqlStorage("Host=localhost;Port=5432;Database=xo_game_matchmakeservice;User Id=local;Password=local;", new PostgreSqlStorageOptions() { SchemaName = "matchmake_jobs" });
                });

                services.AddHangfireServer();

                services.AddMassTransit(x =>
                {
                    x.SetKebabCaseEndpointNameFormatter();

                    x.SetInMemorySagaRepositoryProvider();

                    var entryAssembly = Assembly.GetEntryAssembly();

                    x.AddConsumers(entryAssembly);
                    x.AddSagaStateMachines(entryAssembly);
                    x.AddSagas(entryAssembly);
                    x.AddActivities(entryAssembly);

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

                services.AddHostedService<HelloMessagePublisher>();

                services.AddLogging(logging => logging.AddConsole()).BuildServiceProvider();

            });

static void ConfigueMatchMakerHost(ContainerBuilder builder)
{

    #region Message Bus

    //IConnectionFactory connection = new ConnectionFactory()
    //{
    //    HostName = Config.RMQSettings.Host,
    //    Port = Config.RMQSettings.Port,
    //    UserName = Config.RMQSettings.UserName,
    //    Password = Config.RMQSettings.Password,
    //    VirtualHost = Config.RMQConnectionConfigurations.ConfigurationMapp.Mq.VirtualHost
    //};

    //builder.Register(context => connection);

    #endregion

    builder.RegisterType<HelloWorldTestProcess>().As<IParallelProcess>();
    builder.RegisterType<NotificationProcess>().As<IParallelProcess>();

    builder.RegisterType<ProcessStarter>().As<IProcessStarter>();
}

var app = builder.Build();

var logger = app.Logger;
var lifetime = app.Lifetime;
var env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} is started.")
);

app.Logger.LogInformation("Hi! The MatchMake.Backend is Running!");
app.UseHangfireDashboard("/jobs");

#region Config All Scheduling Jobs

var starter = app.Services.GetRequiredService<IProcessStarter>();
starter.ScheduleAllProcesses();

#endregion

app.Run();