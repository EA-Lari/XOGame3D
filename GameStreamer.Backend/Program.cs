using Autofac;
using MassTransit;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(ConfigueMatchMakerHost)
            .ConfigureServices((hostContext, services) => {

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

                //services.AddHangfire(configuration =>
                //{
                //    configuration.UsePostgreSqlStorage("Host=localhost;Port=25432;Database=xo_admin;User Id=xo_admin;Password=xo_admin;", new PostgreSqlStorageOptions() { SchemaName = "matchmake_jobs" });
                //});

                //services.AddHangfireServer();

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

            });

static void ConfigueMatchMakerHost(ContainerBuilder builder)
{

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

//app.UseHangfireDashboard("/jobs");

#region Config All Scheduling Jobs

//var starter = app.Services.GetRequiredService<IProcessStarter>();
//starter.ScheduleAllProcesses();

#endregion

app.Run();