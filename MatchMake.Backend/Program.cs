using Autofac;
using Hangfire;
using MatchMake.Backend;
using Hangfire.PostgreSql;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using MatchMake.Backend.Setup;
using Serilog;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, MatchMake.Backend!");

CreateHostBuilder(args).Build().Run();


static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
                .ConfigureServices(services => services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<StartHostProcessesService>();
                    
                    #region Job Management Service

                    services.AddHangfire(config => config.UsePostgreSqlStorage("Host=postgres;Database=MatchMaker_JobManagement;User Id=xo_admin;Password=xo_admin;"));
                    services.AddHangfireServer();

                    // app.UseHangfireDashboard("/jobs");

                    BackgroundJob.Enqueue(
                                    () => Console.WriteLine("Hello Single Job from Hangfire!")
                                    );

                    RecurringJob.AddOrUpdate(
                                    () => Console.WriteLine("Hello Recurring Job from Hangfire!"),
                                    Cron.Minutely
                                    );

                    #endregion

                });

static void ConfigureContainer(HostBuilderContext hostContext, ContainerBuilder builder)
{
    builder.RegisterType<StartHostProcessesService>().As<IHostedService>().SingleInstance();

    var Config = new Settings();
    SettingsExtensions.SetFromEnvironmentVariables(Config);

    #region Logger

    var logger = Log.Logger;
    logger = new LoggerConfiguration().CreateLogger();
    
    logger.Information("Стартуем логирование MatchMake.Backend!");

    builder.Register(context => logger).As<ILogger>().SingleInstance();

    #endregion

    builder.Register(context => Config).As<Settings>().SingleInstance();

    #region Database

    builder.Register(context =>
                    new DbContextOptionsBuilder<MbeContext>()
                        .UseNpgsql(Config.ConnectionStrings.MatchMakeContext)
                        .Options)
                .As<DbContextOptions<MbeContext>>().SingleInstance();

    builder.Register(context => new DbContextOptionsBuilder<SomeExampleContext>()
           .UseNpgsql(Config.ConnectionStrings.MatchMakeContext).Options)
           .As<DbContextOptions<SomeExampleContext>>().SingleInstance();

    builder.RegisterType<SomeExampleContext>()
           .InstancePerDependency();

    //builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IAsyncRepositoryWithTypedId<,>))
    //       .InstancePerDependency();

    // Add a custom scoped service
    //services.AddScoped<ITodoRepository, TodoRepository>();

    #endregion

    #region Message Broker

    #endregion

    #region Match Make Services



    #endregion
}

// Hangfire
// https://www.youtube.com/watch?v=iilRdmNILC8
// https://github.com/codaza/Hangfire