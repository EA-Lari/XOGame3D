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
using static MatchMake.Backend.Storage.DbaseMapping.RoomMap;
using MatchMake.Backend.DataStorage.MatchMake.Context;
using MatchMake.Backend.DataStorage.MatchMakeDbase;
using MatchMake.Backend.Storage.Contracts;
using MatchMake.Backend.Processes;
using MatchMake.Backend.Contracts;

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
                    new DbContextOptionsBuilder<MatchMakeContext>()
                        .UseNpgsql(Config.ConnectionStrings.MatchMakeContext)
                        .Options)
                .As<DbContextOptions<MatchMakeContext>>().SingleInstance();

    builder.RegisterType<MatchMakeContext>()
           .InstancePerDependency();

    builder.RegisterGeneric(typeof(AsyncRepositoryUnderMatchMakeDbase<,>)).As(typeof(IAsyncRepository<,>))
           .InstancePerDependency();

    #endregion

    #region Automapper Models

    //builder.Register(ctx => new MapperConfiguration(cfg => { cfg.AddProfile(new Mobile.BackEcosystem.Automapper.MappingProfile(Convert.ToBoolean(Config.IsInstanceForMkb))); }
    //));
    //builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

    #endregion

    #region Message Broker

    #endregion

    #region Match Make Domain Processes

    // Create Auto Factory which register dependencies as function
    builder.Register<Func<Type, IParallelProcess>>(x =>
    {
        var context = x.Resolve<IComponentContext>();
        return y =>
        {
            return (IParallelProcess)context.Resolve(y);
        };
    })
        .SingleInstance();

    #endregion
}

// Hangfire
// https://www.youtube.com/watch?v=iilRdmNILC8
// https://github.com/codaza/Hangfire