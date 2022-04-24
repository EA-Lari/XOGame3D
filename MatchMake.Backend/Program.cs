using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.PostgreSql;
using MatchMake.Backend.Contracts;
using MatchMake.Backend.Domain.Processes.UserNotification_TestHangfire;
using MatchMake.Backend.Processes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var t = builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(ConfigueMatchMakerHost)
            .ConfigureServices(services => {
                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                services.AddHostedService<NotificationProcess>();
                services.AddHostedService<HelloWorldTestProcess>();
                }
            );

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

    #region Business Processes   

    #endregion
}


builder.Services.AddHangfire(configuration =>
                    {
                        configuration.UsePostgreSqlStorage("Host=localhost;Port=25432;Database=xo_admin;User Id=xo_admin;Password=xo_admin;", new PostgreSqlStorageOptions() { SchemaName = "matchmake_jobs"});
                    });

//builder.Services.AddHangfireServer();

var app = builder.Build();

var registeredProcessList = app.Services.GetServices<IParallelProcess>();

var logger = app.Logger;
var lifetime = app.Lifetime;
var env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} started")
);

app.Logger.LogInformation("Hi! The MatchMake.Backend is Running!");
app.UseHangfireDashboard("/jobs");

#region Config All Scheduling Jobs



#endregion

app.Run();