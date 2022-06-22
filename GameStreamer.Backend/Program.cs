using Autofac;
using MassTransit;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using GameStreamer.Backend.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            //.ConfigureContainer<ContainerBuilder>(ConfigueGameStreamerHost)
            .ConfigureServices((hostContext, services) => {

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

                services.AddMassTransit(x =>
                {
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

                //services.AddMassTransitHostedService();


            });

//static void ConfigueGameStreamerHost(ContainerBuilder builder)
//{

//}

var app = builder.Build();

var logger = app.Logger;
var lifetime = app.Lifetime;
var env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} is started.")
);

app.Logger.LogInformation("Hi! The GameStreamer.Backend is Running!");

app.Run();