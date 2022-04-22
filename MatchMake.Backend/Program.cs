using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.PostgreSql;
using MatchMake.Backend.Contracts;
using MatchMake.Backend.Processes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddSingleton<IParallelProcess, HelloWorldTestProcess>();

builder.Services.AddHangfire(configuration =>
                    {
                        configuration.UsePostgreSqlStorage("Host=localhost;Port=30000;Database=xo_admin;User Id=xo_admin;Password=xo_admin;", new PostgreSqlStorageOptions() { SchemaName = "matchmake_jobs"});
                    });

builder.Services.AddHangfireServer();

var app = builder.Build();

IParallelProcess process = app.Services.GetRequiredService<IParallelProcess>();
ILogger logger = app.Logger;
IHostApplicationLifetime lifetime = app.Lifetime;
IWebHostEnvironment env = app.Environment;

lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation(
        $"The application {env.ApplicationName} started" +
        $" with injected {process}")
);

app.Logger.LogInformation("Hi! The MatchMake.Backend is Running!");
app.UseHangfireDashboard("/jobs");

app.Run();

Task.Delay(5000);

BackgroundJob.Enqueue(
                () => process.StartAsync(new CancellationToken())
                );

//CreateHostBuilder(args).Build().Run();


//static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
//                .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
//                .ConfigureServices(services => services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true))
//                .ConfigureServices((hostContext, services) =>
//                {
//                    //services.AddHostedService<StartHostProcessesService>();
//                });

//static void ConfigureContainer(HostBuilderContext hostContext, ContainerBuilder builder)
//{
//    builder.RegisterType<StartHostProcessesService>().As<IHostedService>().SingleInstance();

//    var Config = new Settings();
//    SettingsExtensions.SetFromEnvironmentVariables(Config);

//    #region Logger

//    var logger = Log.Logger;
//    logger = new LoggerConfiguration().CreateLogger();

//    logger.Information("Стартуем логирование MatchMake.Backend!");

//    builder.Register(context => logger).As<ILogger>().SingleInstance();

//    #endregion

//    builder.Register(context => Config).As<Settings>().SingleInstance();

//    #region Database

//    builder.Register(context =>
//                    new DbContextOptionsBuilder<MatchMakeContext>()
//                        .UseNpgsql(Config.ConnectionStrings.MatchMakeContext)
//                        .Options)
//                .As<DbContextOptions<MatchMakeContext>>().SingleInstance();

//    builder.RegisterType<MatchMakeContext>()
//           .InstancePerDependency();

//    builder.RegisterGeneric(typeof(AsyncRepositoryUnderMatchMakeDbase<,>)).As(typeof(IAsyncRepository<,>))
//           .InstancePerDependency();

//    #endregion

//    #region Automapper Models

//    //builder.Register(ctx => new MapperConfiguration(cfg => { cfg.AddProfile(new Mobile.BackEcosystem.Automapper.MappingProfile(Convert.ToBoolean(Config.IsInstanceForMkb))); }
//    //));
//    //builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

//    #endregion

//    #region Message Broker

//    #endregion

//}