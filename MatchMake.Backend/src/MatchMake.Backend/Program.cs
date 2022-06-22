using MassTransit;
using Autofac.Extensions.DependencyInjection;
using MatchMake.Backend.ServiceBus.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureServices((hostContext, services) => {

                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

                services.AddMassTransit(config =>
                {

                    config.AddConsumer<OrderConsumer>();

                    config.UsingRabbitMq((rmqContext, cfg) =>
                    {
                        cfg.Host("localhost", "xo_game", h =>
                        {
                            h.Username("xo_admin");
                            h.Password("xo_admin");
                        });

                        cfg.ReceiveEndpoint("order-queue", c =>
                        {
                            c.ConfigureConsumer<OrderConsumer>(rmqContext);
                        });

                        //cfg.ConfigureEndpoints(rmqContext);

                    });

                });

            });

var app = builder.Build();

var logger = app.Logger;
var lifetime = app.Lifetime;
var env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} is started.")
);

app.Logger.LogInformation("Hi! The MatchMake.Backend is Running!");

app.Run();